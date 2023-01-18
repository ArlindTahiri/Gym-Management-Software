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
            Name.Text = member.Forename;
            Surname.Text = member.Surname;
            Adress.Text = member.Street;
            PostalCode.Text = member.PostcalCode.ToString();
            City.Text = member.City;
            Country.Text = member.Country;
            EMail.Text = member.EMail;
            Iban.Text = member.Iban;
            Birthday.Text = member.Birthday.ToString("dd.MM.yyyy");

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

        private void ChangeMember(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (!Name.Text.IsNullOrEmpty() && !Surname.Text.IsNullOrEmpty() && !Adress.Text.IsNullOrEmpty()
                && !PostalCode.Text.IsNullOrEmpty() && !City.Text.IsNullOrEmpty() && !Country.Text.IsNullOrEmpty() && !EMail.Text.IsNullOrEmpty()
                && !Iban.Text.IsNullOrEmpty() && !Birthday.Text.IsNullOrEmpty())
                {
                    if (TextValidation.CheckIsPostalCode(PostalCode.Text))
                    {
                        if (TextValidation.CheckIsMail(EMail.Text))
                        {
                            if (TextValidation.CheckIsIban(Iban.Text))
                            {
                                if (TextValidation.CheckIsDate(Birthday.Text))
                                {
                                    admin.UpdateMember(memberID, Name.Text, Surname.Text, Adress.Text, Int32.Parse(PostalCode.Text), City.Text, Country.Text, EMail.Text,
                                    Iban.Text, DateTime.Parse(Birthday.Text));

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
