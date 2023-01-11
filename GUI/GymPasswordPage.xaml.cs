using GUI.ArticleGUIs;
using GUI.ContractGUIs;
using GUI.EmployeeGUIs;
using GUI.LoginGUIs;
using GUI.MemberGUIs;
using GUI.Order_GUIs;
using loremipsum.Gym;
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
        public GymPasswordPage(string destination)
        {
            Destination = destination;
            InitializeComponent();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            string username = Username.Text;
            string password = Password.Text;
            IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];

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
                LoginPage loginPage = new LoginPage();
                NavigationService.Navigate(loginPage);
            }

            if(Destination == "Order")
            {
                OrderPage orderPage = new OrderPage();
                NavigationService.Navigate(orderPage);
            }
        }
    }
}
