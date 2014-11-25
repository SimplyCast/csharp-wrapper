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

namespace SimplyCast.SimplyCast360.Responses
{
    /// <summary>
    /// A representation of a 360 contact entry.
    /// </summary>
    [XmlRoot(ElementName = "contact")]
    public class ContactEntity
    {
        private int id;
        private int contactId;
        private int listId;
        private RelationLink[] links;

        /// <summary>
        /// The 360 handle ID for the contact instance.
        /// </summary>
        [XmlAttribute("id")]
        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        /// <summary>
        /// The contact's ID in the contact manager.
        /// </summary>
        [XmlElement("row")]
        public int ContactID
        {
            get { return this.contactId; }
            set { this.contactId = value; }
        }

        /// <summary>
        /// The ID of a list that the contact belongs to.
        /// </summary>
        [XmlElement("list")]
        public int ListID
        {
            get { return this.listId; }
            set { this.listId = value; }
        }

        /// <summary>
        /// A collection of relation links for the project.
        /// </summary>
        [XmlArray("links")]
        [XmlArrayItem("link")]
        public RelationLink[] Links
        {
            get { return this.links; }
            set { this.links = value; }
        }
    }
}
