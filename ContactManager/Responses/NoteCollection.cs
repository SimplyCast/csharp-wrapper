//----------------------------------------------------------------
// NoteCollection.cs
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
    /// This class contains a collection of CRM note entities and fully 
    /// supports XML serialization.
    /// </summary>
    [XmlRoot(ElementName = "notes")]
    public class NoteCollection : GenericCollection
    {
        private NoteEntity[] notes;

        /// <summary>
        /// An collection of CRM note entities.
        /// </summary>
        [XmlElement("note")]
        public NoteEntity[] Notes
        {
            get { return this.notes; }
            set { this.notes = value; }
        }
    }
}
