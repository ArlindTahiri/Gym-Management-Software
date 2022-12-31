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
using GUI.EmployeeGUIs;

namespace GUI
{
    /// <summary>
    /// Interaktionslogik für GymHomepage.xaml
    /// </summary>
    public partial class GymHomepage : Page
    {
        public GymHomepage()
        {
            InitializeComponent();
        }

        private void Member_Click(object sender, RoutedEventArgs e)
        {
            GymPasswordMemberPage gymPasswordMemberPage = new GymPasswordMemberPage();
            NavigationService.Navigate(gymPasswordMemberPage);
        }

        private void Contract_Click(object sender, RoutedEventArgs e)
        {
            GymPasswordContractPage gymPasswordPage = new GymPasswordContractPage();
            NavigationService.Navigate(gymPasswordPage);
        }

        private void EmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeePage emplyee = new EmployeePage();
            NavigationService.Navigate(emplyee);
        }
    }
}
