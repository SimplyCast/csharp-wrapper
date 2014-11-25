//----------------------------------------------------------------
// MetadataFieldEntity.cs
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
    /// An XML request entity.
    /// </summary>
    [XmlRoot(ElementName = "metadataField")]
    public class MetadataFieldEntity
    {
        private string[] values;

        /// <summary>
        /// A metadata field value to modify.
        /// </summary>
        [XmlArray("values")]
        [XmlArrayItem("value")]
        public string[] Values
        {
            get { return this.values; }
            set { this.values = value; }
        }
    }
}
