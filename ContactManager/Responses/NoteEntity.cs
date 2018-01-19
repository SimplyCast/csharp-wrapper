//----------------------------------------------------------------
// NoteEntity.cs
// Copyright SimplyCast 2018
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
    /// This class contains a CRM note entity representation and fully 
    /// supports XML serialization.
    /// </summary>
    [XmlRoot(ElementName = "note")]
    public class NoteEntity
    {
        #region Private Members
        private int id;
        private int contactId;
        private DateTime created;
        private DateTime modified;
        private string title;
        private string description;
        private RelationLink[] links;
        #endregion

        /// <summary>
        /// The ID of the note; can be used to directly access the 
        /// note resource.
        /// </summary>
        [XmlAttribute("id")]
        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        /// <summary>
        /// The ID of the contact the note belongs to.
        /// </summary>
        [XmlAttribute("contactId")]
        public int ContactID
        {
            get { return this.contactId; }
            set { this.contactId = value; }
        }

        /// <summary>
        /// The date and time that the note was created.
        /// </summary>
        [XmlAttribute("created", DataType = "dateTime")]
        public DateTime Created
        {
            get { return this.created; }
            set { this.created = value; }
        }

        /// <summary>
        /// The date and time of the last modification.
        /// </summary>
        [XmlAttribute("modified", DataType = "dateTime")]
        public DateTime Modified
        {
            get { return this.modified; }
            set { this.modified = value; }
        }

        /// <summary>
        /// The title of the note.
        /// </summary>
        [XmlElement("title")]
        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        /// <summary>
        /// The name of the note.
        /// </summary>
        [XmlElement("description")]
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        /// <summary>
        /// A collection of relation links. Will contain a link to the note resource.
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