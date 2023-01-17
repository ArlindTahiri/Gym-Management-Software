using GUI.ArticleGUIs;
using GUI.ContractGUIs;
using GUI.EmployeeGUIs;
using GUI.LoginGUIs;
using GUI.MemberGUIs;
using GUI.Order_GUIs;
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

                case "CheckoutMembers": QuestionLabel.Content = "Wollen Sie wirklich bei allen Mitgliedern einen Kassensturz durchführen?";
                    break;                
            }                  
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

            ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
            var fileInfo = new FileInfo(@"log4net.config");
            XmlConfigurator.Configure(repository, fileInfo);
        }
      

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            if(destination.Equals("Member"))
            {
                admin.DeleteMembers();
                MemberPage memberPage = new MemberPage();
                NavigationService.Navigate(memberPage);
            }

            if (destination.Equals("Employee"))
            {
                admin.DeleteEmployees();
                EmployeePage employeePage= new EmployeePage();
                NavigationService.Navigate(employeePage);
            }

            if (destination.Equals("Article"))
            {
                admin.DeleteArticles();
                ArticlePage articlePage =new ArticlePage();
                NavigationService.Navigate(articlePage);
            }

            if (destination.Equals("Contract"))
            {
                admin.DeleteContracts();
                ContractPage contractPage =new ContractPage();
                NavigationService.Navigate(contractPage);
            }

            if (destination.Equals("Login"))
            {
                admin.DeleteLogIns();
                LoginPage loginPage = new LoginPage();
                NavigationService.Navigate(loginPage);
            }

            if (destination.Equals("Order"))
            {             
                admin.DeleteOrders();
                OrderPage orderPage = new OrderPage();
                NavigationService.Navigate(orderPage);
            }

            if (destination.Equals("Home"))
            {
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
                ArticlePage articlePage =new ArticlePage();
                NavigationService.Navigate(articlePage);
            }

            if (destination.Equals("DeleteContract"))
            {
                admin.DeleteContract(ID);
                ContractPage contractPage = new ContractPage();
                NavigationService.Navigate(contractPage);
            }

            if (destination.Equals("DeleteEmployee"))
            {
                admin.DeleteEmployee(ID);
                EmployeePage employeePage = new EmployeePage();
                NavigationService.Navigate(employeePage);
            }

            if (destination.Equals("DeleteLogin"))
            {
                admin.DeleteLogIn(_name);
                LoginPage loginPage = new LoginPage();
                NavigationService.Navigate(loginPage);
            }

            if (destination.Equals("DeleteMember"))
            {
                admin.DeleteMember(ID);
                MemberPage memberPage = new MemberPage();
                NavigationService.Navigate(memberPage);
            }

            if (destination.Equals("DeleteOrder"))
            {
                Order orderToDelete = query.GetOrderDetails(ID);
                log.Info("Returned the order: " + orderToDelete.ToString());
                admin.DeleteOrder(ID);
                OrderPage orderPage = new OrderPage();
                NavigationService.Navigate(orderPage);
            }

            if (destination.Equals("CheckoutMembers"))
            {
                admin.CheckOutMembers();
                EmployeePage employeePage= new EmployeePage();
            }
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            if (destination.Equals("Member"))
            {              
                MemberPage memberPage = new MemberPage();
                NavigationService.Navigate(memberPage);
            }

            if (destination.Equals("Employee"))
            {             
                EmployeePage employeePage = new EmployeePage();
                NavigationService.Navigate(employeePage);
            }

            if (destination.Equals("Article"))
            {            
                ArticlePage articlePage = new ArticlePage();
                NavigationService.Navigate(articlePage);
            }

            if (destination.Equals("Contract"))
            {              
                ContractPage contractPage = new ContractPage();
                NavigationService.Navigate(contractPage);
            }

            if (destination.Equals("Login"))
            {              
                LoginPage loginPage = new LoginPage();
                NavigationService.Navigate(loginPage);
            }

            if (destination.Equals("Order"))
            {              
                OrderPage orderPage = new OrderPage();
                NavigationService.Navigate(orderPage);
            }

            if (destination.Equals("Home"))
            {             
                GymHomepage gymHomepage = new GymHomepage();
                NavigationService.Navigate(gymHomepage);
            }

            if (destination.Equals("DeleteArticle"))
            {            
                ArticlePage articlePage = new ArticlePage();
                NavigationService.Navigate(articlePage);
            }

            if (destination.Equals("DeleteContract"))
            {             
                ContractPage contractPage = new ContractPage();
                NavigationService.Navigate(contractPage);
            }

            if (destination.Equals("DeleteEmployee"))
            {              
                EmployeePage employeePage = new EmployeePage();
                NavigationService.Navigate(employeePage);
            }

            if (destination.Equals("DeleteLogin"))
            {               
                LoginPage loginPage = new LoginPage();
                NavigationService.Navigate(loginPage);
            }

            if (destination.Equals("DeleteMember"))
            {             
                MemberPage memberPage = new MemberPage();
                NavigationService.Navigate(memberPage);
            }

            if (destination.Equals("DeleteOrder"))
            {            
                OrderPage orderPage = new OrderPage();
                NavigationService.Navigate(orderPage);
            }

            if (destination.Equals("CheckoutMembers"))
            {            
                EmployeePage employeePage = new EmployeePage();
            }
        }
    }
}
