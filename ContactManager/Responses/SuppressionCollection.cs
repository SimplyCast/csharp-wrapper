//----------------------------------------------------------------
// SuppressionCollection.cs
// Copyright SimplyCast 2014
// This projected is licensed under the terms of the MIT license.
//  (see the attached LICENSE.txt).
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SimplyCast.ContactManager.Responses
{
    /// <summary>
    /// A collection of suppression entries.
    /// </summary>
    [XmlRoot(ElementName = "suppressionEntries")]
    public class SuppressionCollection
    {
        private SuppressionEntry[] suppressionEntries;

        /// <summary>
        /// A collection of suppression entries.
        /// </summary>
        [XmlElement("suppressionEntry")]        
        public SuppressionEntry[] SuppressionEntries
        {
            get { return this.suppressionEntries; }
            set { this.suppressionEntries = value; }
        }
    }
}
