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
    /// Interaktionslogik für DeleteArticle.xaml
    /// </summary>
    public partial class DeleteArticle : Page
    {
        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        private int articleID;
        public DeleteArticle(int articleID)
        {
            InitializeComponent();
            this.articleID = articleID;
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            admin.DeleteArticle(articleID);
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
