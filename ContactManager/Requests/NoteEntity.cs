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

namespace SimplyCast.ContactManager.Requests
{
    /// <summary>
    /// An XML request entity.
    /// </summary>
    [XmlRoot(ElementName = "note")]
    public class NoteEntity
    {
        private string title;
        private string description;

        /// <summary>
        /// The title of the CRM note.
        /// </summary>
        [XmlElement("title")]
        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        /// <summary>
        /// The description attached to the note.
        /// </summary>
        [XmlElement("description")]
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }
    }
}
