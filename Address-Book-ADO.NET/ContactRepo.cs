using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Threading;
using System.ComponentModel.Design;
namespace Address_Book_ADO.NET
{
    public class ContactRepo
    {
        private string connectionstring;
        public ContactRepo(string _connectionstring) {

            connectionstring = _connectionstring;
        }
        public bool ContactPresent(string firstname,string lastname)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                string query = "Select count(*) from Contacts where FirstName=@FirstName and LastName=@LastName";
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@FirstName",firstname);
                    cmd.Parameters.AddWithValue("@LastName",lastname);
                   int count=(int) cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        
        
        public void AddContacts(string firstname,string lastname,string address,string city,string state,int zip,long phone,string email,string addressbookname)
        {
            if (!ContactPresent(firstname, lastname))
            {
                if (AddressRepo.AddressBookPresent(addressbookname))
                {

                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        string query2 = "INSERT INTO Contacts (FirstName, LastName, Address, City, State, Zip, Phone, Email,AddressBookName) VALUES (@FirstName, @LastName, @Address, @City, @State, @Zip, @Phone, @Email,@AddressBookName)";
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand(query2, con))
                        {

                            cmd.Parameters.AddWithValue("@FirstName", firstname);
                            cmd.Parameters.AddWithValue("@LastName", lastname);
                            cmd.Parameters.AddWithValue("@Address", address);
                            cmd.Parameters.AddWithValue("@City", city);
                            cmd.Parameters.AddWithValue("@State", state);
                            cmd.Parameters.AddWithValue("@Zip", zip);
                            cmd.Parameters.AddWithValue("@Phone", phone);
                            cmd.Parameters.AddWithValue("@Email", email);
                            cmd.Parameters.AddWithValue("@AddressBookName", addressbookname);
                            cmd.ExecuteNonQuery();
                            Console.WriteLine("Contact added successfully");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Address Book not found");
                }
            }
            else
            {
                Console.WriteLine("Contact already exist");
            }
        }
        public void UpdateContact(string firstname, string lastname,string newfirstname,string newlastname, string address, string city, string state, int zip, long phone, string email,string addressbookname)
        {
            if (ContactPresent(firstname, lastname)){
                if (AddressRepo.AddressBookPresent(addressbookname))
                {

                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        string query3 = "Update Contacts set Firstname=@NewFirstName,LastName=@NewLastName,Address=@Address,City=@City,State=@State,Zip=@Zip,Phone=@Phone,Email=@Email,AddressBookName=@AddressBookName where FirstName=@FirstName and LastName=@LastName";
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand(query3, con))
                        {
                            cmd.Parameters.AddWithValue("@FirstName", firstname);
                            cmd.Parameters.AddWithValue("@LastName", lastname);
                            cmd.Parameters.AddWithValue("@NewFirstName", newfirstname);
                            cmd.Parameters.AddWithValue("@NewLastName", newlastname);
                            cmd.Parameters.AddWithValue("@Address", address);
                            cmd.Parameters.AddWithValue("@City", city);
                            cmd.Parameters.AddWithValue("@State", state);
                            cmd.Parameters.AddWithValue("@Zip", zip);
                            cmd.Parameters.AddWithValue("@Phone", phone);
                            cmd.Parameters.AddWithValue("@Email", email);
                            cmd.Parameters.AddWithValue("@AddressBookName", addressbookname);
                            cmd.ExecuteNonQuery();
                            Console.WriteLine("Contact Updated successfully");
                        }

                    }
                }
                else
                {
                    Console.WriteLine("Address Book not present");
                }
            }
            else
            {
                Console.WriteLine("Contact not found");
            }
        }
        public void DeleteContact(string firstname, string lastname)
        {
            string query1 = @"Select count(*) from Contacts where FirstName=@FirstName and LastName=@LastName";
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query1, con))
                {
                    cmd.Parameters.AddWithValue("@FirstName", firstname);
                    cmd.Parameters.AddWithValue("@LastName", lastname);
                    int count=(int)cmd.ExecuteNonQuery ();
                    if (count>0)
                    {
                        Console.WriteLine("Contact not found");
                        return;
                    }
                }
            }
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                string query2 = @"Delete from Contacts where FirstName=@FirstName and LastName=@LastName";
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query2, con))
                {
                    cmd.Parameters.AddWithValue("@FirstName", firstname);
                    cmd.Parameters.AddWithValue("@LastName", lastname);
                    int count=(int)cmd.ExecuteNonQuery() ;
                    Console.WriteLine((count+1)+ " row deleted" );
                    Console.WriteLine("Contact deleted successfully");
                }
            }
            
        }
        public void SearchByCity(string city)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                string query = @"Select count(*) from Contacts where City=@City";
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@City", city);
                    int count = (int)cmd.ExecuteScalar();
                    if (count <= 0)
                    {
                        Console.WriteLine("No contacts in this city");
                        return;
                    }
                }
            }
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                string query = @"Select FirstName,LastName from Contacts where City=@City ";
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@City", city);
                   using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader["FirstName"]+" " + reader["LastName"]);
                        }
                    }
                }
            }
        }
        public void SearchByState(string state)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                string query = @"Select count(*) from Contacts where State=@State";
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@State", state);
                    int count = (int)cmd.ExecuteScalar();
                    if (count <= 0)
                    {
                        Console.WriteLine("No Contacts in this state");
                        return;
                    }
                }
            }
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                string query = @"Select FirstName,LastName from Contacts where State=@State ";
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@State", state);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader["FirstName"] + " " + reader["LastName"]);
                        }
                    }
                }
            }
        }
        public void ViewByCity()
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                string query = @"SELECT City, FirstName, LastName FROM Contacts ORDER BY City ";
                con.Open();
                using(SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        string CurrentCity = null;
                        while (reader.Read())
                        {
                            string city = reader["City"].ToString();
                            if (city != CurrentCity)
                            {
                                CurrentCity = city;
                                Console.WriteLine("CITY: "+reader["City"]);
                                Console.WriteLine("---------------");
                            }
                            Console.WriteLine(reader["FirstName"]+" " + reader["LastName"]);
                        }
                    }
                }
            }
        }
        public void ViewByState()
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                string query = @"Select State,FirstName,LastName from Contacts order by State";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        string CurrentState = null;
                        while (reader.Read())
                        {
                            string state = reader["State"].ToString();
                            if (state != CurrentState)
                            {
                                CurrentState = state;
                                Console.WriteLine("State: " + state);
                                Console.WriteLine("---------------");
                            }
                            Console.WriteLine(reader["FirstName"]+" "+reader["LastName"]);
                        }
                    }
                }
            }
        }
    }
}
