//----------------------------------------------------------------
// ProjectEntity.cs
// Copyright SimplyCast 2014
// This projected is licensed under the terms of the MIT license.
//  (see the attached LICENSE.txt).
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimplyCast.Common.Responses;
using System.Xml.Serialization;

namespace SimplyCast.SimplyCast360.Responses
{
    /// <summary>
    /// This class represents a 360 project.
    /// </summary>
    [XmlRoot(ElementName = "project")]
    public class ProjectEntity
    {
        #region Private Members
        private int id;
        private string name;
        private int active;
        private ConnectionEntity[] connections;
        private RelationLink[] links;
        #endregion

        /// <summary>
        /// The ID of the 360 project.
        /// </summary>
        [XmlAttribute("id")]
        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        /// <summary>
        /// The name of the 360 project.
        /// </summary>
        [XmlElement("name")]
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// XML serialization property for the active flag. Use IsActive to 
        /// check the value.
        /// </summary>
        [XmlAttribute("active")]
        public int Active {
            set { this.active = value; }
        }

        /// <summary>
        /// Returns the active state of the 360 project.
        /// </summary>
        public bool IsActive
        {
            get { return (this.active == 1) ? true : false; }
        }

        /// <summary>
        /// Return all the API connection endpoints for the requested 360
        /// project. 
        /// </summary>
        [XmlArray("connections")]
        [XmlArrayItem("connection")]
        public ConnectionEntity[] Connections
        {
            get { return this.connections; }
            set { this.connections = value; }
        }

        /// <summary>
        /// A collection of relation links for the project.
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
