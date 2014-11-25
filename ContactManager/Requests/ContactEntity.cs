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

namespace SimplyCast.ContactManager.Requests
{
    /// <summary>
    /// This class contains contact request information.
    /// </summary>
    [XmlRoot(ElementName = "contact")]
    public class ContactEntity
    {
        private string id;
        private FieldEntity[] fields;
        private ListEntity[] listIds;

        /// <summary>
        /// The ID attribute; only used when adding a contact to a list.
        /// </summary>
        [XmlAttribute("id")]
        public string ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        /// <summary>
        /// A collection of fields to set when creating or updating.
        /// </summary>
        [XmlArray("fields")]
        [XmlArrayItem("field")]
        public FieldEntity[] Fields
        {
            get { return this.fields; }
            set { this.fields = value; }
        }

        /// <summary>
        /// A collection of contact list IDs that is used when assigning a 
        /// contact to a list.
        /// </summary>
        [XmlArray("lists")]
        [XmlArrayItem("list")]
        public ListEntity[] ListIds
        {
            get { return this.listIds; }
            set { this.listIds = value; }
        }
    }
}
