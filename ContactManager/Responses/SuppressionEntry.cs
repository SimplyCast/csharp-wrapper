//----------------------------------------------------------------
// SuppressionEntry.cs
// Copyright SimplyCast 2014
// This projected is licensed under the terms of the MIT license.
//  (see the attached LICENSE.txt).
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SimplyCast.ContactManager.Responses
{
    /// <summary>
    /// A representation of a suppression list entry.
    /// </summary>
    [XmlRoot(ElementName = "suppressionEntry")]
    public class SuppressionEntry
    {
        private string value;
        private DateTime added;
        private int hardBounced;

        /// <summary>
        /// The suppressed contact value for the entry (an email address, phone
        /// number, etc.)
        /// </summary>
        [XmlText()]
        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        /// <summary>
        /// The date and time that the value was added.
        /// </summary>
        [XmlAttribute("added")]
        public DateTime Added
        {
            get { return this.added; }
            set { this.added = value; }
        }

        /// <summary>
        /// If the entry is an email address, 1 indicates that the value was
        /// a hard bounce.
        /// </summary>
        [XmlAttribute("hardBounced")]
        public int HardBounced
        {
            get { return this.hardBounced; }
            set { this.hardBounced = value; }
        }
    }
}
