using loremipsum.Gym;
using loremipsum.Gym.Entities;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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

            if (!ContractID.Text.IsNullOrEmpty() && !NameM.Text.IsNullOrEmpty() && !SurnameM.Text.IsNullOrEmpty() && !AdressM.Text.IsNullOrEmpty()
                && !PostalCodeM.Text.IsNullOrEmpty() && !CityM.Text.IsNullOrEmpty() && !CountryM.Text.IsNullOrEmpty() && !ContoM.Text.IsNullOrEmpty() && !BirthdayM.Text.IsNullOrEmpty())
            {

                admin.AddMember(Int32.Parse(ContractID.Text), NameM.Text, SurnameM.Text, AdressM.Text, Int32.Parse(PostalCodeM.Text), CityM.Text,
                      CountryM.Text, ContactAdressM.Text, ContoM.Text, DateTime.Parse(BirthdayM.Text));

                GymHomepage home = new GymHomepage();
                NavigationService.Navigate(home);
            } else
            {
                WarningLabel.Content = "Bitte geben Sie für alle Daten etwas ein!";
            }
           
        }

        private void ContractIDM_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextValidation.CheckIsNumeric(e);
        }

        private void PostalCodeM_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextValidation.CheckIsNumeric(e);
        }

        private void ContractID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IList<Contract> contracts = admin.ListContracts();
            foreach (Contract i in contracts)
            {
                ContractID.Items.Add(i.ContractID);
            }
        }
    }
}
