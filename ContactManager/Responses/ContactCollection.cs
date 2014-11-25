//----------------------------------------------------------------
// ContactCollection.cs
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
    /// This class contains a collection of contact entity representations
    /// and fully supports XML serialization.
    /// </summary>
    [XmlRoot(ElementName = "contacts")]
    public class ContactCollection : GenericCollection
    {
        private ContactEntity[] contacts;

        /// <summary>
        /// A collection of contact entity representations.
        /// </summary>
        [XmlElement("contact")]
        public ContactEntity[] Contacts
        {
            get { return this.contacts; }
            set { this.contacts = value; }
        }
    }
}
