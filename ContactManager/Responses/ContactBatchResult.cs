//----------------------------------------------------------------
// ContactBatchResult.cs
// Copyright SimplyCast 2015
// This projected is licensed under the terms of the MIT license.
//  (see the attached LICENSE.txt).
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SimplyCast.Common.Responses;
using System.IO;

namespace SimplyCast.ContactManager.Responses
{
    /// <summary>
    /// A result object representation of a batch operation API call.
    /// </summary>
    public class ContactBatchResult
    {
        private int httpResponseCode;
        private string rawContact;
        private ContactEntity contact;

        /// <summary>
        /// HTTP response code of the individual request.
        /// </summary>
        [XmlElement("code")]
        public int HttpResponseCode
        {
            get { return this.httpResponseCode; }
            set { this.httpResponseCode = value; }
        }

        /// <summary>
        /// Raw response setter for deserialization.
        /// </summary>
        [XmlElement("response")]
        public string RawContact
        {
            get { return this.rawContact; }
            set { 
                this.rawContact = value;
                this.contact = ContactBatchResult.decodeContact(value);
            }
        }

        /// <summary>
        /// Accessor for the contact entity returned as part of the request.
        /// </summary>
        public ContactEntity Contact
        {
            get { return this.contact; }
        }

        private static ContactEntity decodeContact(string value)
        {
            XmlSerializer xml = new XmlSerializer(typeof(ContactEntity));
            StringReader reader = new StringReader(System.Net.WebUtility.HtmlDecode(value));

            string foo = System.Net.WebUtility.HtmlDecode(value);

            return (ContactEntity) xml.Deserialize(reader);
        }
    }
}
