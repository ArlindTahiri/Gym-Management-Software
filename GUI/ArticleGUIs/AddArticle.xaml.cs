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

namespace GUI.ArticleGUIs
{
    /// <summary>
    /// Interaktionslogik für AddArticle.xaml
    /// </summary>
    public partial class AddArticle : Page
    {

        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        public AddArticle()
        {
            InitializeComponent();
        }

        private void AddArticle1_Click(object sender, RoutedEventArgs e)
        {
            Article article = new Article(Name.Text, Int32.Parse(Price.Text), Int32.Parse(TargetStock.Text), Int32.Parse(ActualStock.Text));
            admin.AddArticle(article);

            GymHomepage home = new GymHomepage();
            NavigationService.Navigate(home);
        }
    }
}
