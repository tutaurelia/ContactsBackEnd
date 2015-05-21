using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactsBackEnd.DATA.Entities;

namespace ContactsBackEnd.DATA.Repositories
{
    public interface IContactsRepository
    {
        IList<Contact> GetAllContacts(string id, int page, int pageSize);
        Contact GetContactById(int id);
        Contact GetContactByEmail(int id);
        void Insert(Contact contact);
        void Update(int id, Contact contact);
        void Delete(int id);
    }
}
