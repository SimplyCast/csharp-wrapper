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

namespace SimplyCast.SimplyCast360.Requests
{
    /// <summary>
    /// A contact entity request for pushing to an inbound 360 connection.
    /// </summary>
    [XmlRoot("row")]
    public class ContactEntity
    {
        private int id;
        private int list;
        private int row;
        private RelationLink[] links;
        
        /// <summary>
        /// The ID of the contact.
        /// </summary>
        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        /// <summary>
        /// The list that contains the contact to push.
        /// </summary>
        [XmlElement("list")]
        public int List
        {
            get { return this.list; }
            set { this.list = value; }
        }

        /// <summary>
        /// The contact to push.
        /// </summary>
        [XmlElement("row")]
        public int Row
        {
            get { return this.row; }
            set { this.row = value; }
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
    }
}
