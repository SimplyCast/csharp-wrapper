//----------------------------------------------------------------
// ContactBatchResultCollection.cs
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

namespace SimplyCast.ContactManager.Responses
{
    /// <summary>
    /// A result object representation of a batch operation API call.
    /// </summary>
    [XmlRoot(ElementName = "results")]
    public class ContactBatchResultCollection
    {
        private ContactBatchResult[] results;

        /// <summary>
        /// An array of contact batch result objects.
        /// </summary>
        [XmlElement("result")]
        public ContactBatchResult[] Results
        {
            get { return this.results; }
            set { this.results = value; }
        }
    }
}
