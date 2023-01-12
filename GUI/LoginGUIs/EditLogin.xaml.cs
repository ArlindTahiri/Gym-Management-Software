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

namespace GUI.LoginGUIs
{
    /// <summary>
    /// Interaktionslogik für EditLogin.xaml
    /// </summary>
    public partial class EditLogin : Page
    {
        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        private IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];
        private LogIn logIn;

        public EditLogin(string loginName)
        {
            InitializeComponent();
            logIn = query.GetLogInDetails(loginName);
            newLogInName.Text = logIn.LogInName;
            newLogInPassword.Text = logIn.LogInPassword;
            newRank.Text = logIn.Rank.ToString();
        }

        private void editLogin_Click(object sender, RoutedEventArgs e)
        {
            UpdateLogIn();
        }

        private void ChangeLogin(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                UpdateLogIn();
            }
        }

        private void UpdateLogIn()
        {
            if(query.GetLogInDetails(newLogInName.Text) == null)
            {
                admin.UpdateLogIn(logIn.LogInName, newLogInName.Text, newLogInPassword.Text, Int32.Parse(newRank.Text));
                LoginPage loginPage = new LoginPage();
                NavigationService.Navigate(loginPage);
            }
            else
            {
                WarningText.Text = "Der neue Benutzername existiert bereits.\n Bitte geben Sie einen neuen Nutzernamen ein.";
            }
        }
    }
}
