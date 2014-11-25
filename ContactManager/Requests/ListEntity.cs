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

namespace SimplyCast.ContactManager.Requests
{
    /// <summary>
    /// This class contains list-specific request information that can be 
    /// serialized.
    /// </summary>
    [XmlRoot(ElementName = "list")]
    public class ListEntity
    {
        private string name;
        private int id;

        /// <summary>
        /// The list ID, specified in requests that assign a contact to a list.
        /// </summary>
        [XmlAttribute("id")]
        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        /// <summary>
        /// A contact list name, used when creating or renaming a list.
        /// </summary>
        [XmlElement("name")]
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
    }
}
