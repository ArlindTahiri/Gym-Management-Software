using GUI.EmployeeGUIs;
using loremipsum.Gym;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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
    /// Interaktionslogik für EditEmployee.xaml
    /// </summary>
    public partial class EditEmployee : Page
    {
        private readonly IProductAdmin admin = Application.Current.Properties["IProductAdmin"] as IProductAdmin;
        private int employeeID;
        public EditEmployee(int employeeID)
        {
            InitializeComponent();
            this.employeeID = employeeID;
        }

        private void EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            admin.UpdateEmployee(employeeID, Forename.Text, Surename.Text, Street.Text, Int32.Parse(PostcalCode.Text), City.Text, Country.Text, EMail.Text, Iban.Text, DateTime.Parse(Birthday.Text));
            GymHomepage home = new GymHomepage();
            NavigationService.Navigate(home);
        }
    }
}
