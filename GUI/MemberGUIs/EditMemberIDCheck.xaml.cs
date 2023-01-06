using GUI.MemberGUIs;
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

namespace GUI.MemberGUIs
{
    /// <summary>
    /// Interaktionslogik für EditMemberIDCheck.xaml
    /// </summary>
    public partial class EditMemberIDCheck : Page
    {
        private IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];
        public EditMemberIDCheck()
        {
            InitializeComponent();
        }

        private void IDCheck_KeyDown(object sender, KeyEventArgs e)
        {
            string content = IDCheck.Text;
            if (e.Key == Key.Enter)
            {
                if (query.GetMemberDetails(Int32.Parse(content))!=null)
                {
                   
                    EditMember editMember = new EditMember(Int32.Parse(content));
                    NavigationService.Navigate(editMember);
                } else
                {
                    WarningText.Text = "Die eingebene ID ist ungültig. Bitte geben Sie eine gültige ID ein.";
                }
            }
        }

    }
}
