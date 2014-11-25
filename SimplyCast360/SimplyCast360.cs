//----------------------------------------------------------------
// SimplyCast360.cs
// Copyright SimplyCast 2014
// This projected is licensed under the terms of the MIT license.
//  (see the attached LICENSE.txt).
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplyCast.SimplyCast360
{
    /// <summary>
    /// SimplyCast 360 API class.
    /// </summary>
    public class SimplyCast360API : ResourceAPI
    {
        /// <summary>
        /// Pass-thru constructor.
        /// </summary>
        /// <param name="connector">The API connection to make requests 
        /// with.</param>
        public SimplyCast360API(SimplyCastAPIConnector connector) : base(connector) { }

        /// <summary>
        /// Get a 360 project. The ID of a 360 project can be retrieved from
        /// the user interface of the 360 project.
        /// </summary>
        /// <param name="projectID">The ID of the project to retrieve.</param>
        /// <returns>A representation of a 360 project (and the API connections
        /// contained within).</returns>
        public Responses.ProjectEntity GetProject(int projectID)
        {
            return this.connection.Call<Responses.ProjectEntity>("GET", "crossmarketer/" + projectID, null, null);
        }

        /// <summary>
        /// Get a connection endpoint by ID. The ID of a connection endpoint
        /// can be retrieved from the user interface of the 360 project.
        /// </summary>
        /// <param name="projectID">The ID of the project containing the 
        /// connection.</param>
        /// <param name="type">The connection endpoint type.</param>
        /// <param name="connectionID">The connection endpoint ID.</param>
        /// <returns>A representation of the connection endpoint.</returns>
        public Responses.ConnectionEntity GetConnection(int projectID, ConnectionType type, int connectionID)
        {
            string path = (type == ConnectionType.Inbound ? "inbound" : "outbound");
            return this.connection.Call<Responses.ConnectionEntity>("GET", "crossmarketer/" + projectID + "/" + path + "/" + connectionID, null, null);
        }

        /// <summary>
        /// Retrieve a collection of contacts from an outbound connection 
        /// endpoint.
        /// </summary>
        /// <param name="projectID">A 360 project ID.</param>
        /// <param name="connectionID">An outbound API connection ID.</param>
        /// <returns>A collection of contact representations.</returns>
        public Responses.ContactCollection GetOutboundContacts(int projectID, int connectionID)
        {
            Dictionary<string, string> queryParameters = new Dictionary<string, string>();
            queryParameters.Add("showprocessed", "1");
            return this.connection.Call<Responses.ContactCollection>("GET", "crossmarketer/" + projectID + "/outbound/" + connectionID, queryParameters, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="connectionID"></param>
        /// <param name="contactID"></param>
        /// <returns>A represenation of the contact entry in the outbound 
        /// connection node.</returns>
        public Responses.ContactEntity GetOutboundContact(int projectID, int connectionID, int contactID)
        {
            return this.connection.Call<Responses.ContactEntity>("GET", "crossmarketer/" + projectID + "/outbound/" + connectionID + "/" + contactID, null, null);
        }

        /// <summary>
        /// Submit a list to an inbound API connection endpoint.
        /// </summary>
        /// <param name="projectID">A 360 project ID.</param>
        /// <param name="connectionID">A 360 API connection endpoint ID.
        /// </param>
        /// <param name="listID">The ID of the list to push into the 360
        /// project.</param>
        public void PushList(int projectID, int connectionID, int listID)
        {
            Requests.ListEntity list = new Requests.ListEntity();
            list.List = listID;
            this.connection.Call<object>("POST", "crossmarketer/" + projectID + "/inbound/" + connectionID, null, list);
        }

        /// <summary>
        /// Submit a contact to an inbound API connection endpoint.
        /// </summary>
        /// <param name="projectID">A 360 project ID.</param>
        /// <param name="connectionID">A 360 API connection endpoint ID.
        /// </param>
        /// <param name="listID">The ID of a list that the contact belongs to.
        /// </param>
        /// <param name="contactID">The ID of the contact to push into the 
        /// 360 workflow.</param>
        public void PushContact(int projectID, int connectionID, int listID, int contactID)
        {
            Requests.ContactEntity contact = new Requests.ContactEntity();
            contact.List = listID;
            contact.Row = contactID;
            this.connection.Call<object>("POST", "crossmarketer/" + projectID + "/inbound/" + connectionID, null, contact);
        }

        /// <summary>
        /// Delete a contact from an outbound node. This only removes the
        /// contact from the outbound processing queue (permanently). 
        /// </summary>
        /// <param name="projectID">The ID of the project.</param>
        /// <param name="connectionID">The ID of the connection.</param>
        /// <param name="contactID">The ID of the contact to remove. This must
        /// be the 360 contact ID, not the contact manager contact ID.</param>
        public void DeleteContact(int projectID, int connectionID, int contactID)
        {
            this.connection.Call<object>("DELETE", "crossmarketer/" + projectID + "/outbound/" + connectionID + "/" + contactID, null, null);
        }

        /// <summary>
        /// The types of API connection endpoints that are available for 360
        /// projects.
        /// </summary>
        public enum ConnectionType
        {
            /// <summary>
            /// An inbound connection type.
            /// </summary>
            Inbound,

            /// <summary>
            /// An outbound connection.
            /// </summary>
            Outbound
        }
    }
}
