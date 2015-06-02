using System;
using System.Collections.Generic;
using System.Data;
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

        public long GetNumberOfContacts(string query)
        {
            try
            {
                const string sql = "SELECT COUNT(*) FROM Contacts where FirstName LIKE @query OR LastName LIKE @query";

                _conn = new SqlConnection(ConnString);
                _conn.Open();

                var cmd = new SqlCommand(sql, _conn);
                var paramQuery = new SqlParameter
                {
                    ParameterName = "@query",
                    Value = "%" + query + "%"
                };
                cmd.Parameters.Add(paramQuery);

                return long.Parse(cmd.ExecuteScalar().ToString());
            }
            finally
            {
                _conn?.Close();
            }
        }

        public bool ContactExists(string email)
        {
            try
            {
                const string sql = "SELECT COUNT([Id]) AS ContactsId FROM Contacts WHERE email = @Email;";

                _conn = new SqlConnection(ConnString);
                _conn.Open();

                var cmd = new SqlCommand(sql, _conn);
                cmd.Parameters.Add("@CEmail", SqlDbType.VarChar);
                cmd.Parameters["@Email"].Value = email;

                return ((int)cmd.ExecuteScalar() >= 1);
            }
            finally
            {
                _conn?.Close();
            }
        }

        public Contact GetContactById(int id)
        {
            Contact contact = null;
            try
            {
                _conn = new SqlConnection(ConnString);
                _conn.Open();

                const string sql = "SELECT Id, FirstName, LastName, [Address], ZipCode, City, Telephone, Email, BirthDate FROM Contacts WHERE [Id] = @Id;";

                var cmd = new SqlCommand(sql, _conn);

                var paramId = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = id
                };
                cmd.Parameters.Add(paramId);

                var rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    contact = new Contact
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
                }
            }
            finally
            {
                _conn?.Close();
            }
            return contact;
        }

        public Contact GetContactByEmail(string email)
        {
            Contact contact = null;
            try
            {
                _conn = new SqlConnection(ConnString);
                _conn.Open();

                const string sql = "SELECT Id, FirstName, LastName, [Address], ZipCode, City, Telephone, Email, BirthDate FROM Contacts WHERE [Email] = @Email;";

                var cmd = new SqlCommand(sql, _conn);

                var paramId = new SqlParameter
                {
                    ParameterName = "@Email",
                    Value = email
                };
                cmd.Parameters.Add(paramId);

                var rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    contact = new Contact
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
                }
            }
            finally
            {
                _conn?.Close();
            }
            return contact;
        }

        public long Insert(Contact contact)
        {
            try
            {
                var contactExists = GetContactByEmail(contact.Email);
                if (contactExists != null)
                {
                    throw new Exception($"Entity {contact.Email} already exists in database!");
                }
                _conn = new SqlConnection(ConnString);

                var cmd = _conn.CreateCommand();
                                
                cmd.CommandText =
                    @"INSERT INTO[dbo].[Contacts] (FirstName, LastName, [Address], ZipCode, City, Telephone, Email, BirthDate) 
                    VALUES(@FirstName, @LastName,  @Address, @ZipCode, @City, @Telephone, @Email, @BirthDate);SELECT CAST(scope_identity() AS int)";

                
                cmd.Parameters.Add("@FirstName", SqlDbType.VarChar);
                cmd.Parameters["@FirstName"].Value = contact.FirstName;

                cmd.Parameters.Add("@LastName", SqlDbType.VarChar);
                cmd.Parameters["@LastName"].Value = contact.LastName;

                cmd.Parameters.Add("@Address", SqlDbType.VarChar);
                cmd.Parameters["@Address"].Value = contact.Address;

                cmd.Parameters.Add("@ZipCode", SqlDbType.Int);
                cmd.Parameters["@ZipCode"].Value = contact.ZipCode;

                cmd.Parameters.Add("@City", SqlDbType.VarChar);
                cmd.Parameters["@City"].Value = contact.City;

                cmd.Parameters.Add("@Telephone", SqlDbType.VarChar);
                cmd.Parameters["@Telephone"].Value = contact.Telephone;

                cmd.Parameters.Add("@Email", SqlDbType.VarChar);
                cmd.Parameters["@Email"].Value = contact.Email;

                cmd.Parameters.Add("@BirthDate", SqlDbType.DateTime);
                cmd.Parameters["@BirthDate"].Value = contact.BirthDate;
                               
                _conn.Open();

                try
                {
                    return  long.Parse(cmd.ExecuteScalar().ToString());
                }
                catch (Exception)
                {
                    throw new Exception($"Entity {contact.FirstName} {contact.LastName} not inserted in database!");
                }
                
            }
            finally
            {
                _conn?.Close();
            }
        }

        public void Update(int id, Contact contact)
        {
            var contactToUpdate = GetContactById(id);
            if (contactToUpdate == null)
            {
                throw new Exception("Contact does not exist in database");
            }

            try
            {
                _conn = new SqlConnection(ConnString);

                var cmd = _conn.CreateCommand();
                cmd.CommandText = @"UPDATE Contacts SET [FirstName]=@paramFirstName, 
                                                        [LastName]=@paramLastName, 
                                                        [Address]=@paramAddress, 
                                                        [ZipCode]=@paramZipCode, 
                                                        [City]=@paramCity, 
                                                        [Telephone]=@paramTelephone, 
                                                        [Email]=@paramEmail,
                                                        [BirthDate]=@paramBirthDate
                                                        WHERE Id=@Id";

                cmd.Parameters.Add("@Id", SqlDbType.Int);
                cmd.Parameters["@Id"].Value = id;

                cmd.Parameters.Add("@paramFirstName", SqlDbType.VarChar);
                cmd.Parameters["@paramFirstName"].Value = contact.FirstName;

                cmd.Parameters.Add("@paramLastName", SqlDbType.VarChar);
                cmd.Parameters["@paramLastName"].Value = contact.LastName;

                cmd.Parameters.Add("@paramAddress", SqlDbType.VarChar);
                cmd.Parameters["@paramAddress"].Value = contact.Address;

                cmd.Parameters.Add("@paramZipCode", SqlDbType.Int);
                cmd.Parameters["@paramZipCode"].Value = contact.ZipCode;

                cmd.Parameters.Add("@paramCity", SqlDbType.VarChar);
                cmd.Parameters["@paramCity"].Value = contact.City;

                cmd.Parameters.Add("@paramTelephone", SqlDbType.VarChar);
                cmd.Parameters["@paramTelephone"].Value = contact.Telephone;

                cmd.Parameters.Add("@paramEmail", SqlDbType.VarChar);
                cmd.Parameters["@paramEmail"].Value = contact.Email;

                cmd.Parameters.Add("@paramBirthDate", SqlDbType.DateTime);
                cmd.Parameters["@paramBirthDate"].Value = contact.BirthDate;

                _conn.Open();

                var number = cmd.ExecuteNonQuery();

                if (number != 1)
                {
                    throw new Exception($"No Contacts were updated with Id: {id}");
                }
            }
            finally
            {
                _conn?.Close();
            }
        }

        public void Delete(int id)
        {
            _conn = new SqlConnection(ConnString);

            var sqlComm = _conn.CreateCommand();
            sqlComm.CommandText = @"DELETE FROM Contacts WHERE [ID] = @Id;";
            sqlComm.Parameters.Add("@Id", SqlDbType.Int);
            sqlComm.Parameters["@Id"].Value = id;

            _conn.Open();

            var rowsAffected = sqlComm.ExecuteNonQuery();

            _conn.Close();

            if (rowsAffected < 1)
            {
                throw new Exception("Entity has not been deleted!");
            }
        }
    }
}
