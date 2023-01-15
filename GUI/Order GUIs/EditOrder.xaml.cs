using loremipsum.Gym;
using loremipsum.Gym.Entities;
using Microsoft.IdentityModel.Tokens;
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
            AmountBox.Text = order.Amount.ToString();
            this.orderID = orderID;
            
        }

        private void EditOrderButton_Click(object sender, RoutedEventArgs e)
        {
            Member m = (Member)MemberCB.SelectedItem;
            Article a = (Article)ArticleCB.SelectedItem;
            int difference = order.Amount - Int32.Parse(AmountBox.Text);
            if (!AmountBox.Text.IsNullOrEmpty())
            {
                admin.UpdateOrder(orderID, m.MemberID, a.ArticleID, Int32.Parse(AmountBox.Text));
                
                GymHomepage gymHomepage = new GymHomepage();
                NavigationService.Navigate(gymHomepage);
            }
            else
            {
                WarningText.Text = "Bitte geben Sie für alle Daten etwas ein";
            }
        }

        private void ChangeOrder(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Member m = (Member)MemberCB.SelectedItem;
                Article a = (Article)ArticleCB.SelectedItem;
                int difference = order.Amount - Int32.Parse(AmountBox.Text);
                if (!AmountBox.Text.IsNullOrEmpty())
                {
                    admin.UpdateOrder(orderID, m.MemberID, a.ArticleID, Int32.Parse(AmountBox.Text));

                    GymHomepage gymHomepage = new GymHomepage();
                    NavigationService.Navigate(gymHomepage);
                }
                else
                {
                    WarningText.Text = "Bitte geben Sie für alle Daten etwas ein";
                }
            }
        }

        private void AmountBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextValidation.CheckIsNumeric(e);
        }

        private void MemberCB_Loaded(object sender, RoutedEventArgs e)
        {
            MemberCB.Items.Clear();
            MemberCB.ItemsSource = admin.ListMembers();
            MemberCB.Text = order.MemberID.ToString();
        }

        private void ArticleCB_Loaded(object sender, RoutedEventArgs e)
        {
            ArticleCB.Items.Clear();
            ArticleCB.ItemsSource = admin.ListArticles();
            ArticleCB.Text = order.ArticleID.ToString();
        }
    }
}
