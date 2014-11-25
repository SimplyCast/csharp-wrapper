//----------------------------------------------------------------
// SuppressionEntries.cs
// Copyright SimplyCast 2014
// This projected is licensed under the terms of the MIT license.
//  (see the attached LICENSE.txt).
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SimplyCast.ContactManager.Requests
{
    /// <summary>
    /// A suppression list request entity.
    /// </summary>
    [XmlRoot(ElementName = "suppressionEntries")]
    public class SuppressionEntries
    {
        private string[] suppressionEntries;

        /// <summary>
        /// An array of entries to add to a suppression list.
        /// </summary>
        [XmlElement("suppressionEntry")]
        public string[] Values {
            get { return this.suppressionEntries; }
            set { this.suppressionEntries = value; }
        }
    }
}
