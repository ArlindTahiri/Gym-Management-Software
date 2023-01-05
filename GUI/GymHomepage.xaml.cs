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
using GUI.ArticleGUIs;
using GUI.EmployeeGUIs;
using GUI.Order_GUIs;

namespace GUI
{
    /// <summary>
    /// Interaktionslogik für GymHomepage.xaml
    /// </summary>
    public partial class GymHomepage : Page
    {
        public GymHomepage()
        {
            InitializeComponent();
        }

        private void Member_Click(object sender, RoutedEventArgs e)
        {
            GymPasswordPage gymPasswordPage = new GymPasswordPage("Member");
            NavigationService.Navigate(gymPasswordPage);
        }

        private void Contract_Click(object sender, RoutedEventArgs e)
        {
            GymPasswordPage gymPasswordPage = new GymPasswordPage("Contract");
            NavigationService.Navigate(gymPasswordPage);
        }

        private void EmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            GymPasswordPage gymPasswordPage = new GymPasswordPage("Employee");
            NavigationService.Navigate(gymPasswordPage);
        }

        private void Inventar_Click(object sender, RoutedEventArgs e)
        {
            GymPasswordPage gymPasswordPage = new GymPasswordPage("Inventar");
            NavigationService.Navigate(gymPasswordPage);
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
           OrderOptions orderOptions = new OrderOptions();
            NavigationService.Navigate(orderOptions);
        }

        private void Logins_Click(object sender, RoutedEventArgs e)
        {
            GymPasswordPage gymPasswordPage = new GymPasswordPage("login");
            NavigationService.Navigate(gymPasswordPage);
        }
    }
}
