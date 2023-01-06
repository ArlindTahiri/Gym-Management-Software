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
    /// Interaktionslogik für ChangeArticle.xaml
    /// </summary>
    public partial class ChangeArticle : Page
    {

        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        private readonly IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];
        private int articleID;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ChangeArticle(int articleID)
        {
            InitializeComponent();
            this.articleID = articleID;

            ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
            var fileInfo = new FileInfo(@"log4net.config");
            XmlConfigurator.Configure(repository, fileInfo);

            log.Info("Opened ChangeArticle Page");
        }

        private void ChangeArticle1_Click(object sender, RoutedEventArgs e)
        {
            log.Info("Updated the old article: " + query.GetArticleDetails(articleID).ToString() +
                " to: " + articleID + " " + Name.Text + " " + Price.Text+"... and returned to GymHomepage");

            admin.UpdateArticle(articleID, Name.Text, Int32.Parse(Price.Text), Int32.Parse(TargetStock.Text), Int32.Parse(ActualStock.Text));

            GymHomepage home = new GymHomepage();
            NavigationService.Navigate(home);

            
        }
    }
}
