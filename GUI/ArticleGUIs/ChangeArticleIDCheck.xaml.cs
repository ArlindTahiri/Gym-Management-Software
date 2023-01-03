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
    /// Interaktionslogik für ChangeArticleIDCheck.xaml
    /// </summary>
    public partial class ChangeArticleIDCheck : Page
    {
        private IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];
        public ChangeArticleIDCheck()
        {
            InitializeComponent();
        }

        private void IDCheck_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (query.SearchArticle(IDCheck.Text) != null)
                {
                    ArticleCache.articleID = Int32.Parse(IDCheck.Text);
                    ChangeArticle changeArticle = new ChangeArticle();
                    NavigationService.Navigate(changeArticle);
                }
            }
        }
    }
}
