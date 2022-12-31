using loremipsum.Gym;
using loremipsum.Gym.Entities;
using Microsoft.EntityFrameworkCore.Query.Internal;
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
    /// Interaktionslogik für AddMember.xaml
    /// </summary>
    public partial class AddMember : Page
    {

        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
       
        
        public AddMember()
        {
            InitializeComponent();    
        }

       
         private void Button_Click(object sender, RoutedEventArgs e)
        {

            
          Member memberToAdd = new(NameM.Text, SurnameM.Text, AdressM.Text, Int32.Parse(PostalCodeM.Text), CityM.Text,
                CountryM.Text, ContactAdressM.Text, Int32.Parse(ContoM.Text), DateTime.Parse(BirthdayM.Text));
            memberToAdd.Contract = null;
            memberToAdd.CurrentBill = 0;
        
           admin.AddMember(memberToAdd);
          // keine objektreferenz???
            GymHomepage home = new GymHomepage();
           NavigationService.Navigate(home);
        }



       
    }
}
