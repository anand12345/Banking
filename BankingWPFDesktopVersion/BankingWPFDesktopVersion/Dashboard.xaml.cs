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

namespace BankingWPFDesktopVersion
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        private Boolean isAdmin = false;
        private Main addNew;
        private Accounts accounts;
        public Dashboard(Boolean isAdminLogin)
        {
            InitializeComponent();
            this.isAdmin = isAdminLogin;
            addNew = new Main();
            accounts = new Accounts();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (isAdmin)
            {
                mainFrame.Navigate(addNew);
            }
            else
            {
                btnAdd.Visibility = Visibility.Hidden;
                mainFrame.Navigate(accounts);
            }
        }

        private void btnAccounts_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(accounts);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(addNew);
        }
    }
}
