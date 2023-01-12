using loremipsum.Gym;
using loremipsum.Gym.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
            Member m = (Member)MemberCB.SelectedItem;
            Article a = (Article)ArticleCB.SelectedItem;
            if (!MemberCB.Text.IsNullOrEmpty() && !ArticleCB.Text.IsNullOrEmpty() && !AmountBox.Text.IsNullOrEmpty())
            {
                if(Int32.Parse(AmountBox.Text) <= a.ActualStock)
                {
                    admin.AddOrder(m.MemberID, a.ArticleID, Int32.Parse(AmountBox.Text));

                    GymHomepage gymHomepage = new GymHomepage();
                    NavigationService.Navigate(gymHomepage);
                }
                else
                {
                    WarningText.Text = "So viele Articel haben wir nicht vorrätig";
                }
            }
            else
            {
                WarningText.Text = "Bitte geben Sie für alle Daten etwas ein!";
            }
        }

        private void ArticleCB_Loaded(object sender, RoutedEventArgs e)
        {

            ArticleCB.Items.Clear();
            ArticleCB.ItemsSource = admin.ListArticles();
        }

        private void MemberCB_Loaded(object sender, RoutedEventArgs e)
        {

            MemberCB.Items.Clear();
            MemberCB.ItemsSource = admin.ListMembers();
        }

        private void AmountBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Member m = (Member)MemberCB.SelectedItem;
                Article a = (Article)ArticleCB.SelectedItem;
                if (!MemberCB.Text.IsNullOrEmpty() && !ArticleCB.Text.IsNullOrEmpty() && !AmountBox.Text.IsNullOrEmpty())
                {
                    if (Int32.Parse(AmountBox.Text) <= a.ActualStock)
                    {
                        admin.AddOrder(m.MemberID, a.ArticleID, Int32.Parse(AmountBox.Text));

                        GymHomepage gymHomepage = new GymHomepage();
                        NavigationService.Navigate(gymHomepage);
                    }
                    else
                    {
                        WarningText.Text = "So viele Articel haben wir nicht vorrätig";
                    }
                }
                else
                {
                    WarningText.Text = "Bitte geben Sie für alle Daten etwas ein!";
                }
            }
        }

        private void AmountBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextValidation.CheckIsNumeric(e);
        }
    }
}
