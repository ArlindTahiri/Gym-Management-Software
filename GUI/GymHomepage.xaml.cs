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
using GUI.ArticleGUIs;
using GUI.EmployeeGUIs;
using GUI.Order_GUIs;
using log4net;
using log4net.Config;
using log4net.Repository;

namespace GUI
{
    /// <summary>
    /// Interaktionslogik für GymHomepage.xaml
    /// </summary>
    public partial class GymHomepage : Page
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(GymHomepage));
        public GymHomepage()
        {
            InitializeComponent();

            ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
            var fileInfo = new FileInfo(@"log4net.config");
            XmlConfigurator.Configure(repository, fileInfo);

            log.Info("Opened gym homepage");
        }

        private void Member_Click(object sender, RoutedEventArgs e)
        {

            log.Info("Clicked on member button");

            GymPasswordPage gymPasswordPage = new GymPasswordPage("Member");
            NavigationService.Navigate(gymPasswordPage);
        }

        private void Contract_Click(object sender, RoutedEventArgs e)
        {
          
            log.Info("Clicked on contract button");

            GymPasswordPage gymPasswordPage = new GymPasswordPage("Contract");
            NavigationService.Navigate(gymPasswordPage);
        }

        private void EmployeeButton_Click(object sender, RoutedEventArgs e)
        {
           

            log.Info("Clicked on employee button");

            GymPasswordPage gymPasswordPage = new GymPasswordPage("Employee");
            NavigationService.Navigate(gymPasswordPage);
        }

        private void Inventar_Click(object sender, RoutedEventArgs e)
        {
            log.Info("Clicked on inventory button");

            GymPasswordPage gymPasswordPage = new GymPasswordPage("Inventar");
            NavigationService.Navigate(gymPasswordPage);
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            log.Info("Clicked on orderButton button");

            OrderOptions orderOptions = new OrderOptions();
            NavigationService.Navigate(orderOptions);
        }

        private void Logins_Click(object sender, RoutedEventArgs e)
        {
            log.Info("Clicked on logins button");

            GymPasswordPage gymPasswordPage = new GymPasswordPage("Login");
            NavigationService.Navigate(gymPasswordPage);

        }
    }
}
