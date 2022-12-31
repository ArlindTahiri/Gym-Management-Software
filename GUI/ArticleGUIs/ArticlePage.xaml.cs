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
    /// Interaktionslogik für ArticlePage.xaml
    /// </summary>
    public partial class ArticlePage : Page
    {
        public ArticlePage()
        {
            InitializeComponent();
        }

        private void EditArticle_Click(object sender, RoutedEventArgs e)
        {
            ArticleChangeOptions articleChangeOptions = new ArticleChangeOptions();
            NavigationService.Navigate(articleChangeOptions);
        }
    }
}
