using log4net;
using log4net.Config;
using log4net.Repository;
using loremipsum.Gym;
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
    /// Interaktionslogik für DeleteArticle.xaml
    /// </summary>
    public partial class DeleteArticle : Page
    {
        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        private readonly IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];
        private int articleID;
        private static readonly ILog log = LogManager.GetLogger(typeof(GymHomepage));
        public DeleteArticle(int articleID)
        {
            InitializeComponent();
            this.articleID = articleID;

            ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
            var fileInfo = new FileInfo(@"log4net.config");
            XmlConfigurator.Configure(repository, fileInfo);

            log.Info("Opened DeleteArticle Page");
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            log.Info("Deleted the article: "+query.GetArticleDetails(articleID).ToString()+"... and returned to GymHomepage");
            admin.DeleteArticle(articleID);
            GymHomepage gymHomepage = new GymHomepage();
            NavigationService.Navigate(gymHomepage);
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            log.Info("Aborted the delete option and returned back to GymHomepage");
            GymHomepage gymHomepage = new GymHomepage();
            NavigationService.Navigate(gymHomepage);
        }
    }
}
