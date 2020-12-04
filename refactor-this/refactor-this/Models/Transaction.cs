using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace refactor_this.Models
{
    public class Transaction
    {
        //public Guid Id { get; set; }
        public float Amount { get; set; }

        public DateTime Date { get; set; }
        //public Guid? AccountId { get; set; }

        public Transaction(float amount, DateTime date)
        {
            Amount = amount;
            Date = date;
        }

        public Transaction()
        {
            
        }


        public List<Transaction> GetTransactions(Guid id)
        {
            using (var connection = Helpers.NewConnection())
            {
                SqlCommand command = new SqlCommand($"select Amount, Date from Transactions where AccountId = '{id}'", connection);
                connection.Open();
                var reader = command.ExecuteReader();
                var transactions = new List<Transaction>();
                while (reader.Read())
                {
                    var amount = (float)reader.GetDouble(0);
                    var date = reader.GetDateTime(1);
                    transactions.Add(new Transaction(amount, date));
                }
                return transactions;
            }
        }

        public bool Save(Guid id)
        {
            SqlTransaction objTrans = null;
            bool res = true;
            using (var connection = Helpers.NewConnection())
            {
                
                SqlCommand command;

                command = new SqlCommand($"INSERT INTO Transactions (Id, Amount, Date, AccountId) VALUES ('{Guid.NewGuid()}', {Amount}, '{Date}', '{id}')", connection);
                connection.Open();
                if (command.ExecuteNonQuery() != 1)
                {
                    objTrans.Rollback();
                    res = false;
                }
                    

                command = new SqlCommand($"update Accounts set Amount = Amount + {Amount} where Id = '{id}'", connection);
                connection.Open();
                if (command.ExecuteNonQuery() != 1)
                {
                    objTrans.Rollback();
                    res = false;
                }

                return res;

            }
        }
    }
}