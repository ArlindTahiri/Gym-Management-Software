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
    /// Interaktionslogik für ArticlePage.xaml
    /// </summary>
    public partial class ArticlePage : Page
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(GymHomepage));
        public ArticlePage()
        {
            InitializeComponent();

            ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
            var fileInfo = new FileInfo(@"log4net.config");
            XmlConfigurator.Configure(repository, fileInfo);

            log.Info("Opened Article Page");
        }

        private void EditArticle_Click(object sender, RoutedEventArgs e)
        {
            ArticleChangeOptions articleChangeOptions = new ArticleChangeOptions();
            NavigationService.Navigate(articleChangeOptions);

            log.Info("Clicked on the EditArticle button");
        }
    }
}
