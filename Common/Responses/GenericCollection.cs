//----------------------------------------------------------------
// GenericCollection.cs
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
    /// This class is a helper for other collection classes, as they will often
    /// share the attributes below.
    /// </summary>
    abstract public class GenericCollection
    {
        #region Private Members
        private int totalCount;
        private int filterCount;
        private int responseCount;
        private RelationLink[] links;
        #endregion

        /// <summary>
        /// Total count represents the total number of entities available on
        /// the resource before filtering.
        /// </summary>
        [XmlAttribute("totalCount")]
        public int TotalCount
        {
            get { return this.totalCount; }
            set { this.totalCount = value; }
        }

        /// <summary>
        /// Filter count represents the total number of entities available on
        /// the resource after any filtering queries (excluding offset/limit)
        /// are applied.
        /// </summary>
        [XmlAttribute("filterCount")]
        public int FilterCount
        {
            get { return this.filterCount; }
            set { this.filterCount = value; }
        }

        /// <summary>
        /// Response count is the number of entities returned in the response, 
        /// provided as a convenience.
        /// </summary>
        [XmlAttribute("responseCount")]
        public int ResponseCount
        {
            get { return this.responseCount; }
            set { this.responseCount = value; }
        }

        /// <summary>
        /// A collection of relation links. Used for paging; next and prev 
        /// links will contain the appropriate query parameters for retrieving
        /// the next and previous pages.
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
