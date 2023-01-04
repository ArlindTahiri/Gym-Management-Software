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

namespace GUI.Order_GUIs
{
    /// <summary>
    /// Interaktionslogik für OrderOptions.xaml
    /// </summary>
    public partial class OrderOptions : Page
    {
        public OrderOptions()
        {
            InitializeComponent();
        }

        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrder addOrder = new AddOrder();
            NavigationService.Navigate(addOrder);
        }

        private void DeleteOrderButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void EditOrderButton_Click(object sender, RoutedEventArgs e)
        {
            EditOrderIDCheck editOrderIDCheck   = new EditOrderIDCheck();
            NavigationService.Navigate(editOrderIDCheck);
        }
    }
}
