using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ArticleChangeOptions()
        {
            InitializeComponent();

            ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
            var fileInfo = new FileInfo(@"log4net.config");
            XmlConfigurator.Configure(repository, fileInfo);

            log.Info("Opened ArticleChangeOptions Page");
        }

        private void AddArticle_Click(object sender, RoutedEventArgs e)
        {
            log.Info("Clicked on the AddArticle button");

            AddArticle addArticle = new AddArticle();
            NavigationService.Navigate(addArticle);

        }

        private void EditArticle_Click(object sender, RoutedEventArgs e)
        {
            log.Info("Clicked on the ChangeArticle button");

            ChangeArticleIDCheck changeArticleIDCheck = new ChangeArticleIDCheck();
            NavigationService.Navigate(changeArticleIDCheck);

        }

        private void DeleteArticle_Click(object sender, RoutedEventArgs e)
        {
            log.Info("Clicked on the DeleteArticle button");

            DeleteArticleIDCheck deleteArticleIDCheck = new DeleteArticleIDCheck();
            NavigationService.Navigate(deleteArticleIDCheck);
        }
    }
}
