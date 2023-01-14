using GUI.ArticleGUIs;
using GUI.ContractGUIs;
using log4net;
using loremipsum.Gym;
using loremipsum.Gym.Entities;
using Microsoft.IdentityModel.Tokens;
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

namespace GUI
{
    /// <summary>
    /// Interaktionslogik für IDCheck.xaml
    /// </summary>
    public partial class IDCheck : Page
    {

        private string destination;
        private IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];
        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        private int ID;
        public IDCheck(string destination)
        {
            InitializeComponent();
            this.destination = destination;

            switch (destination)
            {
                case "EditArticle": QuestionBox.Content = "Bitte geben Sie die ID des Artikels ein, den Sie ändern wollen.";
                    GymData.ItemsSource = admin.ListArticles();
                    break;

                case "DeleteArticle":
                    QuestionBox.Content = "Bitte geben Sie die ID des Artikels ein, den Sie löschen wollen.";
                    GymData.ItemsSource = admin.ListArticles();
                    break;

                case "EditContract":
                    QuestionBox.Content = "Bitte geben Sie die ID des Vertrags ein, den Sie bearbeiten wollen.";
                    GymData.ItemsSource = admin.ListContracts();
                    break;
            }
        }

        private void IDCheck1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (!IDCheckBox.Text.IsNullOrEmpty())
                {
                    ID = Int32.Parse(IDCheckBox.Text);
                    if (destination.Equals("EditArticle") && query.GetArticleDetails(ID) != null)
                    {
                        ChangeArticle changeArticle = new ChangeArticle(ID);
                        NavigationService.Navigate(changeArticle);
                    }

                    if (destination.Equals("DeleteArticle") && query.GetArticleDetails(ID) != null)
                    {
                        DeletePage deletePage = new DeletePage("DeleteArticle", ID);
                        NavigationService.Navigate(deletePage);
                    }
                    
                    if (destination.Equals("EditContract") && query.GetContractDetails(ID) != null)
                    {
                        EditContract editContract = new EditContract(ID);
                        NavigationService.Navigate(editContract);
                    }

                    else
                    {

                        WarningBox.Content = "Die eingegebene ID ist ungültig. Bitte geben Sie eine existierende ID ein.";
                    }
                }
                else
                {
                    WarningBox.Content = "Die eingegebene ID ist ungültig. Bitte geben Sie eine gültige ID ein.";
                }
            }
        }

        private void IDCheckBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextValidation.CheckIsNumeric(e);
        }
    }
}
