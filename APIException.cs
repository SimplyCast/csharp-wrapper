//----------------------------------------------------------------
// APIException.cs
// Copyright SimplyCast 2014
// This projected is licensed under the terms of the MIT license.
//  (see the attached LICENSE.txt).
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace SimplyCast
{
    class APIException : Exception
    {
        private HttpStatusCode code;
        private string status;

        /// <summary>
        /// APIExceptions are thrown when a 400 or 500 series error is 
        /// encountered in an API call.
        /// </summary>
        public APIException() { }

        /// <summary>
        /// APIExceptions are thrown when a 400 or 500 series error is 
        /// encountered in an API call.
        /// </summary>
        /// <param name="message">The error message.</param>
        public APIException(string message) : base(message) { }

        /// <summary>
        /// APIExceptions are thrown when a 400 or 500 series error is 
        /// encountered in an API call.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="inner">Inner exception.</param>
        public APIException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// The HTTP status code of the error response.
        /// </summary>
        public HttpStatusCode StatusCode
        {
            get { return this.code; }
            set { this.code = value; }
        }

        /// <summary>
        /// The status description of the error response.
        /// 
        /// </summary>
        public string StatusDescription
        {
            get { return this.status; }
            set { this.status = value; }
        }
    }
}
