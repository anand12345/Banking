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
using Microsoft.Reporting.WinForms;
using System.Data;

namespace BankingWPFDesktopVersion
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class CustomerReport : UserControl
    {
        public CustomerReport()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
          
            DataController controller = new DataController();
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "CustomerReport"; // Name of the DataSet we set in .rdlc
            reportDataSource.Value = controller.getCustomerDataTable();
            reportViewer.LocalReport.ReportPath = "F:\\Resoneuronance\\Banking\\BankingWPFDesktopVersion\\BankingWPFDesktopVersion\\Report1.rdlc"; // Path of the rdlc file
            reportViewer.LocalReport.DataSources.Add(reportDataSource);
            reportViewer.RefreshReport();
        }

        private void reportViewer_RenderingComplete(object sender, Microsoft.Reporting.WinForms.RenderingCompleteEventArgs e)
        {



        }
    }
}
