using System;

namespace ContactsBackEnd.Entities
{
    public class Contact
    {
        public Contact()
        {
            
        }

        public Contact(string firstName, string lastName, string address, int zipCode, string city, string telephone, string email, DateTime? birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            ZipCode = zipCode;
            City = city;
            Telephone = telephone;
            Email = email;
            BirthDate = birthDate;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
