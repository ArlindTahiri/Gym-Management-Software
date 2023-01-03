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
    /// Interaktionslogik für ArticleChangeOptions.xaml
    /// </summary>
    public partial class ArticleChangeOptions : Page
    {
        public ArticleChangeOptions()
        {
            InitializeComponent();
        }

        private void AddArticle_Click(object sender, RoutedEventArgs e)
        {
            AddArticle addArticle = new AddArticle();
            NavigationService.Navigate(addArticle); 
        }

        private void EditArticle_Click(object sender, RoutedEventArgs e)
        {
            ChangeArticleIDCheck changeArticleIDCheck = new ChangeArticleIDCheck();
            NavigationService.Navigate(changeArticleIDCheck);
        }

        private void DeleteArticle_Click(object sender, RoutedEventArgs e)
        {
            DeleteArticleIDCheck deleteArticleIDCheck = new DeleteArticleIDCheck();
            NavigationService.Navigate(deleteArticleIDCheck);
        }
    }
}
