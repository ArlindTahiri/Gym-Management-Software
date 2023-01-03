using GUI.EmployeeGUIs;
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

namespace GUI
{
    /// <summary>
    /// Interaktionslogik für DeleteEmployee.xaml
    /// </summary>
    public partial class DeleteEmployee : Page
    {
        IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];

        public DeleteEmployee()
        {
            InitializeComponent();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            admin.DeleteEmployee(EmployeeCache.cacheID);

            GymHomepage home = new GymHomepage();
            NavigationService.Navigate(home);
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            GymHomepage home = new GymHomepage();
            NavigationService.Navigate(home);
        }
    }
}
