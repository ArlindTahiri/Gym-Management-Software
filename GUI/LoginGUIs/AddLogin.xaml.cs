using log4net;
using loremipsum.Gym;
using loremipsum.Gym.Entities;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
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

namespace GUI.LoginGUIs
{
    /// <summary>
    /// Interaktionslogik für AddLogin.xaml
    /// </summary>
    public partial class AddLogin : Page
    {
        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        private readonly IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];
        private string Firstlogin;

        public AddLogin()
        {
            InitializeComponent();
        }

        public AddLogin(string firstlogin)//auto set value of rank=1;
        {
            Firstlogin = firstlogin;
            InitializeComponent();
            Rank.Text = 1.ToString();
            Rank.IsReadOnly= true;
            WarningText.Text = "Der erste erstellte Benutzer muss Adminrechte haben.\n Der Rang 1 ist somit vorgegeben.";
        }

        private void addLogin_Click(object sender, RoutedEventArgs e)
        {
            InsertLogIn();
        }

        private void CreateLogin(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                InsertLogIn();
            }
        }

        private void InsertLogIn()
        {
            if (logInName.Text.Length > 4)
            {
                if (logInPassword.Text.Length > 4)
                {
                    LogIn logIn = new LogIn(logInName.Text, logInPassword.Text, Int32.Parse(Rank.Text));
 
                    if (query.GetLogInDetails(logInName.Text) == null)
                    {
                        admin.AddLogIn(logIn);
                        LoginPage loginhome = new LoginPage();
                        NavigationService.Navigate(loginhome);
                    }
                    else
                    { 
                        WarningText.Text = "Der eingegebene Log In Name exisitert bereits.\n Bitte geben Sie einen noch nicht vorhandenen Log In Namen ein.";
                    }
                   
                } else
                {
                    WarningText.Text = "Das eingegebene passwort ist zu kurz.";
                }
            } else
            {
                WarningText.Text = "Der eingegebene Name ist zu kurz";
            }
        }

        private void Rank_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextValidation.CheckIsNumeric(e);
        }
    }
}
