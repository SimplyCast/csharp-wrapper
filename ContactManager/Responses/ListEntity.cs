//----------------------------------------------------------------
// ListEntity.cs
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
    /// This class contains a contact list entity representation and fully 
    /// supports XML serialization.
    /// </summary>
    [XmlRoot(ElementName = "list")]
    public class ListEntity
    {
        #region Private Members
        private int id;
        private int size;
        private DateTime created;
        private DateTime lastAdded;
        private DateTime lastDeleted;
        private string name;
        private RelationLink[] links;
        #endregion

        /// <summary>
        /// The ID of the contact list; can be used to directly access the 
        /// list resource.
        /// </summary>
        [XmlAttribute("id")]
        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        /// <summary>
        /// The number of contacts that are in the list
        /// </summary>
        [XmlAttribute("size")]
        public int Size
        {
            get { return this.size; }
            set { this.size = value; }
        }

        /// <summary>
        /// The date and time that the list was created.
        /// </summary>
        [XmlAttribute("created", DataType = "dateTime")]
        public DateTime Created
        {
            get { return this.created; }
            set { this.created = value; }
        }

        /// <summary>
        /// The date of last contact entry into the list.
        /// </summary>
        [XmlAttribute("lastAdded", DataType = "dateTime")]
        public DateTime LastAdded
        {
            get { return this.lastAdded; }
            set { this.lastAdded = value; }
        }

        /// <summary>
        /// The date of last contact deletion from the list.
        /// </summary>
        [XmlAttribute("lastDeleted", DataType = "dateTime")]
        public DateTime LastDeleted
        {
            get { return this.lastDeleted; }
            set { this.lastDeleted = value; }
        }

        /// <summary>
        /// The name of the list.
        /// </summary>
        [XmlElement("name")]
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// A collection of relation links. Will contain a link to the contact list.
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