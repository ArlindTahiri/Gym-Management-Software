using GUI.ArticleGUIs;
using GUI.ContractGUIs;
using GUI.EmployeeGUIs;
using GUI.MemberGUIs;
using GUI.Order_GUIs;
using log4net;
using loremipsum.Gym;
using loremipsum.Gym.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
                    ArticleInventory.ItemsSource = admin.ListArticles();    
                    ArticleInventory.Visibility = Visibility.Visible;
                    break;

                case "DeleteArticle":
                    QuestionBox.Content = "Bitte geben Sie die ID des Artikels ein, den Sie löschen wollen.";
                    ArticleInventory.ItemsSource = admin.ListArticles();
                    ArticleInventory.Visibility = Visibility.Visible;
                    break;

                case "EditContract":
                    QuestionBox.Content = "Bitte geben Sie die ID des Vertrags ein, den Sie bearbeiten wollen.";
                    ContractInventory.ItemsSource = admin.ListContracts();
                    ContractInventory.Visibility = Visibility.Visible;
                    break;

                case "DeleteContract":
                    QuestionBox.Content = "Bitte geben Sie die ID des Vertrags ein, den Sie löschen wollen.";
                    ContractInventory.ItemsSource = admin.ListContracts();
                    ContractInventory.Visibility = Visibility.Visible;
                    break;

                case "EditEmployee":
                    QuestionBox.Content = "Bitte geben Sie die ID des Mitarbeiters ein, den sie bearbeiten wollen.";
                    EmployeeData.ItemsSource = admin.ListEmployees();
                    EmployeeData.Visibility = Visibility.Visible;
                    break;

                case "DeleteEmployee":
                    QuestionBox.Content = "Bitte geben Sie die ID des Mitarbeiters ein, den Sie löschen wollen.";
                    EmployeeData.ItemsSource = admin.ListEmployees();
                    EmployeeData.Visibility = Visibility.Visible;
                    break;

                case "EditMember":
                    QuestionBox.Content = "Bitte geben Sie die ID des Mitglieds ein, das sie bearbeiten wollen.";
                    MemberData.ItemsSource = admin.ListMembers();
                    MemberData.Visibility = Visibility.Visible;
                    break;

                case "DeleteMember":
                    QuestionBox.Content = "Bitte geben Sie die ID des Mitglieds ein, das Sie löschen wollen.";
                    MemberData.ItemsSource = admin.ListMembers();
                    MemberData.Visibility = Visibility.Visible;
                    break;

                case "EditOrder":
                    QuestionBox.Content = "Bitte geben Sie die ID der Bestellung ein, die Sie bearbeiten wollen.";
                    OrderData.ItemsSource = admin.ListOrders();
                    OrderData.Visibility = Visibility.Visible;
                    break;

                case "DeleteOrder":
                    QuestionBox.Content = "Bitte geben Sie die ID der Bestellung ein, die Sie löschen wollen.";
                    OrderData.ItemsSource = admin.ListOrders();
                    OrderData.Visibility = Visibility.Visible;
                    break;

                case "Training":
                    QuestionBox.Content = "Bitte geben Sie Ihre Mitglieds ID ein";                   
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

                    if (destination.Equals("DeleteContract") && query.GetContractDetails(ID)!= null)
                    {
                        DeletePage deletePage = new DeletePage("DeleteContract", ID);
                        NavigationService.Navigate(deletePage);
                    }

                    if (destination.Equals("EditEmployee") && query.GetEmployeeDetails(ID)!= null)
                    {
                        AddAndEditEmployee addEmployee = new AddAndEditEmployee(ID);
                        NavigationService.Navigate(addEmployee);
                    }

                    if (destination.Equals("DeleteEmployee") && query.GetEmployeeDetails(ID) != null)
                    {
                        DeletePage deletePage = new DeletePage("DeleteEmployee", ID);
                        NavigationService.Navigate(deletePage);
                    }

                    if (destination.Equals("EditMember") && query.GetMemberDetails(ID) != null)
                    {
                        EditMember editMember = new EditMember(ID);
                        NavigationService.Navigate(editMember);
                    }

                    if (destination.Equals("DeleteMember") && query.GetMemberDetails(ID) != null)
                    {
                        DeletePage deletePage = new DeletePage("DeleteMember", ID);
                        NavigationService.Navigate(deletePage);
                    }

                    if (destination.Equals("EditOrder") && query.GetOrderDetails(ID) != null)
                    {
                        EditOrder editOrder = new EditOrder(ID);
                        NavigationService.Navigate(editOrder);
                    }

                    if (destination.Equals("DeleteOrder") && query.GetOrderDetails(ID)!= null)
                    {
                        DeletePage deletePage = new DeletePage("DeleteOrder", ID);
                        NavigationService.Navigate(deletePage);
                    }

                    if (destination.Equals("Training") && query.GetMemberDetails(ID)!= null)
                    {
                        Member searchMember = query.GetMemberDetails(ID);

                        if (!admin.ListTrainingMembersID().Contains(searchMember.MemberID))
                        {
                            admin.InsertTrainingMember(ID);                           
                        }
                        else
                        {
                            admin.DeleteTrainingMember(ID);                          
                        }
                        GymHomepage home = new GymHomepage();
                        NavigationService.Navigate(home);
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
