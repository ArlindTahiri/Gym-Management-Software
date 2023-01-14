using GUI.EmployeeGUIs;
using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.EmployeeGUIs
{
    /// <summary>
    /// Interaktionslogik für EmployeeChangeOptions.xaml
    /// </summary>
    public partial class EmployeeChangeOptions : Page
    {

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public EmployeeChangeOptions()
        {
            InitializeComponent();

            ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
            var fileInfo = new FileInfo(@"log4net.config");
            XmlConfigurator.Configure(repository, fileInfo);

            log.Info("Opened EmployeeChangeOptions Page");
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            log.Info("Clicked on the AddEmploye button");
            AddEmployee addEmployee = new AddEmployee();
            NavigationService.Navigate(addEmployee);
        }

        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            IDCheck iDCheck = new IDCheck("DeleteEmployee");
            NavigationService.Navigate(iDCheck);
        }

        private void EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            IDCheck iDCheck = new IDCheck("EditEmployee");
            NavigationService.Navigate(iDCheck);
        }
    }
}
