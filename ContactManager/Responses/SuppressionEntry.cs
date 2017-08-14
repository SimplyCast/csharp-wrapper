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
using System.Globalization;

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
        private DateTime hardBounced;
        private CultureInfo culture = CultureInfo.InvariantCulture;

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
        [XmlIgnore]
        public DateTime Added
        {
            get { return this.added; }
            set { this.added = value; }
        }

        /// <summary>
        /// The date and time that the value hardbounced 
        /// if the valued is a email is a email.
        /// </summary>
        [XmlIgnore]
        public DateTime HardBounced
        {
            get { return this.hardBounced; }
            set { this.hardBounced = value; }
        }

        /// <summary>
        /// Returns the string repensentation of added
        /// </summary> 
        [XmlAttribute("added")]
        public string AddedString
        {
            get { return this.added.ToString("yyyy-MM-dd'T'HH:mm:sszzz", culture); }
            set { this.added = convertString(value); }
        }

        /// <summary>
        /// Returns the String form of hardBounced.
        /// </summary>
        [XmlAttribute("hardBounced")]
        public string HardBouncedString
        {
            get { return this.hardBounced.ToString("yyyy-MM-dd'T'HH:mm:sszzz", culture); }
            set { this.hardBounced = convertString(value); }
        }

        private DateTime convertString(string dateString)
        {
            DateTime result;
            if (DateTime.TryParseExact(dateString, "yyyy-MM-dd'T'HH:mm:sszzz", culture, DateTimeStyles.None, out result)) {
                return result;
            }
            else
            {
                return DateTime.MinValue;
            }

        }
    }
}
