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
    /// Interaktionslogik für LoginChangeOptions.xaml
    /// </summary>
    public partial class LoginChangeOptions : Page
    {
        public LoginChangeOptions()
        {
            InitializeComponent();
        }

        private void AddLogin_Click(object sender, RoutedEventArgs e)
        {
            AddLogin addLogin = new AddLogin();
            NavigationService.Navigate(addLogin);
        }

        private void DeleteLogin_Click(object sender, RoutedEventArgs e)
        {
            DeleteLoginCheck deleteLoginCheck = new DeleteLoginCheck();
            NavigationService.Navigate(deleteLoginCheck);
        }

        private void EditLogin_Click(object sender, RoutedEventArgs e)
        {
            EditLoginCheck editLoginCheck = new EditLoginCheck();
            NavigationService.Navigate(editLoginCheck);
        }
    }
}
