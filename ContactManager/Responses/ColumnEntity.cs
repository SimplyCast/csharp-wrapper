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
using SimplyCast.Common.Responses;

namespace SimplyCast.ContactManager.Responses
{
    /// <summary>
    /// This class represents a column entity representation and fully supports
    /// XML serialization. A column entity represents a column in the Contact 
    /// Manager and its properties. Columns exist and have values for all 
    /// contacts. It doesn't represent any values stored; the combination of a
    /// column and a value is a field (and can be retrieved from a contact).
    /// </summary>
    [XmlRoot(ElementName = "column")]
    public class ColumnEntity
    {
        #region Private Members
        private string id;
        private ColumnType type;
        private int userDefined;
        private int visible;
        private int editable;
        private string name;
        private string[] mergeTags;
        private RelationLink[] links;
        #endregion

        /// <summary>
        /// The unique internal identifier of the column. 
        /// </summary>
        [XmlAttribute("id")]
        public string ID 
        {
            get { return this.id; }
            set { this.id = value; }
        }

        /// <summary>
        /// The data type of the column (string, date, etc).
        /// </summary>
        [XmlAttribute("type")]
        public ColumnType Type 
        {
            get { return this.type; }
            set { this.type = value; }
        }

        /// <summary>
        /// A flag indicating if the column is user-defined or if it is a 
        /// system column. 
        /// </summary>
        public bool IsUserDefined 
        {
            get { return this.userDefined == 0 ? false : true; }
        }

        /// <summary>
        /// An accessor for the raw userDefined value. Used only for XML 
        /// serialization.
        /// </summary>
        [XmlAttribute("userDefined")]
        public int UserDefined
        {
            get { throw new Exception("Not Implemented"); }
            set { this.userDefined = value; }
        }

        /// <summary>
        /// A flag indicating if the column is visible in the user interface.
        /// </summary>
        public bool IsVisible
        {
            get { return this.visible == 0 ? false : true; }
        }

        /// <summary>
        /// An accessor for the raw visible value. Used only for XML
        /// serialization.
        /// </summary>
        [XmlAttribute("visible")]
        public int Visible
        {
            get { throw new Exception("Not Implemented"); }
            set { this.visible = value; }
        }

        /// <summary>
        /// A flag indicating if the column is editable.
        /// </summary>
        public bool IsEditable
        {
            get { return this.editable == 0 ? false : true; }
        }

        /// <summary>
        /// An accessor for the raw editable value. Used only for XML
        /// serialization.
        /// </summary>
        [XmlAttribute("editable")]
        public int Editable
        {
            get { throw new Exception("Not Implemented"); }
            set { this.editable = value; }
        }

        /// <summary>
        /// The name of the column (displayed in the user interface).
        /// </summary>
        [XmlElement("name")]
        public string Name 
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// A collection of relation links containing at least a 
        /// self-reference.
        /// </summary>
        [XmlArray("links")]
        [XmlArrayItem("link")]
        public RelationLink[] Links
        {
            get { return this.links; }
            set { this.links = value; }
        }

        /// <summary>
        /// An array of merge tags. Merge tags are used in application content
        /// to dynamically insert contact information at the time of send.
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
            Date = 1,

            /// <summary>
            /// This column type specifically stores Integers.
            /// </summary>
            [XmlEnum("int")]
            Integer = 2,

            /// <summary>
            /// This column type specifically stores Number.
            /// </summary>
            [XmlEnum("number")]
            Number = 3,

            /// <summary>
            /// This column type specifically stores bool.
            /// </summary>
            [XmlEnum("boolean")]
            Boolean = 4,

            /// <summary>
            /// This column type specifically stores text.
            /// </summary>
            [XmlEnum("text")]
            Text = 5
        }
    }
}
