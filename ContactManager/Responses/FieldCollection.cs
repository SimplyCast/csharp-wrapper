//----------------------------------------------------------------
// FieldCollection.cs
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
using System.Collections;

namespace SimplyCast.ContactManager.Responses
{
    /// <summary>
    /// This class contains a collection of field entities and fully supports 
    /// XML serialization.
    /// </summary>
    [XmlRoot(ElementName = "fields")]
    public class FieldCollection : GenericCollection
    {
        private FieldEntity[] fields;

        /// <summary>
        /// A collection of field entities.
        /// </summary>
        [XmlElement("field")]
        public FieldEntity[] Fields
        {
            get { return this.fields; }
            set { this.fields = value; }
        }
    }
}
