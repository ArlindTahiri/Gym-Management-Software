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

namespace GUI.Order_GUIs
{
    /// <summary>
    /// Interaktionslogik für EditOrder.xaml
    /// </summary>
    public partial class EditOrder : Page
    {
        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        private readonly IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];
        private readonly IGymPersistence persistence = (IGymPersistence)Application.Current.Properties["IGymPersistence"];
        private int orderID;
        private Order order;

        public EditOrder(int orderID)
        {
            InitializeComponent();
            order = query.GetOrderDetails(orderID);
            MemberIDBox.Text = order.MemberID.ToString();
            ArticleIDBox.Text = order.ArticleID.ToString();
            AmountBox.Text = order.Amount.ToString();
            this.orderID = orderID;
            
        }

        private void EditOrderButton_Click(object sender, RoutedEventArgs e)
        {
            admin.UpdateOrder(orderID, Int32.Parse(MemberIDBox.Text), Int32.Parse(ArticleIDBox.Text), Int32.Parse(AmountBox.Text));
            GymHomepage gymHomepage = new GymHomepage();
            NavigationService.Navigate(gymHomepage);
        }

        private void ChangeOrder(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                admin.UpdateOrder(orderID, Int32.Parse(MemberIDBox.Text), Int32.Parse(ArticleIDBox.Text), Int32.Parse(AmountBox.Text));
                GymHomepage gymHomepage = new GymHomepage();
                NavigationService.Navigate(gymHomepage);
            }
        }
    }
}
