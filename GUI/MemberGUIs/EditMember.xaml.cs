using GUI.MemberGUIs;
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

namespace GUI.MemberGUIs
{
    /// <summary>
    /// Interaktionslogik für EditMember.xaml
    /// </summary>
    public partial class EditMember : Page
    {

        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        private int memberID;
        public EditMember(int memberID)
        {
            InitializeComponent();

            this.memberID = memberID;
        }

        private void EditMemberButton_Click(object sender, RoutedEventArgs e)
        {
            admin.UpdateMember(memberID, NameE.Text, SurnameE.Text, AdressE.Text, Int32.Parse(PostalCodeE.Text), CityE.Text, CountryE.Text, ContactAdressE.Text,
                ContoE.Text, DateTime.Parse(BirthdayE.Text));

           
            GymHomepage home = new GymHomepage();
            NavigationService.Navigate(home);
        }
    }
}
