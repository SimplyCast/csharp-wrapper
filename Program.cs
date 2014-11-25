using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

using SimplyCast;
using Responses = SimplyCast.ContactManager.Responses;
using Requests = SimplyCast.ContactManager.Requests;

using SC360Responses = SimplyCast.SimplyCast360.Responses;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string publicKey = "da9567b5fbd208807534dddb85f6e0e28073490e";
            string secretKey = "feb8d64b66619678738af5da5e7eb4f04f287de0";

            //Program.s(); return;


            SimplyCastAPI api = new SimplyCastAPI(publicKey, secretKey);

            SimplyCast.Examples e = new SimplyCast.Examples(api);
            e.MetadataColumnManagementExample();

            
            return;
        }

        public static void WriteColumn(object c)
        {
            if (c.GetType().ToString() == "SimplyCast.ContactManager.Responses.ColumnCollection")
            {
                Responses.ColumnCollection cc = (Responses.ColumnCollection) c;
                foreach (Responses.ColumnEntity ce in cc.Columns)
                {
                    Program.WriteColumnEntity(ce);
                }
            }
            else
            {
                Responses.ColumnEntity ce = (Responses.ColumnEntity) c;
                Program.WriteColumnEntity(ce);
            }
        }

        private static void WriteColumnEntity(Responses.ColumnEntity c)
        {
            string header = "Column \"" + c.ID + "\"";
            string properties = "";
            if (c.IsEditable)
            {
                properties += "editable, ";
            }
            if (c.IsVisible)
            {
                properties += "visible, ";
            }
            if (c.IsUserDefined)
            {
                properties += "user";
            }

            properties = properties.TrimEnd(new char[] { ' ', ',' });

            if (properties.Length > 0)
            {
                header += " (" + properties + ") ";
            }

            Console.WriteLine(header);
            Console.WriteLine("Type: " + c.Type);
            Console.WriteLine("Name: " + c.Name);

            if (c.MergeTags.Length > 0) {
                foreach (string m in c.MergeTags)
                {
                    Console.WriteLine("\tMerge: " + m);
                }
            }
        }

        public static void Write360Project(SC360Responses.ProjectEntity p)
        {
            Console.WriteLine("Project (" + p.ID + "):" + p.Name);
            foreach (SC360Responses.ConnectionEntity co in p.Connections)
            {
                Console.WriteLine("Connection (" + co.ID + "-" + co.Type + "): " + co.Name);
            }
        }

        public static void Write360Endpoint(SC360Responses.ConnectionEntity co)
        {
            Console.WriteLine("Connection (" + co.ID + "-" + co.Type + "): " + co.Name);
        }

        public static void WriteMetadata(object m)
        {
            if (m.GetType().ToString() == "SimplyCast.ContactManager.Responses.MetadataFieldCollection")
            {
                Responses.MetadataFieldCollection mc = (Responses.MetadataFieldCollection)m;
                foreach (Responses.MetadataFieldEntity e in mc.MetadataFields)
                {
                    Program.WriteMetadataEntry(e);
                }
            }
            else if (m.GetType().ToString() == "SimplyCast.ContactManager.Responses.MetadataFieldEntity")
            {
                Program.WriteMetadataEntry(m);
            }
            else if (m.GetType().ToString() == "SimplyCast.ContactManager.Responses.MetadataColumnCollection") 
            {
                Responses.MetadataColumnCollection mc = (Responses.MetadataColumnCollection)m;
                foreach (Responses.MetadataColumnEntity e in mc.MetadataColumns)
                {
                    Program.WriteMetadataEntry(e);
                }
            }
            else if (m.GetType().ToString() == "SimplyCast.ContactManager.Responses.MetadataColumnEntity") 
            {
                Program.WriteMetadataEntry(m);
            }
        }

        private static void WriteMetadataEntry(object e)
        {
            if (e.GetType().ToString() == "SimplyCast.ContactManager.Responses.MetadataFieldEntity")
            {
                Responses.MetadataFieldEntity ec = (Responses.MetadataFieldEntity)e;
         
                string header = "Field " + ec.ID;
                string properties = "";
                if (ec.IsEditable)
                {
                    properties += "editable, ";
                }
                if (ec.IsVisible)
                {
                    properties += "visible, ";
                }
                if (ec.IsUserDefined)
                {
                    properties += "user";
                }

                properties = properties.TrimEnd(new char[] {' ', ','});

                if (properties.Length > 0)
                {
                    header += " (" + properties + ") ";
                }

                Console.WriteLine(header);
                Console.WriteLine("Name: " + ec.Name);
                Console.WriteLine("Values: ");
                foreach(object o in ec.Values) {
                    Console.WriteLine("\t'" + o.ToString() + "'");
                }
            }
            else if (e.GetType().ToString() == "SimplyCast.ContactManager.Responses.MetadataColumnEntity")
            {
                Responses.MetadataColumnEntity ec = (Responses.MetadataColumnEntity)e;

                string header = "Field " + ec.ID;
                string properties = "";
                if (ec.IsEditable)
                {
                    properties += "editable, ";
                }
                if (ec.IsVisible)
                {
                    properties += "visible, ";
                }
                if (ec.IsUserDefined)
                {
                    properties += "user";
                }

                properties = properties.TrimEnd(new char[] { ' ', ',' });

                if (properties.Length > 0)
                {
                    header += " (" + properties + ") ";
                }

                Console.WriteLine(header);
                Console.WriteLine("Name: " + ec.Name);
            }
            Console.WriteLine("");
        }

        public static void WriteContact(Responses.ContactEntity e) 
        {
            Console.WriteLine("Contact " + e.ID);
            if (e.Fields.Length > 0)
            {
                Console.WriteLine("Fields:");
                foreach (Responses.FieldEntity f in e.Fields)
                {
                    if (f.Value.Trim().Length > 0)
                    {
                        Console.WriteLine("\t" + f.Name + ": " + f.Value);
                    }
                }
            }
            if (e.Lists.Length > 0)
            {
                Console.WriteLine("Lists:");
                foreach (Responses.ListEntity l in e.Lists)
                {
                    Console.WriteLine("\t" + l.ID);
                }
            }
        }
    }
}