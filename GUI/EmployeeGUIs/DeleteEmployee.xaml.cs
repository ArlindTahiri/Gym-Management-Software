using GUI.EmployeeGUIs;
using log4net;
using log4net.Config;
using log4net.Repository;
using loremipsum.Gym;
using loremipsum.Gym.Entities;
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
    /// Interaktionslogik für DeleteEmployee.xaml
    /// </summary>
    public partial class DeleteEmployee : Page
    {
        IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        private readonly IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private int employeeID;

        public DeleteEmployee(int employeeID)
        {
            InitializeComponent();
            this.employeeID = employeeID;


            ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
            var fileInfo = new FileInfo(@"log4net.config");
            XmlConfigurator.Configure(repository, fileInfo);

            log.Info("Opened DeleteEmployee Page");
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            log.Info("Deleted the employee: " + query.GetEmployeeDetails(employeeID).ToString() + "... and returned to GymHomepage");
            admin.DeleteEmployee(employeeID);

            GymHomepage home = new GymHomepage();
            NavigationService.Navigate(home);
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            log.Info("Aborted the deleteArticle option and returned back to GymHomepage");
            GymHomepage home = new GymHomepage();
            NavigationService.Navigate(home);
        }
    }
}
