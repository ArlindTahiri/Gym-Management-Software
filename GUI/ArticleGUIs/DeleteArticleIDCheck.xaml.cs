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
    /// Interaktionslogik für DeleteArticleIDCheck.xaml
    /// </summary>
    public partial class DeleteArticleIDCheck : Page
    {
        private IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];
        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        public DeleteArticleIDCheck()
        {
            InitializeComponent();
        }

        private void IDCheck_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string content = IDCheck.Text;

                if (query.GetArticleDetails(Int32.Parse(content))!=null)
                {
                   DeleteArticle deleteArticle = new DeleteArticle(Int32.Parse(content));
                    NavigationService.Navigate(deleteArticle);
                }
            }
        }
    }
}
