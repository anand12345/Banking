﻿using System;
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
    public partial class Transactions : Page
    {
        private DataTable account;
        private DataTable customer;
        private DataTable transactions;
        private DataController dc;

        public Transactions(DataTable account)
        {
            InitializeComponent();
            //this.accountNumber = dataset.account.account_noColumn.ToString();
            this.account = account;
            dc = new DataController();
            this.customer = dc.getCustomer(account.Rows[0]["customer_id"].ToString());
            this.transactions = dc.getTransactions(account.Rows[0]["account_no"].ToString());
            //gridAccounts.ItemsSource = new DataController().getCustomer(accountNumber).DefaultView;
            gridTransactions.ItemsSource = transactions.DefaultView;
            gridTransactions.AutoGenerateColumns = true;
            lblCustomerNameLoaded.Content = customer.Rows[0]["customer_name"];
            lblBalance.Content = account.Rows[0]["closing_balance"];
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
