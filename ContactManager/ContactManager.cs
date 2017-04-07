//----------------------------------------------------------------
// ContactManager.cs
// Copyright SimplyCast 2014
// This projected is licensed under the terms of the MIT license.
//  (see the attached LICENSE.txt).
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplyCast.ContactManager
{
    /// <summary>
    /// Contact manager API resource class.
    /// </summary>
    public class ContactManagerAPI : ResourceAPI
    {
        /// <summary>
        /// Pass-thru constructor.
        /// </summary>
        /// <param name="connector">The API connection to make requests 
        /// with.</param>
        public ContactManagerAPI(SimplyCastAPIConnector connector) : base(connector) { }

        #region Enums
        /// <summary>
        /// An enum of column data types.
        /// </summary>
        public enum ColumnType
        {
            /// <summary>
            /// This column type stores general data (string or numeric).
            /// </summary>
            String = 0,

            /// <summary>
            /// This column type specifically stores dates/times.
            /// </summary>
            Date = 1,

            /// <summary>
            /// This column type specifically stores Integers.
            /// </summary>
            Integer = 2,

            /// <summary>
            /// This column type specifically stores Number.
            /// </summary>
            Number = 3,

            /// <summary>
            /// This column type specifically stores bool.
            /// </summary>
            Boolean = 4,

            /// <summary>
            /// This column type specifically stores text.
            /// </summary>
            Text = 5,

        }

        /// <summary>
        /// Enum representing the types of metadata columns available.
        /// </summary>
        public enum MetadataColumnType
        {
            /// <summary>
            /// This column type holds a single string value.
            /// </summary>
            SingleString = 0,

            /// <summary>
            /// This column type holds a single numeric value.
            /// </summary>
            SingleNumber = 1,

            /// <summary>
            /// This column type holds multiple string values.
            /// </summary>
            MultiString = 2,

            /// <summary>
            /// This column type holds multiple numeric values.
            /// </summary>
            MultiNumber = 3,

            /// <summary>
            /// This column type acts as a numeric adder. For example, if the 
            /// value of the column is currently 5 and you 'set' the value to
            /// 4, the value will become 9.
            /// </summary>
            SumNumber = 4
        }

        /// <summary>
        /// A suppression list type; each data type is stored in a different 
        /// list.
        /// </summary>
        public enum SuppressionType
        {
            /// <summary>
            /// An email suppression list.
            /// </summary>
            Email,

            /// <summary>
            /// A phone number suppression list.
            /// </summary>
            Phone,

            /// <summary>
            /// A fax number suppression list.
            /// </summary>
            Fax,

            /// <summary>
            /// A mobile number suppression list.
            /// </summary>
            Mobile
        }
        #endregion

        #region List Management Resources
        #region GetLists
        /// <summary>
        /// Get a collection of contact lists entities.
        /// </summary>
        /// <returns>A ListCollection of the first hundred lists on the 
        /// account.</returns>
        public Responses.ListCollection GetLists()
        {
            return this.GetLists(0, 100, "");
        }

        /// <summary>
        /// Get a collection of contact list entities in a specific range.
        /// </summary>
        /// <param name="offset">The zero based offset index to start 
        /// retrieving from.</param>
        /// <param name="limit">The number of lists to return.</param>
        /// <returns>A ListCollection object of lists.</returns>
        public Responses.ListCollection GetLists(int offset, int limit)
        {
            return this.GetLists(offset, limit, "");
        }

        /// <summary>
        /// Get a collection of contact list entities in a specific
        /// range with an additional query.
        /// </summary>
        /// <param name="offset">The zero-based offset to start the page at.
        /// </param>
        /// <param name="limit">The number of entries to retrieve past the 
        /// offset.</param>
        /// <param name="query">An optional query to filter the results by. 
        /// More details on what values can be queried upon are available 
        /// in the API reference docs.</param>
        /// <returns>A ListCollection of lists.</returns>
        public Responses.ListCollection GetLists(int offset, int limit, string listName)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>(3);
            queryParams.Add("offset", offset.ToString());
            queryParams.Add("limit", limit.ToString());

            if (listName.Length > 0) {
                queryParams.Add("listName", listName);
            }

            return this.connection.Call<Responses.ListCollection>(SimplyCastAPI.GET, "contactmanager/lists", queryParams, null);
        }

        #endregion

        /// <summary>
        /// Create a new contact list.
        /// </summary>
        /// <param name="listName">The name of the new list.</param>
        /// <returns>A ListEntity representation of the new list.</returns>
        public Responses.ListEntity CreateList(string listName)
        {
            Requests.ListEntity list = new Requests.ListEntity();
            list.Name = listName;
            return this.connection.Call<Responses.ListEntity>(SimplyCastAPI.POST, "contactmanager/lists", null, list);
        }

        /// <summary>
        /// Retrieve a contact list entity by its ID.
        /// </summary>
        /// <param name="listID">The ID of the list to retrieve.</param>
        /// <returns>A ListEntity of the retrieved contact list.</returns>
        public Responses.ListEntity GetList(int listID)
        {
            return this.connection.Call<Responses.ListEntity>(SimplyCastAPI.GET, "contactmanager/lists/" + listID, null, null);
        }

        /// <summary>
        /// Helper function to retrieve lists by a specific list name. As 
        /// list names aren't necessarily unique (although the API enforces 
        /// uniqueness on list creation), this method can return zero, one, 
        /// or more lists.
        /// </summary>
        /// <param name="name">The name of the list to retrieve.</param>
        /// <returns>A ListCollection of matching contact lists.</returns>
        public Responses.ListCollection GetListsByName(string name)
        {
            return this.GetLists(0, 100, name);
        }

        /// <summary>
        /// Rename a list, given its list ID. Keep in mind that the API 
        /// enforces uniqueness of list names, and will throw an error if 
        /// the given list name already exists.
        /// </summary>
        /// <param name="listID">The ID of the list to change.</param>
        /// <param name="name">The new name of the list.</param>
        /// <returns>A ListEntity with the updated contact list
        /// representation.</returns>
        public Responses.ListEntity RenameList(int listID, string name)
        {
            Requests.ListEntity list = new Requests.ListEntity();
            list.Name = name;
            return this.connection.Call<Responses.ListEntity>(SimplyCastAPI.POST, "contactmanager/lists/" + listID, null, list);
        }

        /// <summary>
        /// Delete a list. This only deletes the list, the contacts on the 
        /// list will still exist in the system, and may belong to other lists.
        /// </summary>
        /// <param name="listID">The ID of the list to delete</param>
        /// <returns>Return true on success, false on failure.</returns>
        public void DeleteList(int listID)
        {
            this.connection.Call<Object>(SimplyCastAPI.DELETE, "contactmanager/lists/" + listID, null, null);
        }

        #region GetContactsFromList
        /// <summary>
        /// Retrieve all contacts that belong to a list.
        /// </summary>
        /// <param name="listID">The ID of the list to retrieve contacts from.
        /// </param>
        /// <returns>A collection of contact representations.</returns>
        public Responses.ContactCollection GetContactsFromList(int listID)
        {
            return this.GetContactsFromList(listID, 0, 100, "");
        }

        /// <summary>
        /// Retrieve all contacts that belong to a list within a specific range.
        /// </summary>
        /// <param name="listID">The ID of the list to retrieve contacts from.
        /// </param>
        /// <param name="offset">The zero-based offset to start the page at.
        /// </param>
        /// <param name="limit">The number of entries to retrieve past the 
        /// offset.</param>
        /// <returns>A collection of contact representations in the requested
        /// range.</returns>
        public Responses.ContactCollection GetContactsFromList(int listID, int offset, int limit)
        {
            return this.GetContactsFromList(listID, offset, limit, "");
        }

        /// <summary>
        /// Retrieve all contacts that belong to a list within a specific range
        /// and matching a specific criteria.
        /// </summary>
        /// <param name="listID">The ID of the list to retrieve contacts from.
        /// </param>
        /// <param name="offset">The zero-based offset to start the page at.
        /// </param>
        /// <param name="limit">The number of entries to retrieve past the 
        /// offset.</param>
        /// <param name="query">An optional query to filter the results by. 
        /// More details on what values can be queried upon are available 
        /// in the API reference docs.</param>
        /// <returns>A collection of contact representations after applying 
        /// offsets and filters.</returns>
        public Responses.ContactCollection GetContactsFromList(int listID, int offset, int limit, string query)
        {
            Dictionary<string, string> queryParameters = new Dictionary<string, string>(3);
            queryParameters.Add("offset", offset.ToString());
            queryParameters.Add("limit", limit.ToString());
            if (query.Length > 0)
            {
                queryParameters.Add("query", query);
            }

            return this.connection.Call<Responses.ContactCollection>(SimplyCastAPI.GET, "contactmanager/lists/" + listID + "/contacts", queryParameters, null);
        }
        #endregion

        #region AddContactsToList

        /// <summary>
        /// Given a list ID and an array of contact IDs, add the contacts to 
        /// the list.
        /// </summary>
        /// <param name="listID">The ID of the list to add the contacts to.
        /// </param>
        /// <param name="contactIDs">An array of contact IDs.</param>
        /// <returns>A collection of contact representations for the entries 
        /// that were added to the list.</returns>
        public Responses.ContactCollection AddContactsToList(int listID, int[] contactIDs)
        {
            return this.AddContactsToList(listID, contactIDs, true);
        }

        /// <summary>
        /// Given a list ID and an array of contact IDs, add the contacts to 
        /// the list.
        /// </summary>
        /// <param name="listID">The ID of the list to add the contacts to.
        /// </param>
        /// <param name="contactIDs">An array of contact IDs.</param>
        /// <param name="strict">If true, this method will throw an error if 
        /// any of the contacts to add to the list don't exist. If false, the 
        /// contacts that don't exist will be ignored.</param>
        /// <returns>A collection of contact representations for the entries 
        /// that were added to the list.</returns>
        public Responses.ContactCollection AddContactsToList(int listID, int[] contactIDs, bool strict)
        {
            Requests.ContactIDCollection contacts = new Requests.ContactIDCollection();
            Dictionary<string, string> queryParams = new Dictionary<string, string>();
            queryParams.Add("strict",  strict ? "1" : "0");

            for (int i = 0; i < contactIDs.Length; i++)
            {
                contacts.addContactID(contactIDs[i]);
            }
            return this.connection.Call<Responses.ContactCollection>("POST", "contactmanager/lists/" + listID + "/contacts", queryParams, contacts);
        }

        #endregion

        /// <summary>
        /// Given a list ID and a contact ID, delete the contact from the list.
        /// Note that this only removes the contact from the list; the contact 
        /// will still exist in the system and may belong to other lists.
        /// </summary>
        /// <param name="listID">The ID of the list to delete the contact from.
        /// </param>
        /// <param name="contactID">The ID of the contact to remove from the 
        /// list.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        public void DeleteContactFromList(int listID, int contactID)
        {
            this.connection.Call<object>("DELETE", "contactmanager/lists/" + listID + "/contacts/" + contactID, null, null);
        }
        #endregion 

        #region Contact Management Resources
        #region ContactCollection
        /// <summary>
        /// Get a collection of contacts from the system contact database.
        /// </summary>
        /// <returns>A collection of contact representations.</returns>
        public Responses.ContactCollection GetContacts()
        {
            return this.GetContacts(0, 100, "", false, false);
        }

        /// <summary>
        /// Get a collection of contacts from the system contact database 
        /// within a defined range.
        /// </summary>
        /// <param name="offset">The zero-based offset to start the page at.
        /// </param>
        /// <param name="limit">The number of entries to retrieve past the 
        /// offset.</param>
        /// <returns>A collection of contact representations.</returns>
        public Responses.ContactCollection GetContacts(int offset, int limit)
        {
            return this.GetContacts(offset, limit, "", false, false);
        }

        /// <summary>
        /// Get a collection of contacts from the system contact database 
        /// within a defined range and filtered by a query.
        /// </summary>
        /// <param name="offset">The zero-based offset to start the page at.
        /// </param>
        /// <param name="limit">The number of entries to retrieve past the 
        /// offset.</param>
        /// <param name="query">An optional query to filter the results by.
        /// More details on what values can be queried upon are available in 
        /// the API reference docs.</param>
        /// <returns>A collection of contact representations.</returns>
        public Responses.ContactCollection GetContacts(int offset, int limit, string query)
        {
            return this.GetContacts(offset, limit, query, false, false);
        }

        /// <summary>
        /// Get a collection of contacts from the system contact database 
        /// within a defined range and filtered by a query.
        /// </summary>
        /// <param name="offset">The zero-based offset to start the page 
        /// at.</param>
        /// <param name="limit">The number of entries to retrieve past the 
        /// offset.</param>
        /// <param name="query">An optional query to filter the results by.
        /// More details on what values can be queried upon are available in 
        /// the API reference docs.</param>
        /// <param name="ignoreEmptyFields">If true, any fields with an empty
        /// value will not be returned in the response. The default is to 
        /// return all fields.</param>
        /// <param name="getExtendedFields">If true, this method will return 
        /// a series of system fields as well as the basic contact fields. 
        /// This is not the default.</param>
        /// <returns>A collection of contact representations.</returns>
        public Responses.ContactCollection GetContacts(int offset, int limit, string query, bool ignoreEmptyFields, bool getExtendedFields)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>();
            queryParams.Add("offset", offset.ToString());
            queryParams.Add("limit", limit.ToString());
            if (query.Length > 0)
            {
                queryParams.Add("query", query);
            }

            queryParams.Add("ignoreEmptyFields", ignoreEmptyFields ? "1" : "0");
            queryParams.Add("extended", getExtendedFields ? "1" : "0");

            return this.connection.Call<Responses.ContactCollection>("GET", "contactmanager/contacts", queryParams, null);
        }
        #endregion

        #region CreateContact

        /// <summary>
        /// Create a new contact.
        /// </summary>
        /// <param name="fields">An array of Field objects containing the 
        /// fields to set.</param>
        /// <returns>A representation of the newly created contact.</returns>
        public Responses.ContactEntity CreateContact(Dictionary<string, string> fields)
        {
            return this.CreateContact(fields, new int[] {});
        }

        /// <summary>
        /// Create a new contact and assign it to one or more lists.
        /// </summary>
        /// <param name="fields">A dictionary of field IDs (as the key) and 
        /// the new field value (as the value).</param>
        /// <param name="listIDs">An array of list IDs to assign the contact 
        /// to.</param>
        /// <returns>A representation of the newly created contact.</returns>
        public Responses.ContactEntity CreateContact(Dictionary<string, string> fields, int[] listIDs)
        {
            Requests.ContactEntity contact = new Requests.ContactEntity();

            Requests.FieldEntity[] fieldArray = new Requests.FieldEntity[fields.Count];
            int fieldCounter = 0;
            foreach (KeyValuePair<string, string> f in fields)
            {
                Requests.FieldEntity fieldEntity = new Requests.FieldEntity();
                fieldEntity.ID = f.Key;
                fieldEntity.Value = f.Value;
                fieldArray[fieldCounter++] = fieldEntity;
            }
            contact.Fields = fieldArray;

            if (listIDs.Length > 0)
            {
                int listCounter = 0;
                Requests.ListEntity[] listArray = new Requests.ListEntity[listIDs.Length];
                foreach (int id in listIDs)
                {
                    Requests.ListEntity listEntity = new Requests.ListEntity();
                    listEntity.ID = id;
                    listArray[listCounter++] = listEntity;
                }
                contact.ListIds = listArray;
            }

            contact.ID = null;

            return this.connection.Call<Responses.ContactEntity>("POST", "contactmanager/contacts", null, contact);
        }
        #endregion

        #region UpsertContact

        /// <summary>
        /// Perform an upsert operation by either creating a contact or merging
        /// the given data into all contacts that match the provided merge 
        /// column. For example, if the email field is provided in the fields
        /// parameter and is also specified as the merge column, all contacts
        /// with that email value will have their entries updated with the new
        /// data. If there is no match, a new contact will be created.
        /// </summary>
        /// <param name="fields">The fields to create / update.</param>
        /// <param name="mergeColumnID">The column ID to merge upon.</param>
        /// <returns>A collection containing all contacts modified by the 
        /// operation.</returns>
        public Responses.ContactCollection UpsertContact(Dictionary<string, string> fields, string mergeColumnID)
        {
            return this.UpsertContact(fields, mergeColumnID, new int[] {});
        }

        /// <summary>
        /// Perform an upsert operation by either creating a contact or merging
        /// the given data into all contacts that match the provided merge 
        /// column. For example, if the email field is provided in the fields
        /// parameter and is also specified as the merge column, all contacts
        /// with that email value will have their entries updated with the new
        /// data. If there is no match, a new contact will be created.
        /// </summary>
        /// <param name="fields">The fields to create / update.</param>
        /// <param name="mergeColumnID">The column ID to merge upon.</param>
        /// <returns>A collection containing all contacts modified by the 
        /// <param name="listIDs">An array of list IDs to add the contact(s) to.
        /// </param>
        /// operation.</returns>
        public Responses.ContactCollection UpsertContact(Dictionary<string, string> fields, string mergeColumnID, int[] listIDs)
        {
            Dictionary<string, string> queryParameters = new Dictionary<string, string>();
            queryParameters.Add("mergecolumn", mergeColumnID);

            Requests.ContactEntity contact = new Requests.ContactEntity();

            Requests.FieldEntity[] fieldArray = new Requests.FieldEntity[fields.Count];
            int fieldCounter = 0;
            foreach (KeyValuePair<string, string> f in fields)
            {
                Requests.FieldEntity fieldEntity = new Requests.FieldEntity();
                fieldEntity.ID = f.Key;
                fieldEntity.Value = f.Value;
                fieldArray[fieldCounter++] = fieldEntity;
            }
            contact.Fields = fieldArray;

            if (listIDs.Length > 0)
            {
                int listCounter = 0;
                Requests.ListEntity[] listArray = new Requests.ListEntity[listIDs.Length];
                foreach (int id in listIDs)
                {
                    Requests.ListEntity listEntity = new Requests.ListEntity();
                    listEntity.ID = id;
                    listArray[listCounter++] = listEntity;
                }
                contact.ListIds = listArray;
            }

            contact.ID = null;
 
            string xml = this.connection.Call<string>("POST", "contactmanager/contacts", queryParameters, contact);

            //We're going to deserialize ourselves, since the response could be 
            //either a contact collection or a contact entity. We're going to
            //turn the contact entity into a collection for consistency.
            using (System.Xml.XmlReader reader = new System.Xml.XmlTextReader(new System.IO.StringReader(xml)))
            {
                //If it's a collection, return it.
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(Responses.ContactCollection));
                if (serializer.CanDeserialize(reader))
                {
                    return (Responses.ContactCollection) serializer.Deserialize(reader);
                }

                serializer = new System.Xml.Serialization.XmlSerializer(typeof(Responses.ContactEntity));

                //If it's an entity, create a collection and put the contact in it.
                if (serializer.CanDeserialize(reader))
                {
                    Responses.ContactEntity c = (Responses.ContactEntity) serializer.Deserialize(reader);
                    Responses.ContactCollection contactCollection = new Responses.ContactCollection();

                    contactCollection.Contacts = new Responses.ContactEntity[] { (Responses.ContactEntity) c };
                    return contactCollection;
                }
                else {
                    throw new Exception("Invalid XML response: could not deserialize into a ContactEntity or ContactCollection.");
                }
            }
        }

        #endregion

        /// <summary>
        /// Get a contact from the contact database by its ID.
        /// </summary>
        /// <param name="contactID">The ID of the contact to retrieve.</param>
        /// <returns>A representation of the retrieved contact.</returns>
        public Responses.ContactEntity GetContact(int contactID)
        {
            return this.connection.Call<Responses.ContactEntity>("GET", "contactmanager/contacts/" + contactID, null, null);
        }

        /// <summary>
        /// Update the field values of the given contact.
        /// </summary>
        /// <param name="contactID">The ID of the contact  to update.</param>
        /// <param name="fields">A dictionary of field IDs (as the key) and 
        /// the new field value (as the value).</param>
        /// <returns>A representation of the updated contact.</returns>
        public Responses.ContactEntity UpdateContact(int contactID, Dictionary<string, string> fields)
        {
            Requests.ContactEntity contact = new Requests.ContactEntity();
            Requests.FieldEntity[] fieldArray = new Requests.FieldEntity[fields.Count];
            int fieldCounter = 0;
            foreach (KeyValuePair<string, string> f in fields)
            {
                Requests.FieldEntity fieldEntity = new Requests.FieldEntity();
                fieldEntity.ID = f.Key;
                fieldEntity.Value = f.Value;
                fieldArray[fieldCounter++] = fieldEntity;
            }
            contact.Fields = fieldArray;

            return this.connection.Call<Responses.ContactEntity>("POST", "contactmanager/contacts/" + contactID, null, contact);
        }

        /// <summary>
        /// Permanently delete a contact from the system (and all lists).
        /// </summary>
        /// <param name="contactID">The ID of the contact to delete.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        public void DeleteContact(int contactID)
        {
            this.connection.Call<object>("DELETE", "contactmanager/contacts/" + contactID, null, null);
        }

        #region GetContactMetadata
        /// <summary>
        /// Get all metadata fields for the specified contact.
        /// </summary>
        /// <param name="contactID">The ID of the contact to retrieve 
        /// metadata fields for.</param>
        /// <returns>A collection of metadata fields.</returns>
        public Responses.MetadataFieldCollection GetContactMetadata(int contactID)
        {
            return this.GetContactMetadata(contactID, 0, 100);
        }

        /// <summary>
        /// Get all metadata fields for the specified contact with paging 
        /// parameters.
        /// </summary>
        /// <param name="contactID">The ID of the contact to retrieve 
        /// metadata fields for.</param>
        /// <param name="offset">The zero-based offset to start the page at.</param>
        /// <param name="limit">The number of entries to retrieve past the offset.</param>
        /// <returns>A collection of metadata fields.</returns>
        public Responses.MetadataFieldCollection GetContactMetadata(int contactID, int offset, int limit)
        {
            Dictionary<string, string> queryParameters = new Dictionary<string, string>();
            queryParameters.Add("offset", offset.ToString());
            queryParameters.Add("limit", limit.ToString());
            return this.connection.Call<Responses.MetadataFieldCollection>("GET", "contactmanager/contacts/" + contactID + "/metadata", queryParameters, null);
        }
        #endregion

        /// <summary>
        /// Get a metadata field for a contact by ID.
        /// </summary>
        /// <param name="contactID">The ID of the contact to get the metadata 
        /// field from.</param>
        /// <param name="metadataFieldID">The ID of the metadata field to get. 
        /// System metadata columns are typically strings. User-defined 
        /// metadata columns, however, are autoincrementing unique 
        /// integers.</param>
        /// <returns>The requested metadata field.</returns>
        public Responses.MetadataFieldEntity GetContactMetadataField(int contactID, string metadataFieldID)
        {
            return this.connection.Call<Responses.MetadataFieldEntity>("GET", "contactmanager/contacts/" + contactID + "/metadata/" + metadataFieldID, null, null);
        }

        /// <summary>
        /// Helper function to retrieve metadata columns by name. As this is
        /// not guaranteed to be unique, this method can return multiple 
        /// fields.
        /// </summary>
        /// <param name="contactID">The ID of the contact to get the metadata
        /// field from.</param>
        /// <param name="metadataFieldName">The name of the field(s) to 
        /// retrieve.</param>
        /// <returns>The requested metadata field(s).</returns>
        public Responses.MetadataFieldCollection GetContactMetadataFieldByName(int contactID, string metadataFieldName)
        {
            Dictionary<string, string> queryParameters = new Dictionary<string, string>();
            queryParameters.Add("query", "`name` = '" + metadataFieldName + "'");
            return this.connection.Call<Responses.MetadataFieldCollection>("GET", "contactmanager/contacts/" + contactID + "/metadata", queryParameters, null);
        }

        #region UpdateContactMetadataField
        /// <summary>
        /// Update a metadata field. This function accepts an array of string 
        /// values. If the metadata field type accepts a singular value, you
        /// are expected to hand only a single value in this parameter. If 
        /// the field type accepts numeric values, hand the string 
        /// representation of the numeric value. 
        /// </summary>
        /// <param name="contactID">The ID of the contact to modify.</param>
        /// <param name="metadataFieldID">The ID of the field to modify.
        /// </param>
        /// <param name="values">The value(s) to set on the field.</param>
        /// <returns>The metadata field with the updated value.</returns>
        public Responses.MetadataFieldEntity UpdateContactMetadataField(int contactID, string metadataFieldID, string[] values)
        {
            return this.UpdateContactMetadataField(contactID, metadataFieldID, values, false);
        }

        /// <summary>
        /// Update a metadata field. This function accepts an array of string 
        /// values. If the metadata field type accepts a singular value, you
        /// are expected to hand only a single value in this parameter. If 
        /// the field type accepts numeric values, hand the string 
        /// representation of the numeric value. 
        /// </summary>
        /// <param name="contactID">The ID of the contact to modify.</param>
        /// <param name="metadataFieldID">The ID of the field to modify.
        /// </param>
        /// <param name="values">The value(s) to set on the field.</param>
        /// <param name="overwrite">This flag gives the option to overwrite
        /// the existing metadata value, in the case of multivalued and
        /// incrementer field types. </param>
        /// <returns>The metadata field with the updated value.</returns>
        public Responses.MetadataFieldEntity UpdateContactMetadataField(int contactID, string metadataFieldID, string[] values, bool overwrite)
        {
            Dictionary<string, string> queryParameters = new Dictionary<string, string>();
            queryParameters.Add("overwrite", overwrite ? "1" : "0");

            Requests.MetadataFieldEntity body = new Requests.MetadataFieldEntity();
            body.Values = values;

            return this.connection.Call<Responses.MetadataFieldEntity>("POST", "contactmanager/contacts/" + contactID + "/metadata/" + metadataFieldID, queryParameters, body);
        }
        #endregion

        /// <summary>
        /// This function will create a collection of contacts. It will require
        /// you to construct the contact entity objects by hand and supply them
        /// as an array.
        /// 
        /// This method will defer the processing of the upload. An identifier
        /// will be returned that can be used to query the status of the batch.
        /// </summary>
        /// <param name="contacts">An array of ContactEntity objects.</param>
        /// <returns>The response will contain a unique identifier that you
        /// can track the batch status of the job with.</returns>
        public Responses.ContactBatchResponse BatchCreateContacts(Requests.ContactEntity[] contacts)
        {
            Requests.ContactBatchCollection batch = new Requests.ContactBatchCollection();
            batch.Contacts = contacts;
            return this.connection.Call<Responses.ContactBatchResponse>("POST", "contactmanager/contacts/batch", null, batch);
        }

        /// <summary>
        /// This method upserts a collection of contacts by providing a column
        /// ID to merge upon. If the provided contact matches an existing one
        /// on the provided column, the contact will be updated. Otherwise, it
        /// will be created.
        /// </summary>
        /// <param name="contacts">An array of ContactEntity objects.</param>
        /// <param name="mergeColumnID"></param>
        /// <returns>The response will contain a unique identifier that you
        /// can track the batch status of the job with.</returns>
        public Responses.ContactBatchResponse BatchCreateContacts(Requests.ContactEntity[] contacts, string mergeColumnID)
        {
            Dictionary<string, string> queryParameters = new Dictionary<string, string>();
            queryParameters.Add("mergecolumn", mergeColumnID);
            Requests.ContactBatchCollection batch = new Requests.ContactBatchCollection();
            batch.Contacts = contacts;
            return this.connection.Call<Responses.ContactBatchResponse>("POST", "contactmanager/contacts/batch", queryParameters, batch);
        }

        /// <summary>
        /// Get information about the given contact batch operation.
        /// </summary>
        /// <param name="batchID">The ID of the batch operation to get 
        /// information for.</param>
        /// <returns>A batch response containing the status of the job.</returns>
        public Responses.ContactBatchResponse GetBatchStatus(int batchID)
        {
            return this.connection.Call<Responses.ContactBatchResponse>("GET", "contactmanager/contacts/batch/" + batchID.ToString(), null, null);
        }

        /// <summary>
        /// Get the results of a contact batch operation (including the 
        /// representations of the created contacts).
        /// </summary>
        /// <param name="batchID">The ID of the batch operation to get a 
        /// result for.</param>
        /// <returns>A ContactBatchResult object containing the batch
        /// result.</returns>
        public Responses.ContactBatchResultCollection GetBatchResult(int batchID)
        {
            return this.connection.Call<Responses.ContactBatchResultCollection>("GET", "contactmanager/contacts/batch/" + batchID.ToString() + "/result", null, null);
        }

        #endregion

        #region Metadata Resources 

        /// <summary>
        /// Get a collection of metadata columns.
        /// </summary>
        /// <returns>A collection of metadata columnns.</returns>
        public Responses.MetadataColumnCollection GetMetadataColumns()
        {
            return this.GetMetadataColumns(0, 100);
        }

        /// <summary>
        /// Get a collection of metadata columns.
        /// </summary>
        /// <param name="offset">The zero-based offset to start the page at.
        /// </param>
        /// <param name="limit">The number of entries to retrieve past the 
        /// offset.</param>
        /// <returns>A collection of metadata columns.</returns>
        public Responses.MetadataColumnCollection GetMetadataColumns(int offset, int limit)
        {
            Dictionary<string, string> queryParameters = new Dictionary<string, string>();
            queryParameters.Add("offset", offset.ToString());
            queryParameters.Add("limit", limit.ToString());
            return this.connection.Call<Responses.MetadataColumnCollection>("GET", "contactmanager/metadata", queryParameters, null);
        }

        /// <summary>
        /// Create a new metadata column.
        /// </summary>
        /// <param name="name">The name of the new metadata column.</param>
        /// <param name="type">The metadata column type.</param>
        /// <returns>An entity representation of the new column.</returns>
        public Responses.MetadataColumnEntity CreateMetadataColumn(string name, MetadataColumnType type) {
            Requests.MetadataColumnEntity requestBody = new Requests.MetadataColumnEntity();
            requestBody.Name = name;
            switch (type)
            {
                case MetadataColumnType.MultiNumber:
                    requestBody.Type = "multi number";
                    break;
                case MetadataColumnType.MultiString:
                    requestBody.Type = "multi string";
                    break;
                case MetadataColumnType.SingleNumber:
                    requestBody.Type = "single number";
                    break;
                case MetadataColumnType.SingleString:
                    requestBody.Type = "single string";
                    break;
                case MetadataColumnType.SumNumber:
                    requestBody.Type = "sum number";
                    break;
            }
                
            return this.connection.Call<Responses.MetadataColumnEntity>("POST", "contactmanager/metadata", null, requestBody);
        }

        /// <summary>
        /// Get a metadata column by ID.
        /// </summary>
        /// <param name="columnID">The ID of the column to retrieve.</param>
        /// <returns>A metadata column entity.</returns>
        public Responses.MetadataColumnEntity GetMetadataColumn(string columnID)
        {
            return this.connection.Call<Responses.MetadataColumnEntity>("GET", "contactmanager/metadata/" + columnID, null, null);
        }

        /// <summary>
        ///  Helper function to retrieve metadata columns by name. As there 
        ///  may be more than one column with a given name, this method can 
        ///  return more than one column.
        /// </summary>
        /// <param name="columnName">The metadata column name to search for.
        /// </param>
        /// <returns>A collection of the matching metadata columns.</returns>
        public Responses.MetadataColumnCollection GetMetadataColumnByName(string columnName)
        {
            Dictionary<string, string> queryParameters = new Dictionary<string, string>();
            queryParameters.Add("query", "`name` = '" + columnName + "'");
            return this.connection.Call<Responses.MetadataColumnCollection>("GET", "contactmanager/metadata", queryParameters, null);
        }

        /// <summary>
        /// Rename a metadata column.
        /// </summary>
        /// <param name="columnID">The ID of the column to rename.</param>
        /// <param name="name">The new name of the column.</param>
        /// <returns>An entity representation of the updated column.
        /// </returns>
        public Responses.MetadataColumnEntity RenameMetadataColumn(string columnID, string name)
        {
            Requests.MetadataColumnEntity requestBody = new Requests.MetadataColumnEntity();
            requestBody.Name = name;
            return this.connection.Call<Responses.MetadataColumnEntity>("POST", "contactmanager/metadata/" + columnID, null, requestBody);
        }

        /// <summary>
        /// Delete a metadata column.
        /// </summary>
        /// <param name="columnID">The ID of the column to delete.</param>
        public void DeleteMetadataColumn(string columnID)
        {
            this.connection.Call<object>("DELETE", "contactmanager/metadata/" + columnID, null, null);
        }

        #endregion

        #region Column Resources

        #region GetColumns
        /// <summary>
        /// Retrieve a collection of columns.
        /// </summary>
        /// <returns>A collection of column representations.</returns>
        public Responses.ColumnCollection GetColumns()
        {
            return this.GetColumns(0, 100);
        }

        /// <summary>
        /// Retrieve a collection of columns.
        /// </summary>
        /// <param name="offset">The zero-based offset to start the page at.
        /// </param>
        /// <param name="limit">The number of entries to retrieve past the 
        /// offset.</param>
        /// <returns>A collection of column representations.</returns>
        public Responses.ColumnCollection GetColumns(int offset, int limit)
        {
            Dictionary<string, string> queryParameters = new Dictionary<string, string>();
            queryParameters.Add("offset", offset.ToString());
            queryParameters.Add("limit", limit.ToString());

            return this.connection.Call<Responses.ColumnCollection>("GET", "contactmanager/columns", queryParameters, null);
        }
        #endregion

        /// <summary>
        /// Helper function to retrieve columns by name. As this in not 
        /// guaranteed to be unique, this method can return multiple columns 
        /// with the same name.
        /// </summary>
        /// <param name="name">The column name to search for.</param>
        /// <returns>A collection of one or more matching columns.</returns>
        public Responses.ColumnCollection GetColumnsByName(string name)
        {
            Dictionary<string, string> queryParameters = new Dictionary<string, string>();
            queryParameters.Add("query", "`name` = '" + name + "'");
            return this.connection.Call<Responses.ColumnCollection>("GET", "contactmanager/columns", queryParameters, null);
        }


        #region CreateColumn

        /// <summary>
        /// Create a new column.
        /// </summary>
        /// <param name="name">The name to give to the new column. Column names
        /// must be unique; an error will be thrown if the requested name 
        /// already exists.</param>
        /// <param name="type">The column type, one of ['string', 'date'].
        /// </param>
        /// <returns>A representation of the new column.</returns>
        public Responses.ColumnEntity CreateColumn(string name, ColumnType type)
        {
            return this.CreateColumn(name, type, new string[] { });
        }

        /// <summary>
        /// Create a new column.
        /// </summary>
        /// <param name="name">The name to give to the new column. Column names
        /// must be unique; an error will be thrown if the requested name 
        /// already exists.</param>
        /// <param name="type">The column type, one of ['string', 'date'].
        /// </param>
        /// <param name="mergeTags">An optional collection of merge tags to 
        /// give the column. Each merge tag should be a string enclosed by 
        /// dual percent signs (like %%TAG%%).</param>
        /// <returns>A representation of the new column.</returns>
        public Responses.ColumnEntity CreateColumn(string name, ColumnType type, string[] mergeTags)
        {
            Requests.ColumnEntity column = new Requests.ColumnEntity();
            column.Name = name;
            column.Type = (Requests.ColumnEntity.ColumnType) type;
            column.MergeTags = mergeTags;

            return this.connection.Call<Responses.ColumnEntity>("POST", "contactmanager/columns", null, column);
        }
        #endregion

        /// <summary>
        /// Retrieve a column by ID.
        /// </summary>
        /// <param name="columnID">The ID of the column to retrieve.</param>
        /// <returns>A representation of the requested column.</returns>
        public Responses.ColumnEntity GetColumn(string columnID)
        {
            return this.connection.Call<Responses.ColumnEntity>("GET", "contactmanager/columns/" + columnID, null, null);
        }

        #region UpdateMergeTags
        /// <summary>
        /// Change the merge tags on the given column.
        /// </summary>
        /// <param name="columnID">The ID of the column to update.</param>
        /// <param name="mergeTags">An array of merge tags to assign to the 
        /// column. Each merge tag should be a string enclosed by dual 
        /// percent signs (like %%TAG%%).</param>
        /// <returns>The updated column entity.</returns>
        public Responses.ColumnEntity UpdateMergeTags(string columnID, string[] mergeTags)
        {
            return this.UpdateMergeTags(columnID, mergeTags, true);
        }

        /// <summary>
        /// Change the merge tags on the given column.
        /// </summary>
        /// <param name="columnID">The ID of the column to update.</param>
        /// <param name="mergeTags">An array of merge tags to assign to the 
        /// column. Each merge tag should be a string enclosed by dual 
        /// percent signs (like %%TAG%%).</param>
        /// <param name="append">If true, append the provided tags to the 
        /// existing tags; otherwise, overwrite all existing merge tags.
        /// </param>
        /// <returns>The updated column entity.</returns>
        public Responses.ColumnEntity UpdateMergeTags(string columnID, string[] mergeTags, bool append)
        {
            Dictionary<string, string> queryParameters = new Dictionary<string, string>();
            queryParameters.Add("append", append ? "1" : "0");

            Requests.ColumnEntity column = new Requests.ColumnEntity();
            column.MergeTags = mergeTags;

            return this.connection.Call<Responses.ColumnEntity>("POST", "contactmanager/columns/" + columnID, queryParameters, column);
        }
        #endregion

        #endregion

        #region Suppression Resources

        #region GetSuppressionListEntries
        /// <summary>
        /// Get a collection of suppression list entries.
        /// </summary>
        /// <param name="listType">The type of list to get entries from.
        /// </param>
        /// <returns>A collection of suppression list entries.</returns>
        public Responses.SuppressionCollection GetSuppressionListEntries(SuppressionType listType)
        {
            return this.GetSuppressionListEntries(listType, 0, 100, "");
        }

        /// <summary>
        /// Get a collection of suppression list entries.
        /// </summary>
        /// <param name="listType">The type of list to get entries from.
        /// </param>
        /// <param name="offset">The zero-based offset to start the page at.
        /// </param>
        /// <param name="limit">The number of entries to retrieve past the 
        /// offset.</param>
        /// <returns>A collection of suppression list entries.</returns>
        public Responses.SuppressionCollection GetSuppressionListEntries(SuppressionType listType, int offset, int limit)
        {
            return this.GetSuppressionListEntries(listType, offset, limit, "");
        }

        /// <summary>
        /// Get a collection of suppression list entries.
        /// </summary>
        /// <param name="listType">The type of list to get entries from.
        /// </param>
        /// <param name="offset">The zero-based offset to start the page at.
        /// </param>
        /// <param name="limit">The number of entries to retrieve past the 
        /// offset.</param>
        /// <param name="query">A query to filter / search for records in
        /// the list. See the API documentation for the query format and 
        /// queryable fields.</param>
        /// <returns>A collection of suppression list entries.</returns>
        public Responses.SuppressionCollection GetSuppressionListEntries(SuppressionType listType, int offset, int limit, string query)
        {
            Dictionary<string, string> queryParameters = new Dictionary<string, string>();
            queryParameters.Add("offset", offset.ToString());
            queryParameters.Add("limit", limit.ToString());

            if (query.Length > 0)
            {
                queryParameters.Add("query", query);
            }

            string list = ContactManagerAPI.SuppressionTypeToString(listType);

            return this.connection.Call<Responses.SuppressionCollection>("GET", "suppression/" + list, queryParameters, null);
        }
        #endregion

        /// <summary>
        /// Add one or more entries to a suppression list.
        /// </summary>
        /// <param name="listType">The suppression list type (email, etc).
        /// </param>
        /// <param name="entries">An array of entries (emails, phone 
        /// numbers, etc).</param>
        /// <returns>A relay of the added records as suppression entries.
        /// </returns>
        public Responses.SuppressionCollection AddToSuppressionList(SuppressionType listType, string[] entries)
        {
            Requests.SuppressionEntries entry = new Requests.SuppressionEntries();
            entry.Values = entries;
            string list = ContactManagerAPI.SuppressionTypeToString(listType);
            return this.connection.Call<Responses.SuppressionCollection>("POST", "suppression/" + list, null, entry); 
        }

        private static string SuppressionTypeToString(SuppressionType type) 
        {
            string list = "";
            switch (type)
            {
                case SuppressionType.Email:
                    list = "email";
                    break;
                case SuppressionType.Fax:
                    list = "fax";
                    break;
                case SuppressionType.Mobile:
                    list = "mobile";
                    break;
                case SuppressionType.Phone:
                    list = "phone";
                    break;
            }

            return list;
        }

        #endregion
    }
}
