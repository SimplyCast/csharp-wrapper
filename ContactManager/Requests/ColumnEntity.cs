//----------------------------------------------------------------
// ColumnEntity.cs
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
    /// A column request entity representation.
    /// </summary>
    [XmlRoot(ElementName = "column")]
    public class ColumnEntity
    {
        private string name;
        private ColumnType type;
        private string[] mergeTags;

        /// <summary>
        /// A column name.
        /// </summary>
        [XmlElement("name")]
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// The column type.
        /// </summary>
        [XmlAttribute("type")]
        public ColumnType Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        /// <summary>
        /// A collection of merge tags.
        /// </summary>
        [XmlArray("mergeTags")]
        [XmlArrayItem("mergeTag")]
        public string[] MergeTags
        {
            get { return this.mergeTags; }
            set { this.mergeTags = value; }
        }

        /// <summary>
        /// An enum of column data types.
        /// </summary>
        public enum ColumnType
        {
            /// <summary>
            /// This column type stores general data (string or numeric).
            /// </summary>
            [XmlEnum("string")]
            String = 0,

            /// <summary>
            /// This column type specifically stores dates/times.
            /// </summary>
            [XmlEnum("date")]
            Date = 1
        }
    }
}
