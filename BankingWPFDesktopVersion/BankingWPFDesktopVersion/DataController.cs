using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;


namespace BankingWPFDesktopVersion
{
    class DataController
    {
        private SQLiteConnection connection;

        public DataController()
        {
            connection = new SQLiteConnection("Data Source=banking.sqlite;Version=3;");
        }

        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public DataTable getCustomerDataTable()
        {
            SQLiteCommand cmd = new SQLiteCommand("select * from customer", connection);
            openConnection();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            DataTable dt = new DataTable();
            //bankingDataSet dt = new bankingDataSet();
            adapter.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                 Console.WriteLine(row["customer_name"].ToString());
            }
            //Console.WriteLine(dt.Rows.Count.ToString());
            adapter.Dispose();
            closeConnection();

            return dt;
        }



        public DataTable getCustomer(string customerId)
        {
            SQLiteCommand cmd = new SQLiteCommand("select * from customer where customer_id like '" + customerId + "'", connection);
            openConnection();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            bankingDataSet dataset = new bankingDataSet();
            adapter.Fill(dataset);
            closeConnection();
            Console.WriteLine(dataset.Tables[3].Rows[0]["customer_name"]);
            //Console.WriteLine(reader["customer_id"].ToString());
            return dataset.Tables[3];
            //throw new NotImplementedException();
            //return new DataTable();
        }

        public DataTable getAccount(string accountNumber)
        {
            SQLiteCommand cmd = new SQLiteCommand("select * from account where account_no like '" + accountNumber + "'", connection);
            openConnection();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            //SQLiteDataReader reader = null;
            //reader = cmd.ExecuteReader();
            bankingDataSet dataset = new bankingDataSet();
            adapter.Fill(dataset);
            closeConnection();
            //Console.WriteLine(dataset.Tables[3].Rows[0]["closing_balance"]);
            //Console.WriteLine(reader["customer_id"].ToString());
            return dataset.Tables[3];
            /*if (dataset.Tables.Count > 0)
                return true;
            SQLiteDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
                return true;
            return false;*/
        }



        public DataTable getTransactions(string accountNo)
        {
            SQLiteCommand cmd = new SQLiteCommand("select * from transactions where account_no like '" + accountNo + "'", connection);
            openConnection();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            bankingDataSet dataset = new bankingDataSet();
            adapter.Fill(dataset);
            closeConnection();
            //Console.WriteLine(dataset.Tables[3].Rows[0]["closing_balance"]);
            //Console.WriteLine(reader["customer_id"].ToString());
            return dataset.Tables[3];
        }

        public Boolean withdrawAmount(string amount,string accountNo)
        {
            Boolean result = false;
            SQLiteCommand cmd = new SQLiteCommand("update account set closing_balance = closing_balance - " + amount + "  where account_no like'" + accountNo + "'", connection);
            openConnection();
            SQLiteTransaction transaction = connection.BeginTransaction();
            try
            {
                cmd.ExecuteNonQuery();
                cmd = new SQLiteCommand("insert into transactions(transaction_type,transaction_date,value_date," +
                       "transaction_amount,transaction_description,cheque_ref_no,account_no) values(?,?,?,?,?,?,?)", connection);
                //cmd.Parameters.Add("@date", System.DateTime.Now.Date.ToShortDateString());
                //cmd.Parameters.Add("@date", System.DateTime.Now.Date);
                cmd.Parameters.AddWithValue("transaction_type", "Withdraw");
                cmd.Parameters.AddWithValue("transaction_date", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("value_date", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("transaction_amount", amount);
                cmd.Parameters.AddWithValue("transaction_description", "");
                cmd.Parameters.AddWithValue("cheque_ref_no", "");
                cmd.Parameters.AddWithValue("account_no", int.Parse(accountNo));

                cmd.ExecuteNonQuery();
                transaction.Commit();
                result = true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            closeConnection();
            return result;
        }


        public Boolean depositAmount(string amount, string accountNo)
        {
            Boolean result = false;
            SQLiteCommand cmd = new SQLiteCommand("update account set closing_balance = closing_balance + " + amount + "  where account_no like'" + accountNo + "'", connection);
            openConnection();
            SQLiteTransaction transaction = connection.BeginTransaction();
            try
            {
                cmd.ExecuteNonQuery();
                cmd = new SQLiteCommand("insert into transactions(transaction_type,transaction_date,value_date," +
                       "transaction_amount,transaction_description,cheque_ref_no,account_no) values(?,?,?,?,?,?,?)", connection);
                cmd.Parameters.AddWithValue("transaction_type", "Deposit");
                cmd.Parameters.AddWithValue("transaction_date", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("value_date", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("transaction_amount", amount);
                cmd.Parameters.AddWithValue("transaction_description", "");
                cmd.Parameters.AddWithValue("cheque_ref_no", "");
                cmd.Parameters.AddWithValue("account_no", int.Parse(accountNo));

                cmd.ExecuteNonQuery();
                transaction.Commit();
                result = true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            closeConnection();
            return result;
        }

        public DataTable getAllTransactionsForAccount(string accountNo)
        {
            string query = "SELECT * FROM account INNER JOIN customer ON " +
                            "account.customer_id = customer.customer_id INNER JOIN transactions " +
                            "ON account.account_no = transactions.account_no " + 
                            "where account.account_no like '" + accountNo + "'";

            openConnection();
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            bankingDataSet dataset = new bankingDataSet();
            adapter.Fill(dataset);
            closeConnection();
            //Console.WriteLine(dataset.Tables[3].Rows[0]["closing_balance"]);
            //Console.WriteLine(reader["customer_id"].ToString());
            closeConnection();
            return dataset.Tables[3];

        }
    }
}
