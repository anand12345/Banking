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
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public string loggedIn;
        public static string MARATHI_FONT = "Shivaji02";
        private SQLiteConnection dbConnection;

        public Main()
        {
            InitializeComponent();
            initComponents();
            this.ShowsNavigationUI = false;
        }

        private void initComponents()
        {
            dbConnection = new SQLiteConnection("Data Source=banking.sqlite;Version=3;");

            //lblLoggedIn.Content = "Logged in as :" + loggedIn;
            cmbSex.Items.Add("Male");
            cmbSex.Items.Add("Female");
            cmbSex.SelectedIndex = 0;

            cmbSex2 = cmbSex;

            cmbMarital.Items.Add("Married");
            cmbMarital.Items.Add("Single");
            cmbMarital.SelectedIndex = 0;

            cmbMarital2 = cmbMarital;

            cmbAccountType.Items.Add("Current");
            cmbAccountType.Items.Add("Savings");
            cmbAccountType.Items.Add("Fixed Deposit");
            cmbAccountType.SelectedIndex = 1;

            dateOfOpening.Text = System.DateTime.Now.ToString();
            txtAccountNo.Text = getAccountNumber();

            txtName.FontFamily = new FontFamily(MARATHI_FONT);
            txtName.FontSize = 18;
            txtAge.FontFamily = new FontFamily(MARATHI_FONT);
            txtAge.FontSize = 18;
            txtMobile.FontFamily = new FontFamily(MARATHI_FONT);
            txtMobile.FontSize = 18;
            txtDeposit.FontFamily = new FontFamily(MARATHI_FONT);
            txtDeposit.FontSize = 18;
            txtAccountNo.FontFamily = new FontFamily(MARATHI_FONT);
            txtAccountNo.FontSize = 18;
            dob.FontFamily = new FontFamily(MARATHI_FONT);
            dob.FontSize = 18;
            //dob.LostFocus = dobChanged;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(dob.SelectedDate.ToString());

            string insertQuery = "insert into customer(customer_name,age,sex,mobile_no,address," +
            "date_of_birth,marital_status) values('" + txtName.Text + "'," + txtAge.Text + ",'" +
            cmbSex.Text + "'," + txtMobile.Text + ",'" + txtAddress.Text + "','" + dob.Text + "','" + cmbMarital.Text + "')";
            checkAndOpenConnection();
            SQLiteTransaction transaction = dbConnection.BeginTransaction();

            try
            {
                SQLiteCommand command = new SQLiteCommand(insertQuery, dbConnection);
                command.ExecuteNonQuery();

                string custId = getCustomerId();

                insertQuery = "insert into account(account_no,customer_id,isactive,account_type,opening_date," +
                    "closing_balance,outstanding_balance) values(" + txtAccountNo.Text + "," + custId + ",1,'" +
                    cmbAccountType.Text + "','" + dateOfOpening.Text + "'," + txtDeposit.Text + ",0)";

                command = new SQLiteCommand(insertQuery, dbConnection);
                command.ExecuteNonQuery();
                transaction.Commit();
                dbConnection.Close();
                MessageBox.Show("Record saved successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in saving!" + ex.Message);
                transaction.Rollback();
            }


            //Console.Write("DB created!");
           
        }

        private string getCustomerId()
        {
            checkAndOpenConnection();
            SQLiteCommand cmd = new SQLiteCommand("select max(customer_id) as lastCustomer from customer",dbConnection);
            SQLiteDataReader reader = cmd.ExecuteReader();
            reader.Read();
            return reader["lastCustomer"].ToString();
        }

        private void checkAndOpenConnection()
        {
            if (dbConnection.State == System.Data.ConnectionState.Closed)
            {
                dbConnection.Open();
            }

        }

        private string getAccountNumber()
        {
            checkAndOpenConnection();
            SQLiteCommand cmd = new SQLiteCommand("select max(account_no) as maxAccountNo from account",dbConnection);
            SQLiteDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int accNo = int.Parse(reader["maxAccountNo"].ToString());
            accNo++;
            return accNo.ToString();
        }

        private void btnShowRpt_Click(object sender, RoutedEventArgs e)
        {
            //CrystalReport1 rpt = new CrystalReport1();
            //rpt.
            //MessageBox.Show(txtName.Text);
            //Reports reports = new Reports(new DataController().getCustomerDataTable());
            //this.NavigationService.Navigate(reports);
        }


        private void dob_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            calculateAge();
        }

        private void dob_LostFocus(object sender, RoutedEventArgs e)
        {
            calculateAge();
        }

        private void calculateAge()
        {
            if (dob.Text.Length > 0)
            {
                try
                {
                    DateTime today = DateTime.Today;
                    int age = today.Year - dob.SelectedDate.Value.Year;
                    if (dob.SelectedDate.Value > today.AddYears(-age))
                        age--;
                    txtAge.Text = age.ToString();
                }
                catch (Exception ex)
                {

                }

            }
        }


    }
}
