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
using System.Data;

namespace BankingWPFDesktopVersion
{
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : Page
    {
        private DataTable reportSource;
        private string reportName;

        public Reports(DataTable dt, string name)
        {
            InitializeComponent();
            this.reportSource = dt;
            this.reportName = name;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            TransactionsReport report = new TransactionsReport();
            report.Load("F:\\Resoneuronance\\Banking\\BankingWPFDesktopVersion\\BankingWPFDesktopVersion\\" + reportName + ".rpt");
            report.SetDataSource(reportSource);
            crystalReportsViewer1.ViewerCore.ReportSource = report;
            //reportSource.Rows[0]["account_no"].ToString();
            //crystalReportsViewer1.Refresh();
            report.Refresh();
        }
    }
}
