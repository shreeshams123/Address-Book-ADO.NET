using System;

namespace Address_Book_ADO.NET
{
    internal class AddressBookMain
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the address book project");
            ContactRepo contactRepo = new ContactRepo();
            contactRepo.CreateContactTable();
        }
    }
}
