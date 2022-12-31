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
    /// Interaktionslogik für ContractChangeOptions.xaml
    /// </summary>
    public partial class ContractChangeOptions : Page
    {
        public ContractChangeOptions()
        {
            InitializeComponent();
        }

        private void ChangeContractButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeContract changeContract = new ChangeContract();
            NavigationService.Navigate(changeContract);
        }

        private void PauseContractButton_Click(object sender, RoutedEventArgs e)
        {
            PauseContract pauseContract = new PauseContract();
            NavigationService.Navigate(pauseContract);
        }

        private void DeleteContractButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteContract deleteContract = new DeleteContract();
            NavigationService.Navigate(deleteContract);
        }
    }
}
