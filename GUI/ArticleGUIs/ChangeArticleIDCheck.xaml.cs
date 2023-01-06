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
    /// Interaktionslogik für ChangeArticleIDCheck.xaml
    /// </summary>
    public partial class ChangeArticleIDCheck : Page
    {
        private IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ChangeArticleIDCheck()
        {
            InitializeComponent();

            ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
            var fileInfo = new FileInfo(@"log4net.config");
            XmlConfigurator.Configure(repository, fileInfo);

            log.Info("Opened ChangeArticleIDCheck Page");
        }

        private void IDCheck_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int articleID = Int32.Parse(IDCheck.Text);

                if (query.GetArticleDetails(articleID)!=null ) 
                {
                    log.Info("Inserted a valid articleID. The ID was: " + articleID);

                    ChangeArticle changeArticle = new ChangeArticle(articleID);
                    NavigationService.Navigate(changeArticle);
                } else
                {
                    log.Error("Inserted an invalid articleID. The ID was; " + articleID);
                    WarningText.Text = "Die eingegebene ID ist ungültig. Bitte geben Sie eine existierende ID ein.";
                }
            }
        }
    }
}
