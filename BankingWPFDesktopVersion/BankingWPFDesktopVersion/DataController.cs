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


    }
}
