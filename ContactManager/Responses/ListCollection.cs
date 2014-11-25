//----------------------------------------------------------------
// ListCollection.cs
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
    /// This class contains a collection of contact list entities and fully 
    /// supports XML serialization.
    /// </summary>
    [XmlRoot(ElementName = "lists")]
    public class ListCollection : GenericCollection
    {
        private ListEntity[] lists;

        /// <summary>
        /// An collection of contact list entities.
        /// </summary>
        [XmlElement("list")]
        public ListEntity[] Lists
        {
            get { return this.lists; }
            set { this.lists = value; }
        }
    }
}
