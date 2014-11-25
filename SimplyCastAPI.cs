//----------------------------------------------------------------
// SimplyCastAPI.cs
// Copyright SimplyCast 2014
// This projected is licensed under the terms of the MIT license.
//  (see the attached LICENSE.txt).
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimplyCast.ContactManager;
using SimplyCast.SimplyCast360;

namespace SimplyCast
{
    /// <summary>
    /// This is the base API class that is used to make all requests to the
    /// API. 
    /// </summary>
    public class SimplyCastAPI
    {
        /// <summary>
        /// HTTP GET method constant.
        /// </summary>
        public const string GET = "GET";

        /// <summary>
        /// HTTP POST method constant.
        /// </summary>
        public const string POST = "POST";

        /// <summary>
        /// HTTP DELETE method constant.
        /// </summary>
        public const string DELETE = "DELETE";

        /// <summary>
        /// 
        /// </summary>
        private SimplyCastAPIConnector connection;

        /// <summary>
        /// Contact manager API handle.
        /// </summary>
        private ContactManagerAPI contactManager;

        /// <summary>
        /// 360 API handle.
        /// </summary>
        private SimplyCast360API simplycast360;

        #region Accessors
        /// <summary>
        /// Contact manager API resource container.
        /// </summary>
        public ContactManagerAPI ContactManager { 
            get { return this.contactManager; }
        }

        /// <summary>
        /// 360 API resource container.
        /// </summary>
        public SimplyCast360API SimplyCast360
        {
            get { return this.simplycast360; }
        }
        #endregion

        /// <summary>
        /// Main API class constructor.
        /// </summary>
        /// <param name="publicKey">The desired account API public key.</param>
        /// <param name="secretKey">The desired account API secret key.</param>
        public SimplyCastAPI(string publicKey, string secretKey)
        {
            this.connection = new SimplyCastAPIConnector(publicKey, secretKey);
            this.contactManager = new ContactManagerAPI(connection);
            this.simplycast360 = new SimplyCast360API(connection);
        }

        public void setURL(string url)
        {
            this.connection.URL = url;
        }
    }
}
