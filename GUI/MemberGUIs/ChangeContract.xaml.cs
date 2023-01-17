using log4net;
using log4net.Config;
using log4net.Repository;
using loremipsum.Gym;
using loremipsum.Gym.Entities;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Reflection;
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
        private readonly IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];     

        public ChangeContract()
        {
            InitializeComponent();           
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            Contract c = (Contract)ContractCB.SelectedItem;
            if (!ContractCB.Text.IsNullOrEmpty())
            {
                if (query.GetMemberDetails(Int32.Parse(IDCheck.Text)) != null)
                {
                    Member changingMember = query.GetMemberDetails(Int32.Parse(IDCheck.Text));
                    Contract contractChanging = query.GetContractDetails(changingMember.ContractID);
                    if (c.ContractID != changingMember.ContractID)
                    {
                        if(!admin.UpdateContractFromMember(Int32.Parse(IDCheck.Text), c.ContractID)) { MessageBox.Show("Bitte schließen Sie alle anderen Anwendungen, die gerade auf MemberBills.csv zugreifen."); }
                        else {
                            MemberPage memberPage = new MemberPage();
                            NavigationService.Navigate(memberPage);
                        }
                    }
                    else
                    {
                        WarningText.Text = "Dies ist schon der aktuelle Vertrag des Mitgliedes";
                    }
                }
                else
                {
                    WarningText.Text = "Dieses Mitglied existiert nicht";
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
            ContractCB.ItemsSource = admin.ListContracts();
        }

        private void IDCheck_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Contract c = (Contract)ContractCB.SelectedItem;
                if (!ContractCB.Text.IsNullOrEmpty())
                {
                    if (query.GetMemberDetails(Int32.Parse(IDCheck.Text)) != null)
                    {
                        Member changingMember = query.GetMemberDetails(Int32.Parse(IDCheck.Text));
                        Contract contractChanging = query.GetContractDetails(changingMember.ContractID);
                        if (c.ContractID != changingMember.ContractID)
                        {
                            if (!admin.UpdateContractFromMember(Int32.Parse(IDCheck.Text), c.ContractID)) { MessageBox.Show("Bitte schließen Sie alle anderen Anwendungen, die gerade auf MemberBills.csv zugreifen."); }
                            else
                            {
                                MemberPage memberPage = new MemberPage();
                                NavigationService.Navigate(memberPage);
                            }
                        }
                        else
                        {
                            WarningText.Text = "Dies ist schon der aktuelle Vertrag des Mitgliedes";
                        }
                    }
                    else
                    {
                        WarningText.Text = "Dieses Mitglied existiert nicht";
                    }
                }
                else
                {
                    WarningText.Text = "Bitte geben Sie für alle Daten etwas ein!";
                }           
            }
            }
        }
    }
