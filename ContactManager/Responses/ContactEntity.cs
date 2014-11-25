//----------------------------------------------------------------
// ContactEntity.cs
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
    /// This class represents a contact entity representation and fully 
    /// supports XML serialization. Field information (email, name, etc.)
    /// is accessible via the Fields property.
    /// </summary>
    [XmlRoot(ElementName = "contact")]
    public class ContactEntity
    {
        #region Private Members
        private int id;
        private DateTime created;
        private DateTime modified;
        private FieldEntity[] fields;
        private ListEntity[] lists;
        private RelationLink[] links;
        #endregion

        /// <summary>
        /// The internal identifier of the contact. Can be used to reference
        /// the contact directly.
        /// </summary>
        [XmlAttribute("id")]
        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        /// <summary>
        /// The date that the contact entry was created.
        /// </summary>
        [XmlAttribute("created", DataType = "dateTime")]
        public DateTime Created
        {
            get { return this.created; }
            set { this.created = value; }
        }

        /// <summary>
        /// The date that the contact entry was last modified.
        /// </summary>
        [XmlAttribute("modified", DataType = "dateTime")]
        public DateTime Modified
        {
            get { return this.modified; }
            set { this.modified = value; }
        }

        /// <summary>
        /// An array of field entities. Each field entity will contain
        /// 
        /// a single field entry (email, for example).
        /// </summary>
        [XmlArray("fields")]
        [XmlArrayItem("field")]
        public FieldEntity[] Fields
        {
            get { return this.fields; }
            set { this.fields = value; }
        }

        /// <summary>
        /// An array of list entities. Each list entity represents a list that
        /// the contact appears on.
        /// </summary>
        [XmlArray("lists")]
        [XmlArrayItem("list")]
        public ListEntity[] Lists
        {
            get { return this.lists; }
            set { this.lists = value; }
        }

        /// <summary>
        /// A collection of relation links. Will contain (at least) a link to
        /// the contact resource that this contact exists at.
        /// </summary>
        [XmlArray("links")]
        [XmlArrayItem("link")]
        public RelationLink[] Links {
            get { return this.links; }
            set { this.links = value; }
        }

        /// <summary>
        /// This method is provided as a helper for extracting a field by the 
        /// column name. Because column names aren't guaranteed to be unique, 
        /// this method returns a list of field entities (you'll probably only
        /// care about the first).
        /// </summary>
        /// <param name="name">The name of the field(s) to retrieve.</param>
        /// <returns>A list of the matching field entities. If you want the 
        /// value, access the Value property.</returns>
        public List<FieldEntity> GetFieldsByName(string name)
        {
            List<FieldEntity> returnFields = new List<FieldEntity>();

            for (int i = 0; i < this.fields.Length; i++)
            {
                if (this.fields[i].Name == name)
                {
                    returnFields.Add(this.fields[i]);
                }
            }

            return returnFields;
        }
    }
}
