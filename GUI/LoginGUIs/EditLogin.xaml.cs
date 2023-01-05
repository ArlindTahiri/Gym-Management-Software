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
    /// Interaktionslogik für EditLogin.xaml
    /// </summary>
    public partial class EditLogin : Page
    {
        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        private string loginName;

        public EditLogin(string loginName)
        {
            InitializeComponent();

            this.loginName = loginName;
        }

        private void editLogin_Click(object sender, RoutedEventArgs e)
        {

            admin.UpdateLogIn(loginName, newLogInName.Text, newLogInPassword.Text, Int32.Parse(newRank.Text));

            GymHomepage home = new GymHomepage();
            NavigationService.Navigate(home);
        }
    }
}
