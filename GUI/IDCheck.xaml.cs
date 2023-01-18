using GUI.ArticleGUIs;
using GUI.ContractGUIs;
using GUI.EmployeeGUIs;
using GUI.LoginGUIs;
using GUI.MemberGUIs;
using GUI.Order_GUIs;
using log4net;
using loremipsum;
using loremipsum.Gym;
using loremipsum.Gym.Entities;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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
        private readonly IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];
        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        ILog log = GymLogger.GetLog();
        private int ID;
        private string login;
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
                    TrainingMembersData.ItemsSource = admin.ListTrainingMembers();
                    TrainingMembersData.Visibility = Visibility.Visible;
                    break;

                case "EditLogin":
                    QuestionBox.Content = "Bitte geben Sie den login ein, den Sie bearbeiten wollen.";
                    LoginData.ItemsSource = admin.ListLogIns();
                    LoginData.Visibility = Visibility.Visible;
                    break;

                case "DeleteLogin":
                    QuestionBox.Content = "Bitte geben Sie den login ein, den Sie löschen wollen.";
                    LoginData.ItemsSource = admin.ListLogIns();
                    LoginData.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void IDCheck1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (!IDCheckBox.Text.IsNullOrEmpty())
                {
                    if (!(destination.Equals("EditLogin") || destination.Equals("DeleteLogin")))
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
                            AddEditContract editContract = new AddEditContract(ID);
                            NavigationService.Navigate(editContract);
                        }

                        if (destination.Equals("DeleteContract") && query.GetContractDetails(ID) != null)
                        {
                            DeletePage deletePage = new DeletePage("DeleteContract", ID);
                            NavigationService.Navigate(deletePage);
                        }

                        if (destination.Equals("EditEmployee") && query.GetEmployeeDetails(ID) != null)
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

                        if (destination.Equals("DeleteOrder") && query.GetOrderDetails(ID) != null)
                        {
                            DeletePage deletePage = new DeletePage("DeleteOrder", ID);
                            NavigationService.Navigate(deletePage);
                        }

                        if (destination.Equals("Training") && query.GetMemberDetails(ID) != null)
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
                    }
                }
                else
                {
                    WarningBox.Content = "Bitte geben Sie eine ID ein.";
                    log.Error("Inserted no ID");
                }
                if (!IDCheckBox.Text.IsNullOrEmpty())
                {
                    if (destination.Equals("DeleteLogin") || destination.Equals("EditLogin"))
                    {

                        this.login = IDCheckBox.Text;

                        if (destination.Equals("EditLogin") && query.GetLogInDetails(login) != null)
                        {
                            AddEditLogin loginPage = new AddEditLogin(login);
                            NavigationService.Navigate(loginPage);
                        }

                        if (destination.Equals("DeleteLogin") && query.GetLogInDetails(login) != null)
                        {
                            if (query.GetLogInDetails(login).Rank != 1)
                            {
                                DeletePage deletePage = new DeletePage("DeleteLogin", login);
                                NavigationService.Navigate(deletePage);
                            }
                            else
                            {
                                WarningBox.Content = "Sie können den amin nicht löschen!";
                                log.Error("Tried deleting the admin account");
                            }
                        }
                        else
                        {
                            WarningBox.Content = "Der eingegebene login existiert nicht. Bitte geben Sie einen gültigen login ein.";
                            log.Error("Inserted an invalid login name.");
                        }
                    }
                } else {
                    WarningBox.Content = "Bitte geben Sie einen login namen ein.";
                    log.Error("Inserted no login name.");
                }
            }
        }
    

        private void IDCheckBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(destination.Equals("EditLogin") || destination.Equals("DeleteLogin")))
            {
                TextValidation.CheckIsNumeric(e);
            }
        }

               
    }
}
