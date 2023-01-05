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
    /// Interaktionslogik für EditLoginCheck.xaml
    /// </summary>
    public partial class EditLoginCheck : Page
    {
        private IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];

        public EditLoginCheck()
        {
            InitializeComponent();
        }


        public void LoginCheck_KeyDown(object sender, KeyEventArgs e)
        {
            string loginName = LoginCheck.Text;
            if(e.Key == Key.Enter)
            {
                if(query.GetLogInDetails(loginName) != null)
                {
                    EditLogin editLogin = new EditLogin(loginName);
                    NavigationService.Navigate(editLogin);
                }
            }
        }
    }
}
