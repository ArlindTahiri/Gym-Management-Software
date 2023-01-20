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
using System.Windows.Media.Media3D;
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
            Username.Focusable= true;
            Username.Focus();
        }

        public GymPasswordPage()//when starting the project this will happen
        {
            InitializeComponent();
            Username.Focusable = true;
            Username.Focus();
            GoBackIcon.Visibility = Visibility.Collapsed;
            if (admin.ListLogIns().Count == 0)
            {
                Destination = "AddLogIn";
                NoLoginTextBlock.Visibility = Visibility.Visible;
                LogInButtonTextblock.Text = "Login erstellen";
                UsernameLabel.Visibility = Visibility.Collapsed;
                Username.Visibility= Visibility.Collapsed;
                PasswordLabel.Visibility = Visibility.Collapsed;
                PasswordBox.Visibility = Visibility.Collapsed;
                WariningLabel.Visibility = Visibility.Collapsed;
            }
            else
            {
                Destination = "Homepage";
            }
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            LogIn login = query.GetLogInDetails(Username.Text);
            if(login != null)
            {
                if (login.LogInPassword == PasswordBox.Password)
                {
                    if (Destination == "Member" && login.Rank <= 2)
                    {
                        MemberPage memberPage = new MemberPage();
                        NavigationService.Navigate(memberPage);
                    }
                    else { WarningText.Content = "Das eingegebene Log In hat nicht genügend Rechte, um auf die Mitglieder zugreifen zu können."; }

                    if (Destination == "Contract" && login.Rank <= 2)
                    {
                        ContractPage contractPage = new ContractPage();
                        NavigationService.Navigate(contractPage);
                    }
                    else { WarningText.Content = "Das eingegebene Log In hat nicht genügend Rechte, um auf die Verträge zugreifen zu können."; }

                    if (Destination == "Employee" && login.Rank == 1)
                    {
                        EmployeePage employeePage = new EmployeePage();
                        NavigationService.Navigate(employeePage);
                    }
                    else { WarningText.Content = "Das eingegebene Log In hat nicht genügend Rechte, um auf die Mitarbeiter zugreifen zu können."; }

                    if (Destination == "Inventar" && login.Rank <= 2)
                    {
                        ArticlePage articlePage = new ArticlePage();
                        NavigationService.Navigate(articlePage);
                    }
                    else { WarningText.Content = "Das eingegebene Log In hat nicht genügend Rechte, um auf das Inventar zugreifen zu können."; }

                    if (Destination == "Login" && login.Rank == 1)
                    {                      
                        LoginPage loginPage = new LoginPage();
                        NavigationService.Navigate(loginPage);
                    } else { WarningText.Content = "Das eingegebene Log In hat nicht genügend Rechte, um auf die Logins zugreifen zu können."; }

                    if (Destination == "Order" && login.Rank <= 2)
                    {
                        OrderPage orderPage = new OrderPage();
                        NavigationService.Navigate(orderPage);
                    }
                    else { WarningText.Content = "Das eingegebene Log In hat nicht genügend Rechte, um auf die Bestellungen zugreifen zu können."; }

                    if (Destination == "Homepage")
                    {
                        GymHomepage homepage = new GymHomepage();
                        NavigationService.Navigate(homepage);
                    }

                    if(Destination == "DeleteAllMembers" && login.Rank == 1)
                    {
                        DeletePage deletePage = new DeletePage("Member");
                        NavigationService.Navigate(deletePage);
                    }
                    else { WarningText.Content = "Das eingegebene Log In hat nicht genügend Rechte, um auf alle Mitglieder zu löschen."; }

                    if (Destination == "DeleteAllOrders" && login.Rank == 1)
                    {
                        DeletePage deletePage = new DeletePage("Order");
                        NavigationService.Navigate(deletePage);
                    }
                    else { WarningText.Content = "Das eingegebene Log In hat nicht genügend Rechte, um auf alle Bestellungen zu löschen."; }

                    if (Destination == "DeleteAllLogIns" && login.Rank == 1)
                    {
                        DeletePage deletePage = new DeletePage("Login");
                        NavigationService.Navigate(deletePage);
                    }
                    else { WarningText.Content = "Das eingegebene Log In hat nicht genügend Rechte, um auf alle Logins zu löschen."; }

                    if (Destination == "DeleteAllEmployees" && login.Rank == 1)
                    {
                        DeletePage deletePage = new DeletePage("Employee");
                        NavigationService.Navigate(deletePage);
                    }
                    else { WarningText.Content = "Das eingegebene Log In hat nicht genügend Rechte, um auf alle Mitarbeiter zu löschen."; }

                    if (Destination== "DeleteAllContracts" && login.Rank ==1)
                    {
                        DeletePage deletePage = new DeletePage("Contract");
                        NavigationService.Navigate(deletePage);
                    }
                    else { WarningText.Content = "Das eingegebene Log In hat nicht genügend Rechte, um auf alle Verträge zu löschen."; }

                    if (Destination == "DeleteAllArticles" && login.Rank == 1)
                    {
                        DeletePage deletePage = new DeletePage("Article");
                        NavigationService.Navigate(deletePage);
                    }
                    else { WarningText.Content = "Das eingegebene Log In hat nicht genügend Rechte, um auf alle Artikel zu löschen."; }

                    if (Destination == "DeleteEverything" && login.Rank == 1)
                    {
                        DeletePage deletePage = new DeletePage("Home");
                        NavigationService.Navigate(deletePage);
                    }
                    else { WarningText.Content = "Das eingegebene Log In hat nicht genügend Rechte, um alles zu löschen."; }

                }
                else { WarningText.Content = "Das eingegebene Log In Passwort ist falsch. Bitte geben Sie das richtige Passwort ein."; } //hier logging
            }
            else { WarningText.Content = "Der eingegebene Log In Name exisitert nicht. Bitte geben Sie einen vorhandenen Log In Namen ein."; }

            if (Destination == "AddLogIn")
            {
                AddEditLogin addEditLogin = new AddEditLogin("FirstTime");
                NavigationService.Navigate(addEditLogin);
            }
      

        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            switch (Destination)
            {
                case "Inventar":
                    GymHomepage homepage = new GymHomepage();
                    NavigationService.Navigate(homepage);
                    break;

                case "Contract":
                    GymHomepage homepage1 = new GymHomepage();
                    NavigationService.Navigate(homepage1);
                    break;

                case "Employee":
                    GymHomepage homepage2 = new GymHomepage();
                    NavigationService.Navigate(homepage2);
                    break;

                case "Member":
                    GymHomepage homepage3 = new GymHomepage();
                    NavigationService.Navigate(homepage3);
                    break;

                case "Order":
                    GymHomepage homepage4 = new GymHomepage();
                    NavigationService.Navigate(homepage4);
                    break;

                case "Login":
                    GymHomepage homepage5 = new GymHomepage();
                    NavigationService.Navigate(homepage5);
                    break;

                case "DeleteAllMembers":
                    MemberPage memberPage = new MemberPage();
                    NavigationService.Navigate(memberPage);
                    break;

                case "DeleteAllOrders":
                    OrderPage orderPage = new OrderPage();
                    NavigationService.Navigate(orderPage);
                    break;

                case "DeleteAllLogIns":
                    LoginPage loginPage = new LoginPage();
                    NavigationService.Navigate(loginPage);
                    break;

                case "DeleteAllEmployees":
                    EmployeePage employeePage = new EmployeePage();
                    NavigationService.Navigate(employeePage);
                    break;

                case "DeleteAllContracts":
                    ContractPage contractPage = new ContractPage();
                    NavigationService.Navigate(contractPage);
                    break;

                case "DeleteAllArticles":
                    ArticlePage articlePage = new ArticlePage();
                    NavigationService.Navigate(articlePage);
                    break;

                case "DeleteEverything":
                    GymHomepage homepage6 = new GymHomepage();
                    NavigationService.Navigate(homepage6);
                    break;
            }
        }

        private void Password_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                string username = Username.Text;
                string password = PasswordBox.Password.ToString();
                LogIn login = query.GetLogInDetails(Username.Text);
                if (login != null)
                {
                    if (login.LogInPassword == PasswordBox.Password.ToString())
                    {
                        if (Destination == "Member" && login.Rank <= 2)
                        {
                            MemberPage memberPage = new MemberPage();
                            NavigationService.Navigate(memberPage);
                        }
                        else { WarningText.Content = "Das eingegebene Log In hat nicht genügend Rechte, um auf die Mitglieder zugreifen zu können."; }

                        if (Destination == "Contract" && login.Rank <= 2)
                        {
                            ContractPage contractPage = new ContractPage();
                            NavigationService.Navigate(contractPage);
                        }
                        else { WarningText.Content = "Das eingegebene Log In hat nicht genügend Rechte, um auf die Verträge zugreifen zu können."; }

                        if (Destination == "Employee" && login.Rank == 1)
                        {
                            EmployeePage employeePage = new EmployeePage();
                            NavigationService.Navigate(employeePage);
                        }
                        else { WarningText.Content = "Das eingegebene Log In hat nicht genügend Rechte, um auf die Mitarbeiter zugreifen zu können."; }

                        if (Destination == "Inventar" && login.Rank <= 2)
                        {
                            ArticlePage articlePage = new ArticlePage();
                            NavigationService.Navigate(articlePage);
                        }
                        else { WarningText.Content = "Das eingegebene Log In hat nicht genügend Rechte, um auf das Inventar zugreifen zu können."; }

                        if (Destination == "Login" && login.Rank == 1)
                        {
                            LoginPage loginPage = new LoginPage();
                            NavigationService.Navigate(loginPage);
                        }
                        else { WarningText.Content = "Das eingegebene Log In hat nicht genügend Rechte, um auf die Logins zugreifen zu können."; }

                        if (Destination == "Order" && login.Rank <= 2)
                        {
                            OrderPage orderPage = new OrderPage();
                            NavigationService.Navigate(orderPage);
                        }
                        else { WarningText.Content = "Das eingegebene Log In hat nicht genügend Rechte, um auf die Bestellungen zugreifen zu können."; }

                        if (Destination == "Homepage")
                        {
                            GymHomepage homepage = new GymHomepage();
                            NavigationService.Navigate(homepage);
                        }

                        if (Destination == "DeleteAllMembers" && login.Rank == 1)
                        {
                            DeletePage deletePage = new DeletePage("Member");
                            NavigationService.Navigate(deletePage);
                        }
                        else { WarningText.Content = "Das eingegebene Log In hat nicht genügend Rechte, um auf alle Mitglieder zu löschen."; }

                        if (Destination == "DeleteAllOrders" && login.Rank == 1)
                        {
                            DeletePage deletePage = new DeletePage("Order");
                            NavigationService.Navigate(deletePage);
                        }
                        else { WarningText.Content = "Das eingegebene Log In hat nicht genügend Rechte, um auf alle Bestellungen zu löschen."; }

                        if (Destination == "DeleteAllLogIns" && login.Rank == 1)
                        {
                            DeletePage deletePage = new DeletePage("Login");
                            NavigationService.Navigate(deletePage);
                        }
                        else { WarningText.Content = "Das eingegebene Log In hat nicht genügend Rechte, um auf alle Logins zu löschen."; }

                        if (Destination == "DeleteAllEmployees" && login.Rank == 1)
                        {
                            DeletePage deletePage = new DeletePage("Employee");
                            NavigationService.Navigate(deletePage);
                        }
                        else { WarningText.Content = "Das eingegebene Log In hat nicht genügend Rechte, um auf alle Mitarbeiter zu löschen."; }

                        if (Destination == "DeleteAllContracts" && login.Rank == 1)
                        {
                            DeletePage deletePage = new DeletePage("Contract");
                            NavigationService.Navigate(deletePage);
                        }
                        else { WarningText.Content = "Das eingegebene Log In hat nicht genügend Rechte, um auf alle Verträge zu löschen."; }

                        if (Destination == "DeleteAllArticles" && login.Rank == 1)
                        {
                            DeletePage deletePage = new DeletePage("Article");
                            NavigationService.Navigate(deletePage);
                        }
                        else { WarningText.Content = "Das eingegebene Log In hat nicht genügend Rechte, um auf alle Artikel zu löschen."; }

                        if (Destination == "DeleteEverything" && login.Rank == 1)
                        {
                            DeletePage deletePage = new DeletePage("Home");
                            NavigationService.Navigate(deletePage);
                        }
                        else { WarningText.Content = "Das eingegebene Log In hat nicht genügend Rechte, um alles zu löschen."; }

                    }
                    else { WarningText.Content = "Das eingegebene Log In Passwort ist falsch. Bitte geben Sie das richtige Passwort ein."; } //hier logging
                }
                else { WarningText.Content = "Der eingegebene Log In Name exisitert nicht. Bitte geben Sie einen vorhandenen Log In Namen ein."; }

                if (Destination == "AddLogIn")
                {
                    AddEditLogin addEditLogin = new AddEditLogin("FirstTime");
                    NavigationService.Navigate(addEditLogin);
                }

            }
        }
    }
}
