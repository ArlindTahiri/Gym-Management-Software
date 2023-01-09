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

namespace GUI.LoginGUIs
{
    /// <summary>
    /// Interaktionslogik für LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {

        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        public LoginPage()
        {
            InitializeComponent();
        }

        private void EditLogins_Click(object sender, RoutedEventArgs e)
        {
            LoginChangeOptions loginChangeOptions = new LoginChangeOptions();
            NavigationService.Navigate(loginChangeOptions);
        }

        private void LoginInventory_Loaded(object sender, RoutedEventArgs e)
        {
           LoginInventory.ItemsSource = admin.ListLogIns();
        }

        private void DeleteLoginsButton_Click(object sender, RoutedEventArgs e)
        {
            DeletePage deletePage = new DeletePage("Login");
            NavigationService.Navigate(deletePage);
        }
    }
}
