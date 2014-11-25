//----------------------------------------------------------------
// ContactIDCollection.cs
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
    /// A representation for the 'add contacts to list' API call.
    /// </summary>
    [XmlRoot(ElementName = "contacts")]
    public class ContactIDCollection
    {
        /// <summary>
        /// A collection of contact IDs to place in the request.
        /// </summary>
        [XmlElement("contact")]
        public List<Contacts> contactIds = new List<ContactIDCollection.Contacts>();

        /// <summary>
        /// Struct to enforce the XML request schema.
        /// </summary>
        public struct Contacts
        {
            /// <summary>
            /// A contact ID.
            /// </summary>
            [XmlAttribute("id")]
            public int ID;
        }

        /// <summary>
        /// Add a contact ID to the request collection.
        /// </summary>
        /// <param name="ID">The contact ID.</param>
        public void addContactID(int ID) {
            Contacts c = new Contacts();
            c.ID = ID;
            this.contactIds.Add(c);
        }
    }
}
