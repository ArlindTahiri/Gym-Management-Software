using GUI.LoginGUIs;
using log4net;
using log4net.Config;
using log4net.Repository;
using loremipsum.Gym;
using loremipsum.Gym.Entities;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
        IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string destination;
        private int ID;
        private string _name;
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

                case "CheckoutMembers":
                    QuestionLabel.Content = "Wollen Sie wirklich bei allen Mitgliedern einen Kassensturz durchführen?";
                    break;
            }

         
            

            ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
            var fileInfo = new FileInfo(@"log4net.config");
            XmlConfigurator.Configure(repository, fileInfo);

            log.Info("Opened DeletePage from destination: "+destination);
        }

        public DeletePage(string destination, string name)
        {
            InitializeComponent();
            this.destination = destination;
            this._name = name;

            if (destination.Equals("DeleteLogin"))
            {
                QuestionLabel.Content = "Wollen Sie wirklich diesen Login löschen?";
                QuestionBox.Text = query.GetLogInDetails(_name).ToString();

            }
            
        }

        public DeletePage(string destination, int ID)
        {
            InitializeComponent();
            this.destination = destination;
            this.ID = ID;

            switch (destination)
            {
                case "DeleteArticle":
                    QuestionLabel.Content = "Wollen Sie wirklich diesen Artikel löschen?";
                    QuestionBox.Text = query.GetArticleDetails(ID).ToString();
                    break;

                case "DeleteContract":
                    QuestionLabel.Content = "Wollen Sie wirklich diesen Vertrag löschen?";
                    QuestionBox.Text = query.GetContractDetails(ID).ToString();
                    break;

                case "DeleteEmployee":
                    QuestionLabel.Content = "Wollen Sie wirklich diesen Mitarbeiter löschen?";
                    QuestionBox.Text = query.GetEmployeeDetails(ID).ToString();
                    break;

                case "DeleteMember":
                    QuestionLabel.Content = "Wollen Sie wirklich dieses Mitglied löschen?";
                    QuestionBox.Text = query.GetMemberDetails(ID).ToString();
                    break;

                case "DeleteOrder":
                    QuestionLabel.Content = "Wollen Sie wirklich diese Bestellung löschen?";
                    QuestionBox.Text = query.GetOrderDetails(ID).ToString();
                    break;
            }
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

            if (destination.Equals("DeleteArticle"))
            {
                admin.DeleteArticle(ID);
                GymHomepage gymHomepage = new GymHomepage();
                NavigationService.Navigate(gymHomepage);
            }

            if (destination.Equals("DeleteContract"))
            {
                admin.DeleteContract(ID);

                GymHomepage gymHomepage = new GymHomepage();
                NavigationService.Navigate(gymHomepage);
            }

            if (destination.Equals("DeleteEmployee"))
            {
                admin.DeleteEmployee(ID);

                GymHomepage home = new GymHomepage();
                NavigationService.Navigate(home);
            }

            if (destination.Equals("DeleteLogin"))
            {
                admin.DeleteLogIn(_name);

                GymHomepage home = new GymHomepage();
                NavigationService.Navigate(home);
            }

            if (destination.Equals("DeleteMember"))
            {
                admin.DeleteMember(ID);

                GymHomepage home = new GymHomepage();
                NavigationService.Navigate(home);
            }

            if (destination.Equals("DeleteOrder"))
            {
                admin.DeleteOrder(ID);

                GymHomepage gymHomepage = new GymHomepage();
                NavigationService.Navigate(gymHomepage);
            }

            if (destination.Equals("CheckoutMembers"))
            {
                admin.CheckOutMembers();

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
