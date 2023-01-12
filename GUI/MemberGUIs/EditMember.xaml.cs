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
    /// Interaktionslogik für EditMember.xaml
    /// </summary>
    public partial class EditMember : Page
    {

        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        private readonly IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];
        private int memberID;
        private Member member;
        public EditMember(int memberID)
        {
            InitializeComponent();

            this.memberID = memberID;
            member = query.GetMemberDetails(memberID);
            NameE.Text = member.Forename;
            SurnameE.Text = member.Surname;
            AdressE.Text = member.Street;
            PostalCodeE.Text = member.PostcalCode.ToString();
            CityE.Text = member.City;
            CountryE.Text = member.Country;
            ContactAdressE.Text = member.EMail;
            ContoE.Text = member.Iban;
            BirthdayE.Text = member.Birthday.ToString("dd.MM.yyyy");

        } 

        private void EditMemberButton_Click(object sender, RoutedEventArgs e)
        {
            if (!NameE.Text.IsNullOrEmpty() && !SurnameE.Text.IsNullOrEmpty() && !AdressE.Text.IsNullOrEmpty()
                && !PostalCodeE.Text.IsNullOrEmpty() && !CityE.Text.IsNullOrEmpty() && !CountryE.Text.IsNullOrEmpty() && !ContactAdressE.Text.IsNullOrEmpty()
                && !ContoE.Text.IsNullOrEmpty() && !BirthdayE.Text.IsNullOrEmpty())
            {
                if (TextValidation.CheckIsPostalCode(PostalCodeE.Text))
                {
                    if (TextValidation.CheckIsMail(ContactAdressE.Text))
                    {
                        if (TextValidation.CheckIsIban(ContoE.Text))
                        {
                            if (TextValidation.CheckIsDate(BirthdayE.Text))
                            {
                                admin.UpdateMember(memberID, NameE.Text, SurnameE.Text, AdressE.Text, Int32.Parse(PostalCodeE.Text), CityE.Text, CountryE.Text, ContactAdressE.Text,
                                ContoE.Text, DateTime.Parse(BirthdayE.Text));

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

        private void PostalCodeE_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextValidation.CheckIsNumeric(e);
        }

        private void ChangeMember(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (!NameE.Text.IsNullOrEmpty() && !SurnameE.Text.IsNullOrEmpty() && !AdressE.Text.IsNullOrEmpty()
                && !PostalCodeE.Text.IsNullOrEmpty() && !CityE.Text.IsNullOrEmpty() && !CountryE.Text.IsNullOrEmpty() && !ContactAdressE.Text.IsNullOrEmpty()
                && !ContoE.Text.IsNullOrEmpty() && !BirthdayE.Text.IsNullOrEmpty())
                {
                    if (TextValidation.CheckIsPostalCode(PostalCodeE.Text))
                    {
                        if (TextValidation.CheckIsMail(ContactAdressE.Text))
                        {
                            if (TextValidation.CheckIsIban(ContoE.Text))
                            {
                                if (TextValidation.CheckIsDate(BirthdayE.Text))
                                {
                                    admin.UpdateMember(memberID, NameE.Text, SurnameE.Text, AdressE.Text, Int32.Parse(PostalCodeE.Text), CityE.Text, CountryE.Text, ContactAdressE.Text,
                                    ContoE.Text, DateTime.Parse(BirthdayE.Text));

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
