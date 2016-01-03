using System.Collections.Generic;

namespace ContactsBackEnd.Models
{
    public interface IContactsRepository
    {
        IList<Contact> GetAllContacts(string query, int page, int pageSize);
        long GetNumberOfContacts(string query);
        bool ContactExists(string email);
        Contact GetContactById(int id);
        Contact GetContactByEmail(string email);
        long Insert(Contact contact);
        void Update(int id, Contact contact);
        void Delete(int id);
        void ResetDataBase10Contacts();
        void ResetDataBase1000Contacts();
    }
}
