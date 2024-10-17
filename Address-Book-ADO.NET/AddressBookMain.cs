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
                Console.WriteLine("MENU");
                Console.WriteLine("1.Add contact");
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

                }
                
            }
        }
    }
}
