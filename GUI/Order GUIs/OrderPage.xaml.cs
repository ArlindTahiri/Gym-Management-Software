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

        private void EditOrders_Click(object sender, RoutedEventArgs e)
        {
            OrderOptions orderOptions = new OrderOptions();
            NavigationService.Navigate(orderOptions);
        }

        private void OrderData_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void DeleteMembersButton_Click(object sender, RoutedEventArgs e)
        {
            DeletePage deletePage = new DeletePage("Order");
            NavigationService.Navigate(deletePage);
        }
    }
}
