using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Address_Book_ADO.NET
{
    internal class AddressBookMain
    {
        private static string _connectionstring;
        public AddressBookMain() {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            _connectionstring = config.GetConnectionString("DefaultConnection");
            Console.WriteLine($"Loaded connection string: {_connectionstring}");
            if (string.IsNullOrEmpty(_connectionstring))
            {
                throw new InvalidOperationException("Connection string is not initialized!");
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the address book project");
            AddressBookMain main = new AddressBookMain();
            ContactRepo contactRepo = new ContactRepo(_connectionstring);
            AddressRepo addressRepo = new AddressRepo(_connectionstring);
            
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("- - - - - - - - - - - - -");
                Console.WriteLine("MENU");
                Console.WriteLine("- - - - - - - - - - - - -");
                Console.WriteLine("1.Add Address Book\n2.Delete Address Book\n3.Add contact\n4.Update Contact\n5.Delete Contact\n6.Search By City\n7.Search By State\n8.View By City\n9.View By State\n10.Count By City\n11.Count By State\n12.Order contacts by name\n13.Order contacts by City\n14.Order Contacts by State\n15.Order Contacts By ZipCode\n16.Exit\n");
                Console.WriteLine("- - - - - - - - - - - - -");
                Console.WriteLine("Enter your choice");
                int choice=Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1: Console.WriteLine("Enter the name of the addressbook");
                        string addname= Console.ReadLine();
                        addressRepo.AddAddressBook(addname);
                        break;
                    case 2: Console.WriteLine("Enter the name of the addressbook");
                        string addname1= Console.ReadLine();
                        addressRepo.DeleteContactsFromAddressBook(addname1);
                        break;
                    case 3: Console.WriteLine("Enter FirstName");
                        string firstname= Console.ReadLine();
                        Console.WriteLine("Enter Lastname");
                        string lastname= Console.ReadLine();
                        Console.WriteLine("Enter Address");
                        string address= Console.ReadLine();
                        Console.WriteLine("Enter City");
                        string city= Console.ReadLine();
                        Console.WriteLine("Enter state");
                        string state= Console.ReadLine();
                        Console.WriteLine("Enter zip");
                        int zip=Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter phone number");
                        long phone=Convert.ToInt64(Console.ReadLine());
                        Console.WriteLine("Enter email id");
                        string email= Console.ReadLine();
                        Console.WriteLine("Enter address book name");
                        string addressbookname= Console.ReadLine();
                        contactRepo.AddContacts(firstname, lastname, address, city, state, zip, phone, email,addressbookname);
                        break;
                    case 4: Console.WriteLine("Enter the firstname of the contact to be updated");
                        string firstname1=Console.ReadLine();
                        Console.WriteLine("Enter the lastname of the contact to update");
                        string lastname1=Console.ReadLine();
                        Console.WriteLine("Enter the new first name");
                        string newfirstname=Console.ReadLine();
                        Console.WriteLine("Enter the new last name");
                        string newlastname=Console.ReadLine();
                        Console.WriteLine("Enter address");
                        string address1=Console.ReadLine();
                        Console.WriteLine("Enter the city");
                        string city1=Console.ReadLine();
                        Console.WriteLine("Enter state");
                        string state1=Console.ReadLine();
                        Console.WriteLine("Enter Zip");
                        int zip1=Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter phone number");
                        long phone1= Convert.ToInt64(Console.ReadLine());
                        Console.WriteLine("Enter Email");
                        string email1=Console.ReadLine();
                        Console.WriteLine("Enter the name of the address book");
                        string addressbookname1=Console.ReadLine();
                        contactRepo.UpdateContact(firstname1 , lastname1, newfirstname,newlastname,address1,city1,state1,zip1,phone1,email1,addressbookname1);   
                        break;
                    case 5: Console.WriteLine("Enter the firstname");
                        string firstname2=Console.ReadLine();
                        Console.WriteLine("Enter the lastname");
                        string lastname2=Console.ReadLine();
                        contactRepo.DeleteContact(firstname2, lastname2);
                        break;
                    case 6: Console.WriteLine("Enter the city");
                        string city2=Console.ReadLine();
                        contactRepo.SearchByCity(city2);
                        break;
                    case 7: Console.WriteLine("Enter the state");
                        string state2= Console.ReadLine();
                        contactRepo.SearchByState(state2);
                        break;
                    case 8: contactRepo.ViewByCity();
                        break;
                    case 9: contactRepo.ViewByState();
                        break;
                    case 10: Console.WriteLine("Enter City");
                        string city3=Console.ReadLine();
                        contactRepo.CountByCity(city3);
                        break;
                    case 11: Console.WriteLine("Enter State");
                        string state3=Console.ReadLine();
                        contactRepo.CountByState(state3);
                        break;
                    case 12: Console.WriteLine("Enter address book name");
                        string addressbook=Console.ReadLine();
                        contactRepo.OrderByName(addressbook);
                        break;
                    case 13: Console.WriteLine("Enter address book name");
                        string addressbook1=Console.ReadLine();
                        contactRepo.OrderByCity(addressbook1);
                        break;
                    case 14: Console.WriteLine("Enter address book name");
                        string addressbook2=Console.ReadLine();
                        contactRepo.OrderByState(addressbook2);
                        break;
                    case 15: Console.WriteLine("Enter address book name");
                        string addressbook3=Console.ReadLine();
                        contactRepo.OrderByZip(addressbook3);   
                        break;
                    case 16: flag = false;
                        break;
                    default: Console.WriteLine("Invalid input");
                        break;
                }
                
            }
        }
    }
}
