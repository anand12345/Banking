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
        public Dashboard(Boolean isAdminLogin)
        {
            InitializeComponent();
            this.isAdmin = isAdminLogin;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (isAdmin)
            {
                mainFrame.Navigate(new Main());
            }
            else
            {
                btnAdd.Visibility = Visibility.Hidden;
            }
        }

        private void btnAccounts_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new Accounts());
        }
    }
}
