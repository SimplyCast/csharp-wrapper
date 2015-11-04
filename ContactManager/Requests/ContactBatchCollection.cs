//----------------------------------------------------------------
// ContactBatchCollection.cs
// Copyright SimplyCast 2015
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
    /// A batch contact upload request entity representation.
    /// </summary>
    [XmlRoot(ElementName = "requests")]
    public class ContactBatchCollection
    {
        private ContactEntity[] contacts;

        /// <summary>
        /// An array of contact entries to create.
        /// </summary>
        [XmlElement("contact")]
        public ContactEntity[] Contacts
        {
            get { return this.contacts; }
            set { this.contacts = value; }
        }
    }
}
