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
    /// Interaktionslogik für AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Page
    {
        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        public AddOrder()
        {
            InitializeComponent();
        }

        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            admin.AddOrder(Int32.Parse(MemberCB.Text), Int32.Parse(ArticleCB.Text), Int32.Parse(AmountBox.Text));
            
            GymHomepage gymHomepage = new GymHomepage();
            NavigationService.Navigate(gymHomepage);
        }

        private void MemberCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IList<Member> members = admin.ListMembers();
            foreach (Member member in members)
            {
                MemberCB.Items.Add(member.MemberID);
            }
        }

        private void ArticleCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IList<Article> articles = admin.ListArticles();
            foreach (Article article in articles)
            {
                ArticleCB.Items.Add(article.ArticleID);
            }
        }
    }
}
