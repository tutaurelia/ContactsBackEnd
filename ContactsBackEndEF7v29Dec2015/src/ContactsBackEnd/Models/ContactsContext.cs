using Microsoft.Data.Entity;

namespace ContactsBackEnd.Models
{
    public class ContactsContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
    }
}
