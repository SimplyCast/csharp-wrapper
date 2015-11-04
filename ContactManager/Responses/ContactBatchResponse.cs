//----------------------------------------------------------------
// ContactBatchResponse.cs
// Copyright SimplyCast 2015
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
    /// A response of a batch operation API call.
    /// </summary>
    [XmlRoot("batch")]
    public class ContactBatchResponse
    {
        #region Private Members
        private int id;
        private DateTime created;
        private DateTime updated;
        private BatchStatus status;
        private RelationLink[] links;
        #endregion

        /// <summary>
        /// The unique identifier of the batch operation.
        /// </summary>
        [XmlAttribute("id")]
        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        /// <summary>
        /// The date and time that the batch operation was submitted.
        /// </summary>
        [XmlAttribute("created", DataType = "dateTime")]
        public DateTime Created
        {
            get { return this.created; }
            set { this.created = value; }
        }

        /// <summary>
        /// The date and time of the last status update.
        /// </summary>
        [XmlAttribute("updated", DataType = "dateTime")]
        public DateTime Updated
        {
            get { return this.updated; }
            set { this.updated = value; }
        }

        /// <summary>
        /// The status of the batch operation.
        /// </summary>
        [XmlAttribute("status")]
        public BatchStatus Status
        {
            get { return this.status; }
            set { this.status = value; }
        }

        /// <summary>
        /// A collection of relation links containing at least a 
        /// self-reference.
        /// </summary>
        [XmlArray("links")]
        [XmlArrayItem("link")]
        public RelationLink[] Links
        {
            get { return this.links; }
            set { this.links = value; }
        }

        /// <summary>
        /// An enumeration of batch statuses.
        /// </summary>
        public enum BatchStatus
        {
            /// <summary>
            /// Pending batch status.
            /// </summary>
            [XmlEnum("pending")]
            Pending,

            /// <summary>
            /// Processing batch status.
            /// </summary>
            [XmlEnum("processing")]
            Processing,

            /// <summary>
            /// Complete batch status.
            /// </summary>
            [XmlEnum("complete")]
            Complete,

            /// <summary>
            /// Error batch status.
            /// </summary>
            [XmlEnum("error")]
            Error
        }
    }
}
