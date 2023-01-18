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

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OrderPage orderPage = new OrderPage();
            NavigationService.Navigate(orderPage);
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
            MemberCB.ItemsSource = admin.ListMembers();

            int i = 0;
            IList<Member> members = admin.ListMembers();
            foreach (Member m in members)
            {
                if (m.MemberID == order.MemberID)
                {
                    MemberCB.SelectedIndex = i;
                }
                else i++;
            }
        }

        private void ArticleCB_Loaded(object sender, RoutedEventArgs e)
        {
            ArticleCB.ItemsSource = admin.ListArticles();
            
            int i = 0;
            IList<Article> articles = admin.ListArticles();
            foreach (Article article in articles)
            {
                if (article.ArticleID == order.ArticleID)
                {
                    ArticleCB.SelectedIndex = i;
                }
                else i++;
            }
        }
    }
}
