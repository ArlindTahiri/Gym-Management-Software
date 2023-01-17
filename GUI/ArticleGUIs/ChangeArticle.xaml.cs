using log4net;
using log4net.Config;
using log4net.Repository;
using loremipsum.Gym;
using loremipsum.Gym.Entities;
using Microsoft.IdentityModel.Tokens;
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
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Article article;
        private int articleID;
       
        public ChangeArticle(int articleID)
        {
            InitializeComponent();
            this.articleID = articleID;
            article = query.GetArticleDetails(articleID);
            Name.Text = article.ArticleName;
            Price.Text = ((double)article.Price / 100).ToString();
            TargetStock.Text = article.TargetStock.ToString();
            ActualStock.Text = article.ActualStock.ToString();

            ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
            var fileInfo = new FileInfo(@"log4net.config");
            XmlConfigurator.Configure(repository, fileInfo);
        }

        private void ChangeArticleButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Name.Text.IsNullOrEmpty() && !Price.Text.IsNullOrEmpty() && !TargetStock.Text.IsNullOrEmpty() && !ActualStock.Text.IsNullOrEmpty())
            {
                if (Price.Text.Contains(".") || Price.Text.Contains(","))
                {
                    string[] string_remove = { ".", "," };
                    string euroPrice = Price.Text;

                    foreach (string c in string_remove)
                    {
                        euroPrice = euroPrice.Replace(c, "");
                    }

                    int centPrice = Int32.Parse(euroPrice);

                    admin.UpdateArticle(articleID, Name.Text, centPrice, Int32.Parse(ActualStock.Text), Int32.Parse(TargetStock.Text));

                    GymHomepage home = new GymHomepage();
                    NavigationService.Navigate(home);
                }
                else
                {                 
                    admin.UpdateArticle(articleID, Name.Text, Int32.Parse(Price.Text), Int32.Parse(ActualStock.Text), Int32.Parse(TargetStock.Text));

                    GymHomepage home = new GymHomepage();
                    NavigationService.Navigate(home);
                }

                if (article.ActualStock > Int32.Parse(ActualStock.Text))
                { 
                    log.Info("The amount of the article: "+article.ToString()+" has been lowered to: "+ActualStock.Text);
                }
            }
            else
            {
                WarningLabel.Content = "Bitte geben Sie für alle Daten etwas ein!";
            }


        }

        private void Price_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            AllowEuroPrice(e);
        }

        private void TargetStock_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextValidation.CheckIsNumeric(e);
        }

        private void ActualStock_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextValidation.CheckIsNumeric(e);
        }

        public static void AllowEuroPrice(TextCompositionEventArgs e)
        {
            int result;

            if (!(int.TryParse(e.Text, out result) || e.Text == "." || e.Text == ","))
            {
                e.Handled = true;
            }
        }

        private void EditArticle(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (!Name.Text.IsNullOrEmpty() && !Price.Text.IsNullOrEmpty() && !TargetStock.Text.IsNullOrEmpty() && !ActualStock.Text.IsNullOrEmpty())
                {
                    if (Price.Text.Contains(".") || Price.Text.Contains(","))
                    {
                        string[] string_remove = { ".", "," };
                        string euroPrice = Price.Text;

                        foreach (string c in string_remove)
                        {
                            euroPrice = euroPrice.Replace(c, "");
                        }

                        int centPrice = Int32.Parse(euroPrice);

                        admin.UpdateArticle(articleID, Name.Text, centPrice, Int32.Parse(ActualStock.Text), Int32.Parse(TargetStock.Text));

                        GymHomepage home = new GymHomepage();
                        NavigationService.Navigate(home);
                    }
                    else
                    {
                        admin.UpdateArticle(articleID, Name.Text, Int32.Parse(Price.Text), Int32.Parse(ActualStock.Text), Int32.Parse(TargetStock.Text));

                        GymHomepage home = new GymHomepage();
                        NavigationService.Navigate(home);
                    }
                }
                else
                {
                    WarningLabel.Content = "Bitte geben Sie für alle Daten etwas ein!";
                }
            }
        }
    }
}
