using loremipsum.Gym.Entities;
using loremipsum.Gym;
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
using log4net;
using log4net.Config;
using log4net.Repository;
using System.IO;
using System.Reflection;
using loremipsum;
using GUI.ArticleGUIs;

namespace GUI.LoginGUIs
{
    /// <summary>
    /// Interaction logic for AddEditLogin.xaml
    /// </summary>
    public partial class AddEditLogin : Page
    {
        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        private readonly IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];
        private readonly ILog log = GymLogger.GetLog();
        private string Firstlogin;
        private LogIn logIn;

        public AddEditLogin()
        {
            InitializeComponent();       
        }

        public AddEditLogin(string firstlogin)//auto set value of rank=1;
        {
            Firstlogin = firstlogin;
            InitializeComponent();
            if (firstlogin == "FirstTime")
            {
                Rank.Text = 1.ToString();
                Rank.IsReadOnly = true;
                WarningText.Text = "Der erste erstellte Benutzer muss Adminrechte haben.\nDer Rang 1 ist somit vorgegeben.";
            }
            else
            {
                logIn = query.GetLogInDetails(firstlogin);
                LogInName.Text = logIn.LogInName;
                LogInPassword.Text = logIn.LogInPassword;
                Rank.Text = logIn.Rank.ToString();
                addLogin.Content = "Login bearbeiten";
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LoginPage loginPage = new LoginPage();
            NavigationService.Navigate(loginPage);
        }

        private void addLogin_Click(object sender, RoutedEventArgs e)
        {
            InsertOrUpdateLogIn();
        }

        private void CreateLogin(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                InsertOrUpdateLogIn();
            }
        }

        private void InsertOrUpdateLogIn()
        {
            if (LogInName.Text.Length > 4)
            {
                if (LogInPassword.Text.Length > 4)
                {
                    if (logIn == null)//insert new login
                    {
                        LogIn logInNew = new LogIn(LogInName.Text, LogInPassword.Text, Int32.Parse(Rank.Text));                       
                        if (query.GetLogInDetails(LogInName.Text) == null)
                        {
                            admin.AddLogIn(logInNew);
                            log.Info("Created a new login account: " + logInNew.ToString());
                            if (Firstlogin == "FirstTime")
                            {
                                GymHomepage homepage = new GymHomepage();
                                NavigationService.Navigate(homepage);
                            }
                            else
                            {
                                LoginPage loginhome = new LoginPage();
                                NavigationService.Navigate(loginhome);
                            }
                        }
                        else
                        {
                            WarningText.Text = "Der eingegebene Log In Name exisitert bereits.\n Bitte geben Sie einen noch nicht vorhandenen Log In Namen ein.";
                            log.Error("Inserted a login name that already exists!");
                        }
                    }
                    else// edit or update login
                    {
                        admin.UpdateLogIn(logIn.LogInName, LogInName.Text, LogInPassword.Text, Int32.Parse(Rank.Text));
                        LogIn newLogin = query.GetLogInDetails(logIn.LogInName);
                        log.Info("Updated the old login: "+ logIn.ToString() +" to: "+ newLogin.ToString());
                        LoginPage loginPage = new LoginPage();
                        NavigationService.Navigate(loginPage);
                    }

                }
                else
                {
                    log.Error("Iserted a password that was too short!");
                    WarningText.Text = "Das eingegebene Passwort ist zu kurz.";
                }
            }
            else
            {
                log.Error("Inserted a login name that was too short!");
                WarningText.Text = "Der eingegebene Name ist zu kurz";
            }
        }

        private void Rank_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextValidation.CheckIsNumeric(e);
        }
    }
}
