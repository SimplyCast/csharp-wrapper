//----------------------------------------------------------------
// MetadataColumnCollection.cs
// Copyright SimplyCast 2014
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
    /// This class contains a collection of metadata column entity 
    /// representations and supports XML serialization.
    /// </summary>
    [XmlRoot(ElementName = "metadataColumns")]
    public class MetadataColumnCollection : GenericCollection
    {
        private MetadataColumnEntity[] metadataColumns;

        /// <summary>
        /// A collection of metadata column entities.
        /// </summary>
        [XmlElement("metadataColumn")]
        public MetadataColumnEntity[] MetadataColumns
        {
            get { return this.metadataColumns; }
            set { this.metadataColumns = value; }
        }

        /// <summary>
        /// Get a column by its name. If there is more than one column
        /// with the same name, only the first one found will be returned.
        /// </summary>
        /// <param name="name">The name of the column to find.</param>
        /// <returns>The column entity that matched, or null if the column
        /// is not found.</returns>
        public MetadataColumnEntity GetByName(string name)
        {
            foreach (MetadataColumnEntity e in metadataColumns)
            {
                if (e.Name == name)
                {
                    return e;
                }
            }

            return null;
        }
    }
}
