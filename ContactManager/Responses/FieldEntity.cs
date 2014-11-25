//----------------------------------------------------------------
// FieldEntity.cs
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
    /// This class represents a field entity. A field entity represents a value
    /// on a column in the Contact Manager, storing information about a
    /// particular contact. For convenience, many of the column attributes are
    /// provided in the entity representation.
    /// </summary>
    [XmlRoot(ElementName = "field")]
    public class FieldEntity
    {
        #region Private Members
        private string id;
        private string value;
        private string name;
        private int userDefined;
        private int visible;
        private int editable;
        private int extended;
        #endregion

        /// <summary>
        /// The unique identifier of the column that the field is associated 
        /// with.
        /// </summary>
        [XmlAttribute("id")]
        public string ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        /// <summary>
        /// The value on the field (an email address or name, for example).
        /// </summary>
        [XmlElement("value")]
        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        /// <summary>
        /// The name of the column assoctiated with the field.
        /// </summary>
        [XmlElement("name")]
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
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
        /// A flag indicating that the field is considered an 'extended' field.
        /// Extended fields are accessible, but generally contain system 
        /// information that may not be used. Extended fields can be optionally
        /// disabled in requests as a means to reduce overhead.
        /// </summary>
        [XmlAttribute("extended")]
        public int Extended
        {
            get { throw new Exception("Not Implemented"); }
            set { this.extended = value; }
        }

        /// <summary>
        /// A flag indicating that the field is considered an 'extended' field.
        /// Extended fields are accessible, but generally contain system 
        /// information that may not be used. Extended fields can be optionally
        /// disabled in requests as a means to reduce overhead.
        /// </summary>
        public bool IsExtended
        {
            get { return this.extended == 0 ? false : true; }
        }
    }
}
