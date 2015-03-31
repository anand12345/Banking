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
    /// Interaction logic for Withdraw.xaml
    /// </summary>
    public partial class Withdraw : Page
    {
        private DataTable account;
        private DataController dc;
        public static string MARATHI_FONT = "Shivaji02";
        public Withdraw(DataTable currentAccount)
        {
            InitializeComponent();
            this.account = currentAccount;
            lblWithdrawal.Content = "रक्कम काढणे";
            txtAmount.FontFamily = new FontFamily(MARATHI_FONT);
            txtAmount.FontSize = 18;
            btnSubmit.Content = "सादर";
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (double.Parse(txtAmount.Text) > double.Parse(account.Rows[0]["closing_balance"].ToString()))
            {
                MessageBox.Show("Not enough cash in the account!");
                return;
            }
            dc = new DataController();
            Boolean transactionSuccessful = dc.withdrawAmount(txtAmount.Text, account.Rows[0]["account_no"].ToString());
            if (!transactionSuccessful)
                MessageBox.Show("Error in transaction!");
            else
                MessageBox.Show("Withdrawn Rs. " + txtAmount.Text + " successfully!");

        }
    }
}
