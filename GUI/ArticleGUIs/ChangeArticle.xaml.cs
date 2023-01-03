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

namespace GUI.ArticleGUIs
{
    /// <summary>
    /// Interaktionslogik für ChangeArticle.xaml
    /// </summary>
    public partial class ChangeArticle : Page
    {

        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        public ChangeArticle()
        {
            InitializeComponent();
        }

        private void ChangeArticle1_Click(object sender, RoutedEventArgs e)
        {
            admin.UpdateArticle(ArticleCache.articleID, Name.Text, Int32.Parse(Price.Text), Int32.Parse(TargetStock.Text), Int32.Parse(ActualStock.Text));

            GymHomepage home = new GymHomepage();
            NavigationService.Navigate(home);
        }
    }
}
