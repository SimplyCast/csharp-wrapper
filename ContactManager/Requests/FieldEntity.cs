﻿//----------------------------------------------------------------
// FieldEntity.cs
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
    /// This class contains request information for creating or modifying a 
    /// contact field.
    /// </summary>
    [XmlRoot(ElementName = "field")]
    public class FieldEntity
    {
        private string id;
        private string value;

        /// <summary>
        /// The column identifier of the field to set.
        /// </summary>
        [XmlAttribute("id")]
        public string ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        /// <summary>
        /// The value of the field.
        /// </summary>
        [XmlText()]
        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
    }
}
