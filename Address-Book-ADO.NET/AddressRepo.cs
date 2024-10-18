using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Address_Book_ADO.NET
{
    
    internal class AddressRepo
    {
        private string connectionstring;
        public AddressRepo(string _connectionstring) {
            connectionstring = _connectionstring;
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
    }
}
