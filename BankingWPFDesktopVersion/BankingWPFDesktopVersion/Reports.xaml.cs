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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SQLite;

namespace BankingWPFDesktopVersion
{
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : Page
    {
        public Reports()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ReportDocument report = new ReportDocument();
            report.Load("F:\\Resoneuronance\\Banking\\BankingWPFDesktopVersion\\BankingWPFDesktopVersion\\CustomerCrystalReport.rpt");
            report.SetDataSource(new DataController().getCustomerDataTable());
            crystalReportsViewer1.ViewerCore.ReportSource = report;
            
        }
    }
}
