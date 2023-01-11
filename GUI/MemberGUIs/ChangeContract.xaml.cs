using loremipsum.Gym;
using loremipsum.Gym.Entities;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
using Contract = loremipsum.Gym.Entities.Contract;

namespace GUI.MemberGUIs
{
    /// <summary>
    /// Interaktionslogik für ChangeContract.xaml
    /// </summary>
    public partial class ChangeContract : Page
    {

        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];


        public ChangeContract()
        {
            InitializeComponent();
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            int t;
            Contract c = (Contract)ContractCB.SelectedItem;
            if (!ContractCB.Text.IsNullOrEmpty())
            {
                IList<Member> members = admin.ListMembers();
                foreach(Member member in members)
                {
                    if(member.MemberID == Int32.Parse(IDCheck.Text))
                    {
                        if (c.ContractID != member.ContractID)
                        {

                            admin.UpdateContractFromMember(Int32.Parse(IDCheck.Text), c.ContractID);

                            GymHomepage home = new GymHomepage();
                            NavigationService.Navigate(home);
                        }
                        else
                        {
                            WarningText.Text = "Dies ist schon der aktuelle Vertrag des Mitgliedes";
                            break;
                        }
                    }
                    else
                    {
                        WarningText.Text = "Dieses Mitglied existiert nicht";
                    }
                }
            }
            else
            {
                WarningText.Text = "Bitte geben Sie für alle Daten etwas ein!";
            }
        }

        private void MemberData_Loaded(object sender, RoutedEventArgs e)
        {
            MemberData.ItemsSource = admin.ListMembers();
        }

        private void IDCheck_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextValidation.CheckIsNumeric(e);
        }

        private void ContractCB_Loaded(object sender, RoutedEventArgs e)
        {
            ContractCB.Items.Clear();
            ContractCB.ItemsSource = admin.ListContracts();
        }
    }
}
