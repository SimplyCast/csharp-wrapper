//----------------------------------------------------------------
// ConnectionEntity.cs
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

namespace SimplyCast.SimplyCast360.Responses
{
    /// <summary>
    /// An API connection endpoint of a 360 project.
    /// </summary>
    [XmlRoot("connection")]
    public class ConnectionEntity
    {
        #region Private members
        private int id;
        private string name;
        private ConnectionType type;
        private int active;
        private RelationLink[] links;
        #endregion

        /// <summary>
        /// The unique ID of the connection.
        /// </summary>
        [XmlAttribute("id")]
        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        /// <summary>
        /// The name of the connection.
        /// </summary>
        [XmlElement("name")]
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// The connection type (inbound connection or outbound connection).
        /// </summary>
        [XmlAttribute("type")]
        public ConnectionType Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        /// <summary>
        /// XML deserialization property. Use IsActive to retrieve the value.
        /// </summary>
        [XmlAttribute("active")]
        public int Active
        {
            set { this.active = value; }
        }

        /// <summary>
        /// Indicates the status of the connection. Inactive connections 
        /// will not do anything.
        /// </summary>
        public bool IsActive
        {
            get { return (this.active == 1) ? true : false; }
        }

        /// <summary>
        /// A collection of relation links for the connection.
        /// </summary>
        [XmlArray("links")]
        [XmlArrayItem("link")]
        public RelationLink[] Links
        {
            get { return this.links; }
            set { this.links = value; }
        }

        /// <summary>
        /// The types of API connection endpoints that are available for 360
        /// projects.
        /// </summary>
        public enum ConnectionType
        {
            /// <summary>
            /// An inbound connection type.
            /// </summary>
            [XmlEnum("in")]
            Inbound,

            /// <summary>
            /// An outbound connection.
            /// </summary>
            [XmlEnum("out")]
            Outbound
        }
    }
}
