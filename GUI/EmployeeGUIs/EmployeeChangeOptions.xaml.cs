using GUI.EmployeeGUIs;
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

namespace GUI.EmployeeGUIs
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

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            AddEmployee addEmployee = new AddEmployee();
            NavigationService.Navigate(addEmployee);
        }

        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            DeleteEmployeeIDCheck deleteEmployeeIDCheck = new DeleteEmployeeIDCheck();
            NavigationService.Navigate(deleteEmployeeIDCheck);
        }

        private void EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            EditEmployeeIDCheck editEmployeeIDCheck = new EditEmployeeIDCheck();
            NavigationService.Navigate(editEmployeeIDCheck);
        }
    }
}
