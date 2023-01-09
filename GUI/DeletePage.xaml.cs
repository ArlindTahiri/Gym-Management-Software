using log4net;
using log4net.Config;
using log4net.Repository;
using loremipsum.Gym;
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

namespace GUI
{
    /// <summary>
    /// Interaktionslogik für DeletePage.xaml
    /// </summary>
    public partial class DeletePage : Page
    {
        IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string destination;
        private string destinationtrans;
        public DeletePage(string destination)
        {
            InitializeComponent();
            this.destination = destination;

            switch (destination)
            {
                case "Member": QuestionLabel.Content = "Wollen Sie wirklich alle Mitglieder löschen?";
                    break;

                case "Employee": QuestionLabel.Content = "Wollen Sie wirklich alle Mitarbeiter löschen?";
                    break;

                case "Article": QuestionLabel.Content = "Wollen Sie wirklich alle Artikel löschen?";
                    break;

                case "Contract": QuestionLabel.Content = "Wollen Sie wirklich alle Verträge löschen?";
                    break;

                case "Login": QuestionLabel.Content = "Wollen Sie wirklich alle Logins löschen?";
                    break;

                case "Order": QuestionLabel.Content = "Wollen Sie wirklich alle Bestellungen löschen?";
                    break;

                case "Home": QuestionLabel.Content = "Wollen Sie wirklich ALLES löschen?";
                    break;
            }
            

            ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
            var fileInfo = new FileInfo(@"log4net.config");
            XmlConfigurator.Configure(repository, fileInfo);

            log.Info("Opened DeletePage from destination: "+destination);
        }

      

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            if(destination.Equals("Member"))
            {
                log.Info("Deleted all members and returned to GymHomepage");
                admin.DeleteMembers();
                GymHomepage gymHomepage = new GymHomepage();
                NavigationService.Navigate(gymHomepage);
            }

            if (destination.Equals("Employee"))
            {
                log.Info("Deleted all employees and returned to GymHomepage");
                admin.DeleteEmployees();
                GymHomepage gymHomepage = new GymHomepage();
                NavigationService.Navigate(gymHomepage);
            }

            if (destination.Equals("Article"))
            {
                log.Info("Deleted all articles and returned to GymHomepage");
                admin.DeleteArticles();
                GymHomepage gymHomepage = new GymHomepage();
                NavigationService.Navigate(gymHomepage);
            }

            if (destination.Equals("Contract"))
            {
                log.Info("Deleted all contracts and returned to GymHomepage");
                admin.DeleteContracts();
                GymHomepage gymHomepage = new GymHomepage();
                NavigationService.Navigate(gymHomepage);
            }

            if (destination.Equals("Login"))
            {
                log.Info("Deleted all logins and returned to GymHomepage");
                admin.DeleteLogIns();
                GymHomepage gymHomepage = new GymHomepage();
                NavigationService.Navigate(gymHomepage);
            }

            if (destination.Equals("Order"))
            {
                log.Info("Deleted all orders and returned to GymHomepage");
                admin.DeleteOrders();
                GymHomepage gymHomepage = new GymHomepage();
                NavigationService.Navigate(gymHomepage);
            }

            if (destination.Equals("Home"))
            {
                log.Info("Deleted everything and returned to GymHomepage");
                admin.DeleteArticles();
                admin.DeleteContracts();
                admin.DeleteLogIns();
                admin.DeleteMembers();
                admin.DeleteEmployees();
                admin.DeleteOrders();
                GymHomepage gymHomepage = new GymHomepage();
                NavigationService.Navigate(gymHomepage);
            }
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            log.Info("Aborted the option and retunred back to GymHomepage");
            GymHomepage gymHomepage = new GymHomepage();
            NavigationService.Navigate(gymHomepage);
        }
    }
}
