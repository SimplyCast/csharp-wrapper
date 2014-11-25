//----------------------------------------------------------------
// MetadataColumnEntity.cs
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
    /// A request representation for metadata columns.
    /// </summary>
    [XmlRoot(ElementName = "metadataColumn")]
    public class MetadataColumnEntity
    {
        private string name;
        private string type;

        /// <summary>
        /// The metadata column name.
        /// </summary>
        [XmlElement("name")]
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// The metadata column type.
        /// </summary>
        [XmlAttribute("type")]
        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
        }
    }
}
