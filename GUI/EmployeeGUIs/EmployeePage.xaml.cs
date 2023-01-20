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
using log4net;
using log4net.Config;
using log4net.Repository;
using loremipsum.Gym;

namespace GUI.EmployeeGUIs
{
    /// <summary>
    /// Interaktionslogik für Page1.xaml
    /// </summary>
    public partial class EmployeePage : Page
    {
        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        public EmployeePage()
        {
            InitializeComponent();
        }

        private void EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            IDCheck iDCheck = new IDCheck("EditEmployee");
            NavigationService.Navigate(iDCheck);
        }

        private void EmployeeData_Loaded(object sender, RoutedEventArgs e)
        {
            EmployeeData.ItemsSource = admin.ListEmployees();
        }

        private void DeleteEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            GymPasswordPage gymPasswordPage = new GymPasswordPage("DeleteAllEmployees");
            NavigationService.Navigate(gymPasswordPage);
        }

        private void CheckoutButton_Click(object sender, RoutedEventArgs e)
        {
            DeletePage deletePage = new DeletePage("CheckoutMembers");
            NavigationService.Navigate(deletePage);
        }

        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            AddAndEditEmployee addEmployee = new AddAndEditEmployee();
            NavigationService.Navigate(addEmployee);
        }

        private void DeleteEmployeeButton1_Click(object sender, RoutedEventArgs e)
        {
            IDCheck iDCheck = new IDCheck("DeleteEmployee");
            NavigationService.Navigate(iDCheck);
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GymHomepage gymHomepage = new GymHomepage();
            NavigationService.Navigate(gymHomepage);
        }
    }
}
