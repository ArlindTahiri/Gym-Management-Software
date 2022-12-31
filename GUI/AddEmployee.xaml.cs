using loremipsum.Gym;
using loremipsum.Gym.Entities;
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
    /// Interaktionslogik für AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Page
    {
        public AddEmployee()
        {
            InitializeComponent();
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = new (Forename.Text, Surename.Text, Street.Text, Int32.Parse(PostcalCode.Text), City.Text, Country.Text, EMail.Text, Int32.Parse(Iban.Text), DateTime.Parse(Birthday.Text), " ");

            GymHomepage home = new GymHomepage();
            NavigationService.Navigate(home);
        }
    }
}
