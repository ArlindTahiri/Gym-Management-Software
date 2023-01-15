using GUI.ArticleGUIs;
using GUI.ContractGUIs;
using GUI.EmployeeGUIs;
using GUI.LoginGUIs;
using GUI.MemberGUIs;
using GUI.Order_GUIs;
using loremipsum.Gym;
using loremipsum.Gym.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaktionslogik für GymPasswordPage.xaml
    /// </summary>
    public partial class GymPasswordPage : Page
    {
        public string Destination;
        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        private readonly IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];
        public GymPasswordPage(string destination)
        {
            Destination = destination;
            InitializeComponent();
        }
        public GymPasswordPage()
        {
            InitializeComponent();
            if (admin.ListLogIns().Count == 0)
            {
                Destination = "AddLogIn";
            }
            else
            {
                Destination = "Homepage";
            }
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            string username = Username.Text;
            string password = PasswordBox.Password.ToString();
            LogIn login = query.GetLogInDetails(Username.Text);
            if(login != null)
            {
                if (login.LogInPassword == PasswordBox.Password.ToString())
                {
                    // paste all the code here.
                }
                else { WarningText.Text = "Das eingegebene Log In Passwort ist falsch.\n Bitte geben Sie das richtige Passwort ein."; }
            }
            else { WarningText.Text = "Der eingegebene Log In Name exisitert nicht.\n Bitte geben Sie einen vorhandenen Log In Namen ein."; }
            

            if (Destination == "Member")
            {
                MemberPage memberPage = new MemberPage();
                NavigationService.Navigate(memberPage);
            }

            if(Destination == "Contract")
            {
                ContractPage contractPage = new ContractPage();
                NavigationService.Navigate(contractPage);
            }

            if(Destination == "Employee")
            {
                EmployeePage employeePage = new EmployeePage();
                NavigationService.Navigate(employeePage);
            }

            if(Destination == "Inventar")
            {
                ArticlePage articlePage = new ArticlePage();
                NavigationService.Navigate(articlePage);
            }

            if(Destination == "Login")
            {
                if (admin.ListLogIns().Count == 0) //if there are no logins you have to go directly to addlogin page and create rank 1 account
                {
                    AddEditLogin addLogin = new AddEditLogin("FirstTime");
                    NavigationService.Navigate(addLogin);
                }
                else
                {
                    LoginPage loginPage = new LoginPage();
                    NavigationService.Navigate(loginPage);
                }
            }
            if(Destination == "AddLogIn")
            {
                AddEditLogin addEditLogin = new AddEditLogin("FirstTime");
                NavigationService.Navigate(addEditLogin);
            }

            if(Destination == "Order")
            {
                OrderPage orderPage = new OrderPage();
                NavigationService.Navigate(orderPage);
            }

            if(Destination == "Homepage")
            {
                GymHomepage homepage= new GymHomepage();
                NavigationService.Navigate(homepage);
            }
        }
    }
}
