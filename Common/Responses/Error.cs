//----------------------------------------------------------------
// Error.cs
// Copyright SimplyCast 2014
// This projected is licensed under the terms of the MIT license.
//  (see the attached LICENSE.txt).
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SimplyCast.Common.Responses
{
    /// <summary>
    /// Helper class for deserializing XML errors.
    /// </summary>
    [XmlRoot("error")]
    public class Error
    {
        private string error;

        /// <summary>
        /// The text summary of the error encountered.
        /// </summary>
        [XmlText()]
        public string Message
        {
            get { return this.error; }
            set { this.error = value; }
        }
    }
}
