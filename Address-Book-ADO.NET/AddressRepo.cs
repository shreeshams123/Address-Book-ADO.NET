using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Address_Book_ADO.NET
{
    
    internal class AddressRepo
    {
        private static string connectionstring;
        public AddressRepo(string _connectionstring) {
            connectionstring = _connectionstring;
        }
        public static bool AddressBookPresent(string addressbookname)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                string query1 = @"Select count(*) from AddressBookTable where Name=@Name";
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query1, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", addressbookname);
                    int count = (int)cmd.ExecuteScalar();
                    if (count <= 0)
                    {
                        return false; ;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
        public void AddAddressBook(string name)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                string query1 = @"Select count(*) from AddressBookTable where Name=@Name";
                con.Open();
                using(SqlCommand cmd=new SqlCommand(query1, con))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    int count=(int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        Console.WriteLine("Address Book already exists");
                        return;
                    }
                }

            }
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                string query2 = @"Insert into AddressBookTable(Name) Values(@Name)";
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query2, con))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("AddressBook added successfully");
                }
            }
        }
        public void DeleteAddressBook(string addressbookname)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                string query2 = "Delete from AddressBookTable where Name=@Name";
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query2, con))
                {
                    cmd.Parameters.AddWithValue("@Name", addressbookname);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Deleted address book successfully");
                }
            }
        }
        public void DeleteContactsFromAddressBook(string addressbookname)
        {
            if (AddressBookPresent(addressbookname))
            {
                int count;
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    string query = @"Select count(*) from Contacts where AddressBookName=@AddressBookName";
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@AddressBookName", addressbookname);
                        count = (int)cmd.ExecuteScalar();
                    }
                }
                if (count >0)
                {
                    Console.WriteLine("There are " + (count) + " contacts in this addressbook do you want to delete the addressbook");
                    Console.WriteLine("Enter yes or no");
                    string answer = Console.ReadLine();
                    if (answer.Equals("yes", StringComparison.OrdinalIgnoreCase))
                    {
                        using (SqlConnection con = new SqlConnection(connectionstring))
                        {
                            string query1 = "Delete from Contacts where AddressBookName=@AddressBookName";
                            con.Open();
                            using (SqlCommand cmd = new SqlCommand(query1, con))
                            {
                                cmd.Parameters.AddWithValue("@AddressBookName", addressbookname);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        DeleteAddressBook(addressbookname);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    DeleteAddressBook(addressbookname);
                }
            }
            else
            {
                Console.WriteLine("Address Book not present");
            }
        }

            }
        }
    

