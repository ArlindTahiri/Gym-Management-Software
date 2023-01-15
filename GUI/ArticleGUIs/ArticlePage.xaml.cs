using log4net;
using log4net.Config;
using log4net.Repository;
using loremipsum.Gym;
using loremipsum.Gym.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.WebRequestMethods;

namespace GUI.ArticleGUIs
{
    /// <summary>
    /// Interaktionslogik für ArticlePage.xaml
    /// </summary>
    public partial class ArticlePage : Page
    {

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        public ArticlePage()
        {
            InitializeComponent();

            ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
            var fileInfo = new FileInfo(@"log4net.config");
            XmlConfigurator.Configure(repository, fileInfo);

            log.Info("Opened Article Page");
        }

        private void ArticleInventory_Loaded(object sender, RoutedEventArgs e)
        {
            ArticleInventory.ItemsSource = admin.ListArticles();
        }

        private void DeleteArticlesButton_Click(object sender, RoutedEventArgs e)
        {
            DeletePage deletePage = new DeletePage("Article");
            NavigationService.Navigate(deletePage);
        }

        private void AddArticle_Click(object sender, RoutedEventArgs e)
        {
            AddArticle addArticle = new AddArticle();
            NavigationService.Navigate(addArticle);
        }

        private void EditArticle_Click_1(object sender, RoutedEventArgs e)
        {
            IDCheck iDCheck = new IDCheck("EditArticle");
            NavigationService.Navigate(iDCheck);
        }

        private void DeleteArticle_Click(object sender, RoutedEventArgs e)
        {
            IDCheck iDCheck = new IDCheck("DeleteArticle");
            NavigationService.Navigate(iDCheck);
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GymHomepage gymHomepage = new GymHomepage();
            NavigationService.Navigate(gymHomepage);
        }
    }
}
