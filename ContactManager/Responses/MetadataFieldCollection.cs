//----------------------------------------------------------------
// MetadataFieldCollection.cs
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
    /// This class contains a collection of metadata column entity 
    /// representations and supports XML serialization.
    /// </summary>
    [XmlRoot(ElementName = "metadataFields")]
    public class MetadataFieldCollection : GenericCollection
    {
        private MetadataFieldEntity[] metadataFields;

        /// <summary>
        /// A collection of metadata column entities.
        /// </summary>
        [XmlElement("metadataField")]
        public MetadataFieldEntity[] MetadataFields
        {
            get { return this.metadataFields; }
            set { this.metadataFields = value; }
        }
    }
}
