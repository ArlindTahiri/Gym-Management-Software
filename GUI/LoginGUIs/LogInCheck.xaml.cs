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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GUI.LoginGUIs
{
    /// <summary>
    /// Interaction logic for LogInCheck.xaml
    /// </summary>
    public partial class LogInCheck : Page
    {
        private IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];
        private string Destination;

        public LogInCheck(string destination)
        {
            Destination= destination;
            InitializeComponent();
            if (destination == "Delete") { Label1.Content = "Geben sie bitte den Login ein, welchen Sie löschen wollen"; }
            if (destination == "Edit") { Label1.Content = "Geben sie bitte den Login ein, welchen Sie bearbeiten wollen"; }
        }

        public void LoginCheck_KeyDown(object sender, KeyEventArgs e)
        {
            string loginName = LoginCheck.Text;
            if (e.Key == Key.Enter)
            {
                if (query.GetLogInDetails(loginName) != null)
                {
                    if (Destination == "Edit")
                    {
                        EditLogin editLogin = new EditLogin(loginName);
                        NavigationService.Navigate(editLogin);
                    }
                    if(Destination == "Delete")
                    {
                        DeletePage deletePage = new DeletePage("DeleteLogin", loginName);
                        NavigationService.Navigate(deletePage);
                    }
                }
                else
                {
                    WarningText.Text = "Der Benutzername exisitert nicht.\n Bitte geben Sie einen gültigen Benutzernamen ein.";
                }
            }
        }
    }
}
