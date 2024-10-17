using System;

namespace Address_Book_ADO.NET
{
    internal class AddressBookMain
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the address book project");
            ContactRepo contactRepo = new ContactRepo();
            //contactRepo.CreateContactTable();
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("- - - - - - - - - - - - -");
                Console.WriteLine("MENU");
                Console.WriteLine("- - - - - - - - - - - - -");
                Console.WriteLine("1.Add contact\n2.Update Contact\n3.Delete Contact");
                Console.WriteLine("- - - - - - - - - - - - -");
                Console.WriteLine("Enter your choice");
                int choice=Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1: Console.WriteLine("Enter FirstName");
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
                        contactRepo.AddContacts(firstname, lastname, address, city, state, zip, phone, email);
                        break;
                    case 2: Console.WriteLine("Enter the firstname of the contact to be updated");
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
                        contactRepo.UpdateContact(firstname1 , lastname1, newfirstname,newlastname,address1,city1,state1,zip1,phone1,email1);   
                        break;
                    case 3: Console.WriteLine("Enter the firstname");
                        string firstname2=Console.ReadLine();
                        Console.WriteLine("Enter the lastname");
                        string lastname2=Console.ReadLine();
                        contactRepo.DeleteContact(firstname2, lastname2);
                        break;

                }
                
            }
        }
    }
}
