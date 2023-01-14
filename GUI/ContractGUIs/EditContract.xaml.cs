using loremipsum.Gym;
using loremipsum.Gym.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace GUI.ContractGUIs
{
    /// <summary>
    /// Interaktionslogik für EditContract.xaml
    /// </summary>
    public partial class EditContract : Page
    {
        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        private readonly IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];
        private int ID;
        private Contract contract;
        public EditContract(int ID)
        {
            InitializeComponent();
            this.ID = ID;
            contract = query.GetContractDetails(ID);
            ContractTypeBox.Text = contract.ContractType;
            PriceBox.Text = ((double) contract.Price / 100).ToString();
        }

        private void EditContractButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ContractTypeBox.Text.IsNullOrEmpty() && !PriceBox.Text.IsNullOrEmpty())
            {
                if (PriceBox.Text.Contains(".") || PriceBox.Text.Contains(","))
                {
                    string[] string_remove = { ".", "," };
                    string euroPrice = PriceBox.Text;

                    foreach (string c in string_remove)
                    {
                        euroPrice = euroPrice.Replace(c, "");
                    }

                    int centPrice = Int32.Parse(euroPrice);

                    admin.UpdateContract(ID, ContractTypeBox.Text, centPrice);

                    GymHomepage home = new GymHomepage();
                    NavigationService.Navigate(home);
                }
                else
                {
                    admin.UpdateContract(ID, ContractTypeBox.Text, Int32.Parse(PriceBox.Text));

                    GymHomepage home = new GymHomepage();
                    NavigationService.Navigate(home);
                }
            }
            else
            {
                WarningLabel.Content = "Bitte geben Sie gültige Daten ein!";
            }


        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if (!ContractTypeBox.Text.IsNullOrEmpty() && !PriceBox.Text.IsNullOrEmpty())
                {
                    if (PriceBox.Text.Contains(".") || PriceBox.Text.Contains(","))
                    {
                        string[] string_remove = { ".", "," };
                        string euroPrice = PriceBox.Text;

                        foreach (string c in string_remove)
                        {
                            euroPrice = euroPrice.Replace(c, "");
                        }

                        int centPrice = Int32.Parse(euroPrice);

                        admin.UpdateContract(ID, ContractTypeBox.Text, centPrice);

                        GymHomepage home = new GymHomepage();
                        NavigationService.Navigate(home);
                    }
                    else
                    {
                        admin.UpdateContract(ID, ContractTypeBox.Text, Int32.Parse(PriceBox.Text));

                        GymHomepage home = new GymHomepage();
                        NavigationService.Navigate(home);
                    }
                }
                else
                {
                    WarningLabel.Content = "Bitte geben Sie gültige Daten ein!";
                }
            }
        }

        private void PriceBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            AllowEuroPrice(e);
        }

        public static void AllowEuroPrice(TextCompositionEventArgs e)
        {
            int result;

            if (!(int.TryParse(e.Text, out result) || e.Text == "." || e.Text == ","))
            {
                e.Handled = true;
            }
        }
    }
}
