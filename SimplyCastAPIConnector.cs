//----------------------------------------------------------------
// SimplyCastAPIConnector.cs
// Copyright SimplyCast 2014
// This projected is licensed under the terms of the MIT license.
//  (see the attached LICENSE.txt).
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Net;
using SimplyCast.Common.Responses;

namespace SimplyCast
{
    /// <summary>
    /// SimplyCast REST API connector.
    /// See https://app.simplycast.com/?q=api/reference
    /// </summary>
    public class SimplyCastAPIConnector
    {
        #region Properties
        /// <summary>
        /// The public key, used to identify the user.
        /// </summary>
        private string publicKey;

        /// <summary>
        /// The secret key, used in generating signatures.
        /// </summary>
        private string secretKey;

        /// <summary>
        /// API URL, sans the resource endpoint.
        /// </summary>
        private string apiURL = "https://api.simplycast.com/";
        #endregion

        public string URL
        {
            get { return this.apiURL; }
            set { this.apiURL = value; }
        }

        /// <summary>
        /// API constructor
        /// </summary>
        /// <param name="publicKey">The public key to make the request with.</param>
        /// <param name="secretKey">The secret key to make the request with.</param>
        public SimplyCastAPIConnector(string publicKey, string secretKey)
        {
            this.publicKey = publicKey;
            this.secretKey = secretKey;
        }

        #region Utility Functions
        /// <summary>
        /// Main API calling function.
        /// </summary>
        /// <typeparam name="T">The entity response return type.</typeparam>
        /// <param name="method">The HTTP method (GET, POST, etc).</param>
        /// <param name="resource">The API resource (the part of the URL 
        /// between the base URL and the query parameters.</param>
        /// <param name="queryParameters">A dictionary of request query 
        /// parameters.</param>
        /// <param name="requestBody">A serializable object containing the
        /// rquest body.</param>
        /// <returns>A response entity.</returns>
        public T Call<T>(string method, string resource, Dictionary<string, string> queryParameters, object requestBody)
        {
            string requestBodyString = "";
            if (requestBody == null)
            {
                requestBodyString = "";
            }
            else
            {
                requestBodyString = this._Serialize(requestBody);
            }

            string date = DateTime.UtcNow.ToString("r");

            string requestBodyHash = "";
            if (requestBodyString.Length > 0)
            {
                requestBodyHash = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(SimplyCastAPIConnector.BytesToHex(System.Security.Cryptography.MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(requestBodyString.Trim())))));
            }

            string authHeader = this._GenerateAuthHeader(method, resource, date, requestBodyHash);

            string url = this.apiURL.Trim('/') + '/' + resource.Trim('/');
            if (queryParameters != null && queryParameters.Count > 0)
            {
                url = url + "?";
                foreach (KeyValuePair<string, string> q in queryParameters)
                {
                    url += System.Uri.EscapeDataString(q.Key) + "=" + System.Uri.EscapeDataString(q.Value) + "&";
                }
                url = url.TrimEnd('&');
            }

            HttpWebRequest webHandle = (HttpWebRequest)WebRequest.Create(url);
            webHandle.Method = method;
            webHandle.Headers.Add("X-Date", date);
            webHandle.Accept ="application/xml";
            webHandle.Headers.Add(authHeader);

            if (requestBodyString.Length > 0)
            {
                int contentLength = Encoding.UTF8.GetBytes(requestBodyString).Length;
                webHandle.ContentType = "application/xml";
                webHandle.ContentLength = contentLength;
                webHandle.Headers.Add("Content-MD5",requestBodyHash);
                Stream requestStream = webHandle.GetRequestStream();
                requestStream.Write(Encoding.UTF8.GetBytes(requestBodyString), 0, contentLength);
                requestStream.Close();
            }

            HttpWebResponse webResponse;
            try
            {
                webResponse = (HttpWebResponse) webHandle.GetResponse();
            }
            catch (WebException e)
            {
                webResponse = (HttpWebResponse) e.Response;
            }

            Stream receiveStream = webResponse.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            string responseData = readStream.ReadToEnd();
            readStream.Close();
            receiveStream.Close();

            if ((int) webResponse.StatusCode >= 400)
            {
                APIException exception;
                try
                {
                    Error error = this._Deserialize<Error>(responseData);
                    exception = new APIException(error.Message);
                    exception.StatusCode = webResponse.StatusCode;
                    exception.StatusDescription = webResponse.StatusDescription;
                }
                catch (Exception e)
                {
                    exception = new APIException("An error condition occurred from the API, but could not be deserialized.", e);
                    exception.StatusCode = webResponse.StatusCode;
                    exception.StatusDescription = webResponse.StatusDescription; 
                }
                throw exception;
            }
            else if (webResponse.StatusCode == HttpStatusCode.NoContent) {
                return default(T);
            }

            return (T) this._Deserialize<T>(responseData);
        }

        private string _GenerateAuthHeader(string method, string resource, string date, string requestBodyHash)
        {
            string signature = (method + "\n" + date + "\n" + resource + "\n" + requestBodyHash).Trim();

            System.Security.Cryptography.HMACSHA1 hmac = new System.Security.Cryptography.HMACSHA1();
            hmac.Key = Encoding.UTF8.GetBytes(this.secretKey);

            string authStr = this.publicKey + ':' + SimplyCastAPIConnector.BytesToHex(hmac.ComputeHash(Encoding.UTF8.GetBytes(signature)));
            return "Authorization: HMAC " + System.Convert.ToBase64String(Encoding.UTF8.GetBytes(authStr));
        }

        /// <summary>
        /// Given an array of bytes, convert it to a hex representation.
        /// </summary>
        /// <param name="bytes">The array of bytes to convert.</param>
        /// <returns>A hex string.</returns>
        public static string BytesToHex(byte[] bytes)
        {
            string str = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                str += bytes[i].ToString("x2");
            }
            return str;
        }

        private T _Deserialize<T>(string xml)
        {
            T obj = default(T);

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (TextReader reader = new StringReader(xml))
            {
                 obj = (T)serializer.Deserialize(reader);
            }

            return obj;
        }

        private string _Serialize(object serializableObject)
        {
            string serialized = "";

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            XmlSerializer serializer = new XmlSerializer(serializableObject.GetType());

            using (MemoryStream stream = new MemoryStream())
            using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
            {
                serializer.Serialize(writer, serializableObject, ns);
                serialized = Encoding.UTF8.GetString(stream.ToArray());
            } 

            return serialized;
        }
        #endregion
    }
}
