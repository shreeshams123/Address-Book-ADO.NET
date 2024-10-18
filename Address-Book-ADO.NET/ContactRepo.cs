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
namespace Address_Book_ADO.NET
{
    public class ContactRepo
    {
        private string connectionstring;
        public ContactRepo(string _connectionstring) {

            connectionstring = _connectionstring;
        }
        
        public void AddContacts(string firstname,string lastname,string address,string city,string state,int zip,long phone,string email,string addressbookname)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                string query1 = @"Select count(*) from AddressBookTable where Name=@Name";
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query1, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", addressbookname);
                    int count = (int)cmd.ExecuteScalar();
                    if (count <= 0) { 
                    Console.WriteLine("Address Book not found");
                    return;
                }
                }
            }
            
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                string query2 = "INSERT INTO Contacts (FirstName, LastName, Address, City, State, Zip, Phone, Email,AddressBookName) VALUES (@FirstName, @LastName, @Address, @City, @State, @Zip, @Phone, @Email,@AddressBookName)";
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query2, con))
                {
                    
                    cmd.Parameters.AddWithValue("@FirstName",firstname);
                    cmd.Parameters.AddWithValue("@LastName", lastname);
                    cmd.Parameters.AddWithValue("@Address",address);
                    cmd.Parameters.AddWithValue("@City",city);
                    cmd.Parameters.AddWithValue("@State",state);
                    cmd.Parameters.AddWithValue("@Zip",zip);
                    cmd.Parameters.AddWithValue("@Phone",phone);
                    cmd.Parameters.AddWithValue("@Email",email);
                    cmd.Parameters.AddWithValue("@AddressBookName", addressbookname);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Contact added successfully");
                }
            }
        }
        public void UpdateContact(string firstname, string lastname,string newfirstname,string newlastname, string address, string city, string state, int zip, long phone, string email,string addressbookname)
        {
            string query1 = "Select count(*) from Contacts where FirstName=@FirstName and LastName=@LastName";
            using(SqlConnection con=new SqlConnection(connectionstring))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query1, con))
                {
                    cmd.Parameters.AddWithValue("@FirstName", firstname);
                    cmd.Parameters.AddWithValue("@LastName",lastname);
                    int count = (int)cmd.ExecuteScalar();
                    if (count <= 0)
                    {
                        Console.WriteLine("Contact not found");
                        return;
                    }
                }
            }
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                string query2 = @"Select count(*) from AddressBookTable where Name=@Name";
                con.Open();
                using(SqlCommand cmd = new SqlCommand(query2, con))
                {
                    cmd.Parameters.AddWithValue("@Name", addressbookname);
                    int count= (int)cmd.ExecuteScalar();
                    if(count <= 0)
                    {
                        Console.WriteLine("Address book not found");
                        return;
                    }
                }
            }
            
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
                    cmd.Parameters.AddWithValue("@AddressBookName",addressbookname);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Contact Updated successfully");
                }

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
                    Console.WriteLine(count+ " row deleted" );
                    Console.WriteLine("Contact deleted successfully");
                }
            }
            
        }
    }
}
