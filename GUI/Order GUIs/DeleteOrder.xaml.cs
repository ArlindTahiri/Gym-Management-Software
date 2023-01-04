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
    /// Interaktionslogik für DeleteOrder.xaml
    /// </summary>
    public partial class DeleteOrder : Page
    {

        IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        private int orderID;
        public DeleteOrder(int orderID)
        {
            InitializeComponent();
            this.orderID = orderID;
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            admin.DeleteOrder(orderID);

            GymHomepage gymHomepage = new GymHomepage();
            NavigationService.Navigate(gymHomepage);
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            GymHomepage gymHomepage = new GymHomepage();
            NavigationService.Navigate(gymHomepage);
        }
    }
}
