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

namespace GUI
{
    /// <summary>
    /// Interaktionslogik für EmployeeChangeOptions.xaml
    /// </summary>
    public partial class EmployeeChangeOptions : Page
    {
        public EmployeeChangeOptions()
        {
            InitializeComponent();
        }

        private void AddMember_Click(object sender, RoutedEventArgs e)
        {
            AddEmployee addEmployee = new AddEmployee();
            NavigationService.Navigate(addEmployee);
        }

        private void DeleteMember_Click(object sender, RoutedEventArgs e)
        {
            DeleteEmployee deleteEmployee = new DeleteEmployee();
            NavigationService.Navigate(deleteEmployee);
        }

        private void EditMember_Click(object sender, RoutedEventArgs e)
        {
            //todo
        }
    }
}
