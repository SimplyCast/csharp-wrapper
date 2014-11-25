//----------------------------------------------------------------
// RelationLink.cs
// Copyright SimplyCast 2014
// This projected is licensed under the terms of the MIT license.
//  (see the attached LICENSE.txt).
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SimplyCast.Common.Responses
{
    /// <summary>
    /// This class represents a relation link. Relation links assist in 
    /// navigating through API resources.
    /// </summary>
    [XmlRoot(ElementName = "link")]
    public class RelationLink
    {
        #region Private Members
        private string rel;
        private string url;
        #endregion

        /// <summary>
        /// The type of relation (self referencing, next/previous page, etc).
        /// </summary>
        [XmlAttribute("rel")]
        public string Rel
        {
            get { return this.rel; }
            set { this.rel = value; }
        }

        /// <summary>
        /// The destination resource of the relation.
        /// </summary>
        [XmlAttribute("href")]
        public string URL
        {
            get { return this.url; }
            set { this.url = value; }
        }
    }
}
