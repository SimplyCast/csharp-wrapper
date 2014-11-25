//----------------------------------------------------------------
// ColumnCollection.cs
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
    /// This class contains a collection of column entity representations and
    /// supports XML serialization.
    /// </summary>
    [XmlRoot(ElementName = "columns")]
    public class ColumnCollection : GenericCollection
    {
        private ColumnEntity[] columns;

        /// <summary>
        /// A collection of column entity representations.
        /// </summary>
        [XmlElement("column")]
        public ColumnEntity[] Columns 
        {
            get { return this.columns; }
            set { this.columns = value; }
        }

        /// <summary>
        /// Get a column by its name. If there is more than one column
        /// with the same name, only the first one found will be returned.
        /// </summary>
        /// <param name="name">The name of the column to find.</param>
        /// <returns>The column entity that matched, or null if the column
        /// is not found.</returns>
        public ColumnEntity GetByName(string name)
        {
            foreach (ColumnEntity e in columns)
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
