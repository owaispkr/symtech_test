using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace refactor_this.Models
{
    public class Account
    {
        private bool isNew;

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        public float Amount { get; set; }

        public Account()
        {
            isNew = true;
        }

        public Account(Guid id)
        {
            isNew = false;
            Id = id;
        }

        public void Save()
        {
            using (var connection = Helpers.NewConnection())
            {
                SqlCommand command;
                if (isNew)
                    command = new SqlCommand($"insert into Accounts (Id, Name, Number, Amount) values ('{Guid.NewGuid()}', '{Name}', {Number}, 0)", connection);
                else
                    command = new SqlCommand($"update Accounts set Name = '{Name}' where Id = '{Id}'", connection);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(Guid id)
        {
            using (var connection = Helpers.NewConnection())
            {
                SqlCommand command = new SqlCommand($"delete from Accounts where Id = '{id}'", connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public Account Get(Guid id)
        {
            using (var connection = Helpers.NewConnection())
            {
                SqlCommand command = new SqlCommand($"select * from Accounts where Id = '{id}'", connection);
                connection.Open();
                var reader = command.ExecuteReader();
                if (!reader.Read())
                    throw new ArgumentException();

                var account = new Account(id);
                account.Name = reader["Name"].ToString();
                account.Number = reader["Number"].ToString();
                account.Amount = float.Parse(reader["Amount"].ToString());
                return account;
            }
        }

        public List<Account> GetAccounts()
        {
            using (var connection = Helpers.NewConnection())
            {
                SqlCommand command = new SqlCommand($"select * from Accounts", connection);
                connection.Open();
                var reader = command.ExecuteReader();
                var accounts = new List<Account>();
                while (reader.Read())
                {

                    var account = new Account();
                    account.Id = new Guid(reader["Id"].ToString());
                    account.Name = reader["Name"].ToString();
                    account.Number = reader["Number"].ToString();
                    account.Amount = float.Parse(reader["Amount"].ToString());
                    //var id = Guid.Parse(reader["Id"].ToString());
                    //var account = Get(id);
                    accounts.Add(account);
                }

                return accounts;
            }
        }
    }
}