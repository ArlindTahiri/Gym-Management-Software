using GUI.EmployeeGUIs;
using loremipsum.Gym;
using loremipsum.Gym.Entities;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
       
       
        private void PostalCode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextValidation.CheckIsNumeric(e);
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MemberPage memberPage = new MemberPage();
            NavigationService.Navigate(memberPage);
        }

        private void ContractCB_Loaded(object sender, RoutedEventArgs e)
        {
            ContractCB.ItemsSource = admin.ListContracts();
        }

        private void EnterMember(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                if (!ContractCB.Text.IsNullOrEmpty() && !Name.Text.IsNullOrEmpty() && !Surname.Text.IsNullOrEmpty() && !Adress.Text.IsNullOrEmpty()
                && !PostalCode.Text.IsNullOrEmpty() && !City.Text.IsNullOrEmpty() && !Country.Text.IsNullOrEmpty() && !EMail.Text.IsNullOrEmpty() && !Iban.Text.IsNullOrEmpty() && !Birthday.Text.IsNullOrEmpty())
                {
                    if (TextValidation.CheckIsPostalCode(PostalCode.Text))
                    {
                        if (TextValidation.CheckIsMail(EMail.Text))
                        {
                            if (TextValidation.CheckIsIban(Iban.Text))
                            {
                                if (TextValidation.CheckIsDate(Birthday.Text))
                                {
                                    Contract c = (Contract)ContractCB.SelectedItem;
                                    admin.AddMember(c.ContractID, Name.Text, Surname.Text, Adress.Text, Int32.Parse(PostalCode.Text), City.Text,
                                          Country.Text, EMail.Text, Iban.Text, DateTime.Parse(Birthday.Text));

                                    GymHomepage home = new GymHomepage();
                                    NavigationService.Navigate(home);
                                }
                                else
                                {
                                    WarningLabel.Content = "Bitte geben Sie einen gültigen Geburtstag ein";
                                }
                            }
                            else
                            {
                                WarningLabel.Content = "Bitte geben Sie eine gültige IBan ein";
                            }
                        }
                        else
                        {
                            WarningLabel.Content = "Bitte geben Sie eine gültige E-Mail Adresse ein.";
                        }
                    }
                    else
                    {
                        WarningLabel.Content = "Bitte geben Sie eine gültige Postleitzahl ein";
                    }
                }
                else
                {
                    WarningLabel.Content = "Bitte geben Sie für alle Daten etwas ein!";
                }
            }
        }

       
    }
}
