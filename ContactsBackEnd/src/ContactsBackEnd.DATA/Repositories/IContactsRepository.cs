using System.Collections.Generic;
using ContactsBackEnd.DATA.Entities;

namespace ContactsBackEnd.DATA.Repositories
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
    }
}
