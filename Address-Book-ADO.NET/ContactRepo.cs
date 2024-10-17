using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
namespace Address_Book_ADO.NET
{
    public class ContactRepo
    {
        private string connectionstring;
        public ContactRepo() {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true,reloadOnChange: true).Build();
            connectionstring = config.GetConnectionString("DefaultConnection");
        }
        public void CreateContactTable()
        {
            string query =@"Create Table Contacts(FirstName Varchar(20) primary key, LastName Varchar(20),Address Varchar(50),City Varchar(10),State Varchar(20),Zip INT,Phone BIGINT,Email Varchar(50))";
            using (SqlConnection con = new SqlConnection(connectionstring)) {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Table Created Successfully");
                };
             };
        

        }
        public void AddContacts(string firstname,string lastname,string address,string city,string state,int zip,long phone,string email)
        {
            string query= "INSERT INTO Contacts (FirstName, LastName, Address, City, State, Zip, Phone, Email) VALUES (@FirstName, @LastName, @Address, @City, @State, @Zip, @Phone, @Email)";
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    
                    cmd.Parameters.AddWithValue("@FirstName",firstname);
                    cmd.Parameters.AddWithValue("@LastName", lastname);
                    cmd.Parameters.AddWithValue("@Address",address);
                    cmd.Parameters.AddWithValue("@City",city);
                    cmd.Parameters.AddWithValue("@State",state);
                    cmd.Parameters.AddWithValue("@Zip",zip);
                    cmd.Parameters.AddWithValue("@Phone",phone);
                    cmd.Parameters.AddWithValue("@Email",email);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Contact added successfully");
                }
            }
        }
    }
}
