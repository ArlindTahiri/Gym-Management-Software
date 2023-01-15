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

namespace GUI.Order_GUIs
{
    /// <summary>
    /// Interaktionslogik für OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {

        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        public OrderPage()
        {
            InitializeComponent();
        }

        private void OrderData_Loaded(object sender, RoutedEventArgs e)
        {
            OrderData.DataContext = admin.ListOrders();
            OrderData.ItemsSource = admin.ListOrders();
        }

        private void DeleteMembersButton_Click(object sender, RoutedEventArgs e)
        {
            DeletePage deletePage = new DeletePage("Order");
            NavigationService.Navigate(deletePage);
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            AddOrder addOrder = new AddOrder();
            NavigationService.Navigate(addOrder);
        }

        private void EditOrder_Click(object sender, RoutedEventArgs e)
        {
            IDCheck iDCheck = new IDCheck("EditOrder");
            NavigationService.Navigate(iDCheck);
        }

        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            IDCheck iDCheck = new IDCheck("DeleteOrder");
            NavigationService.Navigate(iDCheck);
        }
    }
}
