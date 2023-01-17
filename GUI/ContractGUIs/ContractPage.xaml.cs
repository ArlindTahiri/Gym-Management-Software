using log4net;
using log4net.Config;
using log4net.Repository;
using loremipsum.Gym;
using System;
using System.Collections.Generic;
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
            IDCheck iDCheck = new IDCheck("DeleteContract");
            NavigationService.Navigate(iDCheck);
        }

        private void AddContractButton_Click(object sender, RoutedEventArgs e)
        {
            AddEditContract addContract = new AddEditContract();
            NavigationService.Navigate(addContract);
        }

        private void ContractInventory_Loaded(object sender, RoutedEventArgs e)
        {
            ContractInventory.ItemsSource = admin.ListContracts();
        }

        private void DeleteContractsButton_Click(object sender, RoutedEventArgs e)
        {
            DeletePage deletePage = new DeletePage("Contract");
            NavigationService.Navigate(deletePage);
        }

        private void ChangeContractButton_Click(object sender, RoutedEventArgs e)
        {
            IDCheck iDCheck = new IDCheck("EditContract");
            NavigationService.Navigate(iDCheck);
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GymHomepage gymHomepage = new GymHomepage();
            NavigationService.Navigate(gymHomepage);
        }
    }
}
