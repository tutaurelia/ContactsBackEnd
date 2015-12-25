using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Threading.Tasks;
using ContactsBackEnd.Entities;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using DbContext = Microsoft.Data.Entity.DbContext;

namespace ContactsBackEnd.DAL
{
    public class ContactsContext : DbContext
    {
        public ContactsContext(DbContextOptions options) :base(options)
        {
            
        }

        public Microsoft.Data.Entity.DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Contact>().ToTable("Contacts");
        }

        

    }
}
