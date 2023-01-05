using loremipsum.Gym;
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

namespace GUI.ContractGUIs
{
    /// <summary>
    /// Interaktionslogik für ContractPage.xaml
    /// </summary>
    public partial class ContractPage : Page
    {

        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        public ContractPage()
        {
          InitializeComponent();
        }

        private void DeleteContractButton_Click(object sender, RoutedEventArgs e)
        {
            ContractIDCheck iDCheck = new ContractIDCheck();
            NavigationService.Navigate(iDCheck);
        }

        private void AddContractButton_Click(object sender, RoutedEventArgs e)
        {
           AddContract addContract = new AddContract();
            NavigationService.Navigate(addContract);
        }

        private void ContractInventory_Loaded(object sender, RoutedEventArgs e)
        {
            ContractInventory.ItemsSource = admin.ListContracts();
        }
    }
}
