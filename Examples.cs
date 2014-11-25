//----------------------------------------------------------------
// Examples.cs
// Copyright SimplyCast 2014
// This projected is licensed under the terms of the MIT license.
//  (see the attached LICENSE.txt).
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplyCast
{
    /// <summary>
    /// This class provides example functions that run through the 
    /// functionality provided by the C# API wrapper.
    /// </summary>
    public class Examples
    {
        private SimplyCastAPI api;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="apiHandle"></param>
        public Examples(SimplyCastAPI apiHandle)
        {
            this.api = apiHandle;
        }

        /// <summary>
        /// List management resource examples.
        /// </summary>
        public void ListManagementExample()
        {
            //Create a new list.
            ContactManager.Responses.ListEntity list = api.ContactManager.CreateList("My new list");

            Console.WriteLine("Created list " + list.ID);


            //Trivially retrieve the new list again, using the ID.
            list = api.ContactManager.GetList(list.ID);

            Console.WriteLine("Retrieved list " + list.ID + " by ID");


            //Retrieve the same list again, this time by name. Since lists can 
            //possibly share a name, error conditions are avoided by returning 
            //a collection of lists instead of a single list.
            ContactManager.Responses.ListCollection lists = api.ContactManager.GetListsByName("My new list");
            if (lists.Lists.Length > 0)
            {
                list = lists.Lists[0];
            }

            Console.WriteLine("Retrieved list " + list.ID + " by name");


            //Create a new contact and add it to our list. You can also add 
            //lists upon creation of a contact as the second parameter 
            //(example in the contact management example).
            ContactManager.Responses.ColumnCollection columns = api.ContactManager.GetColumns();
            Dictionary<string, string> fields = new Dictionary<string, string>();

            //Fields are added by column ID, not by the name of the column. You
            //can get the ID of a column by calling GetByName() on a column 
            //collection.
            fields.Add(columns.GetByName("email").ID, "test@example.com");

            ContactManager.Responses.ContactEntity contact = api.ContactManager.CreateContact(fields);
            
            api.ContactManager.AddContactsToList(list.ID, new int[] { contact.ID });

            Console.WriteLine("Added contact " + contact.ID + " to list " + list.ID);


            //Rename our list.
            list = api.ContactManager.RenameList(list.ID, "New list name.");

            Console.WriteLine("Renamed list to '" + list.Name + "'");


            //Delete the list. This doesn't delete the contacts within the 
            //list; they'll still exist and may still be in other extant 
            //lists.
            api.ContactManager.DeleteList(list.ID);


            Console.WriteLine("Deleted list.");

            Console.Write("Press enter to continue...");
            Console.Read();
        }

        /// <summary>
        /// Examples of contact management resources. 
        /// </summary>
        public void ContactManagementExample()
        {
            ContactManager.Responses.ListEntity list;

            //Create a testing list.
            try
            {
                list = api.ContactManager.CreateList("test list");
            }
            catch (APIException e)
            {
                list = api.ContactManager.GetListsByName("test list").Lists[0];
            }

            Console.WriteLine("Created test list " + list.ID);


            //Retrieve our column mapping. We can use this to find out the
            //column IDs we'll need when setting contact fields. 
            ContactManager.Responses.ColumnCollection columns = api.ContactManager.GetColumns();

            //Build up our contact data. Notice that we're using the column ID
            //as the field key, *not* the column name.
            Dictionary<string, string> fields = new Dictionary<string, string>();

            fields.Add(columns.GetByName("email").ID, "test@example.com");
            fields.Add(columns.GetByName("name").ID, "Test Contact");

            //Create a new contact, and assign them to the test list at the
            //same time.
            ContactManager.Responses.ContactEntity contact = api.ContactManager.CreateContact(fields, new int[] { list.ID });

            Console.WriteLine("Created contact " + contact.ID);

            //Run a query on the contact database to retrieve our contact. 
            //A contrived example, but an example nonetheless. The syntax 
            //is detailed in the online documentation, but basically looks
            //something like: '`colID` = "value" AND `col2ID` = "other value"'
            string query = "`" + columns.GetByName("email").ID + "` = \"test@example.com\" AND `"
                + columns.GetByName("name").ID + "` = \"Test Contact\"";
            Console.WriteLine("Executing query: " + query);
            ContactManager.Responses.ContactCollection queryResult = api.ContactManager.GetContacts(0, 100, query);

            if (queryResult.Contacts.Length > 0)
            {
                Console.WriteLine("Ran search query and found " + queryResult.Contacts.Length + " contacts");
            }
            else
            {
                Console.WriteLine("Ran search query and found no contacts.");
            }

            //Retrieve a field by its name, and get the value from it.
            string email = contact.GetFieldsByName("email")[0].Value;
            Console.WriteLine("\tContact email: " + email);

            //Output each list that the contact belongs to (should only be our
            //test list).
            foreach (ContactManager.Responses.ListEntity l in contact.Lists)
            {
                Console.WriteLine("\tContact belongs to list " + l.ID);
            }


            //Change the contact's phone number.
            Dictionary<string, string> updateFields = new Dictionary<string, string>();
            updateFields.Add(columns.GetByName("phone").ID, "15555551234");

            contact = api.ContactManager.UpdateContact(contact.ID, updateFields);

            Console.WriteLine("Updated phone number: " + contact.GetFieldsByName("phone")[0].Value);

            //Add tags to a contact.
            ContactManager.Responses.MetadataColumnCollection metadataColumns = api.ContactManager.GetMetadataColumns();
            string metadataFieldID = metadataColumns.GetByName("Tags").ID;

            api.ContactManager.UpdateContactMetadataField(contact.ID, metadataFieldID, new string[] { "tag 1", "tag 2" });

            Console.WriteLine("Updated metadata tags");

            //Get metadata tags and display them.
            ContactManager.Responses.MetadataFieldEntity metadataField = api.ContactManager.GetContactMetadataFieldByName(contact.ID, "Tags").MetadataFields[0];
            Console.WriteLine("Retrieved metadata tags.");

            foreach(string value in metadataField.Values) {
                Console.WriteLine("\tMetadata tag: " + value);
            }

            //Delete our test list
            api.ContactManager.DeleteList(list.ID);

            Console.WriteLine("Deleted test list");

            //Refresh the contact and see if it still belongs to the list.
            contact = api.ContactManager.GetContact(contact.ID);

            Console.WriteLine("Contact belongs to " + contact.Lists.Length + " lists");


            api.ContactManager.DeleteContact(contact.ID);

            Console.WriteLine("Deleted contact.");

            Console.Write("Press enter to continue...");
            Console.Read();
        }

        /// <summary>
        /// Examples of the column management resources.
        /// 
        /// Currently, there aren't many operations that can be done on
        /// columns via the API. Columns can only be created and 
        /// retrieved. If you have to create many columns, it is 
        /// recommended to use metadata columns instead. They're much
        /// more flexible and it is possible to do things like 
        /// delete them. For example, things like email address should
        /// be standard columns, things like tags and lead scoring should
        /// be metadata.
        /// 
        /// WARNING: running this example will result in unreversable 
        /// changes - the created column cannot be deleted.
        /// </summary>
        public void ColumnManagementExample()
        {
            ContactManager.Responses.ColumnCollection columns = api.ContactManager.GetColumns();
            ContactManager.Responses.ColumnEntity column;

            //Create the test column if it doesn't exist yet, or load it if it
            //does.
            if (columns.GetByName("test column") == null)
            {
                column = api.ContactManager.CreateColumn("test column", ContactManager.ContactManagerAPI.ColumnType.String);
                Console.WriteLine("Created column " + column.ID);
            }
            else
            {
                column = columns.GetByName("test column");
                Console.WriteLine("Retrieved column " + column.ID);
            }

            //Replace all merge tags with %%MERGE TAG%%. Merge tags replace 
            //into content with field data.
            api.ContactManager.UpdateMergeTags(column.ID, new string[] {"%%MERGE TAG%%"}, false);

            Console.WriteLine("Updated merge tags.");

            //Reload the column.
            column = api.ContactManager.GetColumn(column.ID);

            Console.WriteLine("Reloaded column.");

            //Display each merge tag.
            foreach (string mergeTag in column.MergeTags)
            {
                Console.WriteLine("\tMerge tag: " + mergeTag);
            }

            Console.Write("Press enter to continue...");
            Console.Read();
        }

        /// <summary>
        /// Metadata column management examples.
        /// </summary>
        public void MetadataColumnManagementExample()
        {
            //Create a new contact to test with.
            ContactManager.Responses.ColumnCollection columns = api.ContactManager.GetColumns();

            Dictionary<string, string> fields = new Dictionary<string, string>();

            fields.Add(columns.GetByName("email").ID, "test@example.com");
            fields.Add(columns.GetByName("name").ID, "Test Contact");

            ContactManager.Responses.ContactEntity contact = api.ContactManager.CreateContact(fields);

            Console.WriteLine("Created test contact " + contact.ID);

            ContactManager.Responses.MetadataColumnCollection metadataColumns = api.ContactManager.GetMetadataColumns();
            Console.WriteLine("Retrieved all metadata columns.");

            //Add new metadata columns.
            if (metadataColumns.GetByName("Test Single Column") == null)
            {
                api.ContactManager.CreateMetadataColumn("Test Single Column", ContactManager.ContactManagerAPI.MetadataColumnType.SingleNumber);
            }
            if (metadataColumns.GetByName("Test Multi Column") == null)
            {
                api.ContactManager.CreateMetadataColumn("Test Multi Column", ContactManager.ContactManagerAPI.MetadataColumnType.MultiString);
            }
            if (metadataColumns.GetByName("Test Sum Column") == null)
            {
                api.ContactManager.CreateMetadataColumn("Test Sum Column", ContactManager.ContactManagerAPI.MetadataColumnType.SumNumber);
            }

            Console.WriteLine("Created metadata test columns.");

            //Update our single number field to have the value 2.
            ContactManager.Responses.MetadataFieldEntity singleColumn = 
                api.ContactManager.UpdateContactMetadataField(contact.ID, metadataColumns.GetByName("Test Single Column").ID, new string[] { "2" });
             
            Console.WriteLine("Updated single column field with one integer.");
            Console.WriteLine("Value: " + singleColumn.Values[0]);

            //Update our multi-string field to have two values.
            ContactManager.Responses.MetadataFieldEntity multiColumn =
                api.ContactManager.UpdateContactMetadataField(contact.ID, metadataColumns.GetByName("Test Multi Column").ID, new string[] { "test multi value 1" , "test multi value 2" });

            Console.WriteLine("Updated multi-valued field with two strings.");
            foreach (string value in multiColumn.Values)
            {
                Console.WriteLine("Value: " + value);
            }

            //"Sum number" fields are incrementers. Updating the field with a
            //number will actually add to the existing value, rather than 
            //replacing it.
            ContactManager.Responses.MetadataFieldEntity sumColumn = new ContactManager.Responses.MetadataFieldEntity();
            for (int i = 0; i < 4; i++)
            {
                sumColumn = api.ContactManager.UpdateContactMetadataField(contact.ID, metadataColumns.GetByName("Test Sum Column").ID, new string[] { "1" });
                Console.WriteLine("Incremented sum number field by 1");
                Console.WriteLine("Value: " + sumColumn.Values[0]);
            }

            api.ContactManager.DeleteMetadataColumn(singleColumn.ID);
            api.ContactManager.DeleteMetadataColumn(multiColumn.ID);
            api.ContactManager.DeleteMetadataColumn(sumColumn.ID);

            Console.WriteLine("Removed test metadata columns.");

            api.ContactManager.DeleteContact(contact.ID);

            Console.WriteLine("Removed test contact " + contact.ID);

            Console.Write("Press enter to continue...");
            Console.Read();
        }

        /// <summary>
        /// 360 Examples. This example requires you to create a test 360 
        /// project that contains an inbound and outbound 360 API connection
        /// node, and the ID of a contact to test with.
        /// 
        /// NOTE that this will remove things from outbound nodes. Make sure
        /// your project you test with is indeed a testing project.
        /// </summary>
        public void SimplyCast360Example(int contactID, int projectID, int inboundConnectionID, int outboundConnectionID)
        {
            SimplyCast360.Responses.ProjectEntity project = api.SimplyCast360.GetProject(projectID);

            Console.WriteLine("Loaded 360 project '" + project.Name + "'");

            ContactManager.Responses.ContactEntity contact = api.ContactManager.GetContact(contactID);

            Console.WriteLine("Loaded contact " + contact.ID);

            int listID = 0;

            if (contact.Lists.Length == 0)
            {
                throw new Exception("The test contact must belong to at least one list.");
            }

            listID = contact.Lists[0].ID;

            api.SimplyCast360.PushContact(projectID, inboundConnectionID, listID, contactID);

            Console.WriteLine("Pushed contact to inbound node.");

            Console.WriteLine("Reading from outbound node to see if any contacts have arrived");

            SimplyCast360.Responses.ContactCollection contacts = api.SimplyCast360.GetOutboundContacts(projectID, outboundConnectionID);

            //Iterate through each contact and remove them from the outbound
            //node. 
            if (contacts != null && contacts.Contacts.Length > 0)
            {
                foreach (SimplyCast360.Responses.ContactEntity c in contacts.Contacts)
                {
                    Console.WriteLine("Found contact " + c.ContactID);

                    api.SimplyCast360.DeleteContact(projectID, outboundConnectionID, c.ID);
                    Console.WriteLine("Removed contact " + c.ContactID + " (360 ID " + c.ID + ") from outbound node.");
                }
            }
            else
            {
                Console.WriteLine("No contacts in outbound node.");
            }

            Console.Write("Press enter to continue...");
            Console.Read();
        }
    }
}
