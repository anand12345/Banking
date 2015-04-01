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
using System.Data;

namespace BankingWPFDesktopVersion
{
    /// <summary>
    /// Interaction logic for Accounts.xaml
    /// </summary>
    public partial class Accounts : Page
    {
        private Transactions transactions;
        private DataTable account;
        private Withdraw withdraw;
        private Deposit deposit;
        //public static string MARATHI_FONT = "Shivaji02";
        public Accounts()
        {
            InitializeComponent();
            btnWithdraw.Visibility = Visibility.Hidden;
            btnDeposit.Visibility = Visibility.Hidden;
            btnTransactions.Visibility = Visibility.Hidden;
            customizeControls();

        }
        private void customizeControls()
        {
            label1.Content = "खाते क्र.";
            btnSubmit.Content = "सादर";
            txtAccountNumber.FontFamily = new FontFamily(Main.MARATHI_FONT);
            txtAccountNumber.FontSize = 18;
            btnWithdraw.Content = "काढणे";
            btnDeposit.Content = "ठेव";
            btnTransactions.Content="व्यवहार";
        
        }
            
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            DataController controller = new DataController();
            if ( (account = controller.getAccount(txtAccountNumber.Text)).Rows.Count > 0)
            {
                btnDeposit.Visibility = Visibility.Visible;
                btnWithdraw.Visibility = Visibility.Visible;
                btnTransactions.Visibility = Visibility.Visible;
                //MessageBox.Show(dataset.transactions.Rows.Count.ToString());
                transactions = new Transactions(account);
                mainFrame.Navigate(transactions);

            }
            else
            {
                MessageBox.Show("खाते अस्तित्वात नाही!");
            }

        }

        private void btnWithdraw_Click(object sender, RoutedEventArgs e)
        {
            if (withdraw == null)
                withdraw = new Withdraw(account);
            mainFrame.Navigate(withdraw);
        }

        private void btnDeposit_Click(object sender, RoutedEventArgs e)
        {
            if (deposit == null)
                deposit = new Deposit(account);
            mainFrame.Navigate(deposit);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            transactions = new Transactions(account);
            mainFrame.Navigate(transactions);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            txtAccountNumber.Focus();
        }
    }
}
