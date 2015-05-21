using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ContactsBackEnd.DATA.Entities;

namespace ContactsBackEnd.DATA.Repositories
{
    public class ContactsRepository : IContactsRepository
    {
        private SqlConnection _conn;

        private const string ConnString = "Data Source=(localdb)\\ProjectsV12;Initial Catalog=ContactsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        
        private readonly string _selectSql = "SELECT Id, FirstName, LastName, [Address], ZipCode, City, Telephone, Email, BirthDate FROM ( SELECT Id, FirstName, LastName, [Address], ZipCode, City, Telephone, Email, BirthDate, ROW_NUMBER() over (order by Id asc) as RowNumber " +
            "FROM (SELECT   Id, FirstName, LastName, [Address], ZipCode, City, Telephone, Email, BirthDate  FROM dbo.Contacts where FirstName LIKE @query OR LastName LIKE @query) as T) as u where RowNumber >=@StartRow AND RowNumber <= @EndRow";

        public IList<Contact> GetAllContacts(string query, int page, int pageSize)
        {
            var contacts = new List<Contact>();
            try
            {
                _conn = new SqlConnection(ConnString);
                _conn.Open();

                var cmd = new SqlCommand(_selectSql, _conn);

                var paramQuery = new SqlParameter
                {
                    ParameterName = "@query",
                    Value = "%" + query + "%"
                };
                cmd.Parameters.Add(paramQuery);

                var paramStartRow = new SqlParameter
                {
                    ParameterName = "@StartRow",
                    Value = page * pageSize
                };
                cmd.Parameters.Add(paramStartRow);

                var paramEndRow = new SqlParameter
                {
                    ParameterName = "@EndRow",
                    Value = (page * pageSize) + pageSize
                };
                cmd.Parameters.Add(paramEndRow);


                var rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var contact = new Contact()
                    {
                        Id = rdr.IsDBNull(rdr.GetOrdinal("Id")) ? -1 : rdr.GetInt32(rdr.GetOrdinal("Id")),
                        FirstName = rdr.IsDBNull(rdr.GetOrdinal("FirstName")) ? null : rdr.GetString(rdr.GetOrdinal("FirstName")),
                        LastName = rdr.IsDBNull(rdr.GetOrdinal("LastName")) ? "" : rdr.GetString(rdr.GetOrdinal("LastName")),
                        Address = rdr.IsDBNull(rdr.GetOrdinal("Address")) ? null : rdr.GetString(rdr.GetOrdinal("Address")),
                        ZipCode = rdr.IsDBNull(rdr.GetOrdinal("ZipCode")) ? -1 : rdr.GetInt32(rdr.GetOrdinal("ZipCode")),
                        City = rdr.IsDBNull(rdr.GetOrdinal("City")) ? null : rdr.GetString(rdr.GetOrdinal("City")),
                        Telephone = rdr.IsDBNull(rdr.GetOrdinal("Telephone")) ? null : rdr.GetString(rdr.GetOrdinal("Telephone")),
                        Email = rdr.IsDBNull(rdr.GetOrdinal("Email")) ? null : rdr.GetString(rdr.GetOrdinal("Email")),
                        BirthDate = rdr.IsDBNull(rdr.GetOrdinal("BirthDate")) ? null as DateTime? : rdr.GetDateTime(rdr.GetOrdinal("BirthDate"))
                    };
                    contacts.Add(contact);
                }
            }
            finally
            {
                _conn?.Close();
            }
            return contacts;
        }





        public Contact GetContactById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Contact GetContactByEmail(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(Contact contact)
        {
            throw new System.NotImplementedException();
        }

        public void Update(int id, Contact contact)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
