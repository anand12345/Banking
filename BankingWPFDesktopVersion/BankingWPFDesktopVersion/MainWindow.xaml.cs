using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;


namespace BankingWPFDesktopVersion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string queryCreate= "create table transactions(transaction_id INTEGER PRIMARY KEY,transaction_type TEXT,transaction_date TEXT,value_date TEXT, REAL,description TEXT,cheque_ref_no TEXT,customer_id INTEGER,account_no INTEGER,FOREIGN KEY(account_no) REFERENCES account(account_no),FOREIGN KEY(customer_id) REFERENCES customer(customer_id));";

        private string queryInsert = "insert into account(account_no,customer_id) values(1,1)";

        private string querySelect = "select * from customer";
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            //this.WindowStyle = WindowStyle.None;
            //btnAdd.Visibility = Visibility.Hidden;
            mainFrame.Navigate(new Login());
            //initDB();
        }

        private void initDB()
        {
            SQLiteConnection m_dbConnection;
            //SQLiteConnection.CreateFile("banking.sqlite");
            m_dbConnection = new SQLiteConnection("Data Source=banking.sqlite;Version=3;");
            if (m_dbConnection.State == System.Data.ConnectionState.Closed)
            {
                m_dbConnection.Open();
            }
            //Console.Write("DB created!");
            SQLiteCommand command = new SQLiteCommand(queryCreate, m_dbConnection);
            command.ExecuteNonQuery();
            //SQLiteDataReader reader = command.ExecuteReader();
            //reader.Read();
            //MessageBox.Show("Done! :" + reader["customer_name"]);
            m_dbConnection.Close();
            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new Main());
        }


    }
}
