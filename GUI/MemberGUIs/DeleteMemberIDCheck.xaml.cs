using GUI.MemberGUIs;
using loremipsum.Gym;
using loremipsum.Gym.Entities;
using Microsoft.IdentityModel.Tokens;
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

namespace GUI.MemberGUIs
{
    /// <summary>
    /// Interaktionslogik für DeleteMemberIDCheck.xaml
    /// </summary>
    public partial class DeleteMemberIDCheck : Page
    {


        private readonly IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];
        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        public DeleteMemberIDCheck()
        {
            InitializeComponent();
        }

        private void IDCheck_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                if (!IDCheck.Text.IsNullOrEmpty())
                {
                    if (query.GetMemberDetails(Int32.Parse(IDCheck.Text)) != null)
                    {

                        DeletePage deletePage = new DeletePage("DeleteMember",Int32.Parse(IDCheck.Text));
                        NavigationService.Navigate(deletePage);
                    } else
                    {
                        WarningText.Text = "Die eingebene ID ist ungültig. Bitte geben Sie eine gültige ID ein.";
                    }
                }
                else
                {

                    WarningText.Text = "Die eingebene ID ist ungültig. Bitte geben Sie eine gültige ID ein.";
                }
            }
        }

        private void IDCheck_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextValidation.CheckIsNumeric(e);
        }


        private void MemberData_Loaded(object sender, RoutedEventArgs e)
        {
            MemberData.ItemsSource = admin.ListMembers();
        }
    }
}
