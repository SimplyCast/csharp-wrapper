//----------------------------------------------------------------
// MetadataColumnEntity.cs
// Copyright SimplyCast 2014
// This projected is licensed under the terms of the MIT license.
//  (see the attached LICENSE.txt).
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using SimplyCast.Common.Responses;
using System.Xml.Schema;

namespace SimplyCast.ContactManager.Responses
{
    /// <summary>
    /// This class represents a metadata entity representation and supports XML
    /// serialization. Metadata entries are a more flexible way of storing 
    /// ancillary contact information such as tagging and lead scoring, 
    /// especially when not requiring broad queries or when storing conditional
    /// data (things that may or may not exist, depending on context).
    /// </summary>
    [XmlRoot(ElementName = "metadataColumn")]
    public class MetadataColumnEntity : IXmlSerializable
    {
        #region Private Members
        private string id;
        private string name;
        private string type;
        private bool userDefined;
        private bool visible;
        private bool editable;

        private RelationLink[] links;
        #endregion

        /// <summary>
        /// The ID of metadata column. Can be used to directly access the 
        /// metadata column representation.
        /// </summary>
        public string ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        /// <summary>
        /// The name of the metadata field.
        /// </summary>
        public string Name
        {
            get { return this.name; }
        }

        /// <summary>
        /// The metadata field type.
        /// </summary>
        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        /// <summary>
        /// A flag indicating if the column is user-defined or if it is a 
        /// system column. 
        /// </summary>
        public bool IsUserDefined
        {
            get { return this.userDefined; }
        }

        /// <summary>
        /// A flag indicating if the column is visible in the user interface.
        /// </summary>
        public bool IsVisible
        {
            get { return this.visible; }
        }

        /// <summary>
        /// A flag indicating if the column is editable.
        /// </summary>
        public bool IsEditable
        {
            get { return this.editable; }
        }

        /// <summary>
        /// A collection of relation links. Will contain a link to the metadata
        /// column resource.
        /// </summary>
        [XmlArray("links")]
        [XmlArrayItem("link")]
        public RelationLink[] Links
        {
            get { return this.links; }
            set { this.links = value; }
        }

        /// <summary>
        /// Unused method of IXmlSerializable.
        /// </summary>
        /// <returns>null</returns>
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Deserialized an XML response into an instance of this object.
        /// </summary>
        /// <param name="reader">An XmlReader object.</param>
        public void ReadXml(XmlReader reader)
        {
            reader.MoveToFirstAttribute();
            do
            {
                switch (reader.Name)
                {
                    case "id":
                        this.id = reader.ReadContentAsString();
                        break;
                    case "type":
                        this.type = reader.ReadContentAsString();
                        break;
                    case "userDefined":
                        this.userDefined = reader.ReadContentAsBoolean();
                        break;
                    case "visible":
                        this.visible = reader.ReadContentAsBoolean();
                        break;
                    case "editable":
                        this.editable = reader.ReadContentAsBoolean();
                        break;
                }
            } while (reader.MoveToNextAttribute());

            List<RelationLink> links = new List<RelationLink>();
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "name":
                            this.name = reader.ReadString();
                            break;
                        case "link":
                        case "links":
                            if (reader.Name == "links")
                            {
                                reader.ReadToDescendant("link");
                            }
                            RelationLink link = new RelationLink();

                            reader.MoveToFirstAttribute();
                            link.Rel = "self";

                            while (reader.MoveToNextAttribute())
                            {
                                if (reader.Name == "href")
                                {
                                    link.URL = reader.ReadContentAsString();
                                }
                                else if (reader.Name == "rel")
                                {
                                    link.Rel = reader.ReadContentAsString();
                                }
                            }

                            links.Add(link);

                            break;
                    }
                }
                else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "metadataColumn")
                {
                    reader.Read();
                    break;
                }
            }

            this.links = new RelationLink[] { };
        }

        /// <summary>
        /// This object is never intended to be serialized, so it is not 
        /// implemented.
        /// </summary>
        /// <param name="writer">An XmlWriter.</param>
        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Enum representing the types of metadata columns available.
        /// </summary>
        public enum ColumnType
        {
            /// <summary>
            /// This column type holds a single string value.
            /// </summary>
            [XmlEnum("single string")]
            SingleString = 0,

            /// <summary>
            /// This column type holds a single numeric value.
            /// </summary>
            [XmlEnum("single number")]
            SingleNumber = 1,

            /// <summary>
            /// This column type holds multiple string values.
            /// </summary>
            [XmlEnum("multi string")]
            MultiString = 2,

            /// <summary>
            /// This column type holds multiple numeric values.
            /// </summary>
            [XmlEnum("multi number")]
            MultiNumber = 3,

            /// <summary>
            /// This column type acts as a numeric adder. For example, if the 
            /// value of the column is currently 5 and you 'set' the value to
            /// 4, the value will become 9.
            /// </summary>
            [XmlEnum("sum number")]
            SumNumber = 4
        }
    }
}
