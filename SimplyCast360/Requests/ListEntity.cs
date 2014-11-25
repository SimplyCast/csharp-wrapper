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

namespace SimplyCast.SimplyCast360.Requests
{
    /// <summary>
    /// A list request entity for pushing to an inbound 360 connection
    /// endpoint.
    /// </summary>
    [XmlRoot(ElementName = "list")]
    public class ListEntity
    {
        private int list;

        /// <summary>
        /// The list to push to a 360 inbound connection endpoint.
        /// </summary>
        [XmlElement("list")]
        public int List
        {
            get { return this.list; }
            set { this.list = value; }
        }
    }
}
