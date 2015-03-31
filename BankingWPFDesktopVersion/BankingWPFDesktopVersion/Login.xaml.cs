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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
            initComponents();
            this.ShowsNavigationUI = false;
            customizeControls();
        }
        private void customizeControls()
        {

            lblPassword.Content = "पासवर्ड";
            lblUser.Content = "वापरकर्ता";
            btnSubmit.Content = "प्रवेश";
        
        }

        private void initComponents()
        {
            cmbUserType.Items.Add("अड्मीन");
            cmbUserType.Items.Add("वापरकर्ता");
            cmbUserType.SelectedIndex = 0;
            //mainFrame.navigat
        }


        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            //Main mainScreen = new Main();
            Dashboard dashboard;
            if (cmbUserType.SelectedIndex == 0 && txtPassword.Password == "passwd")
            {
                //mainScreen.loggedIn = cmbUserType.Text;
                //NavigationService nv = NavigationService.GetNavigationService(this);
                //nv.Navigate(mainScreen);
                //MessageBox.Show(mainScreen.loggedIn);
                //mainScreen.lblLoggedIn.Content = "Logged in as :" + mainScreen.loggedIn;
                dashboard = new Dashboard(true);
                this.NavigationService.Navigate(dashboard);

            }
            else if (cmbUserType.SelectedIndex == 1 && txtPassword.Password == "userpass")
            {
                //mainScreen.loggedIn = cmbUserType.SelectedItem.ToString();
                //NavigationService nv = NavigationService.GetNavigationService(this);
                //nv.Navigate(mainScreen);
                dashboard = new Dashboard(false);
                this.NavigationService.Navigate(dashboard);
            }
            else
            {
                MessageBox.Show("Invalid credentials!");
            }
        }
    }
}
