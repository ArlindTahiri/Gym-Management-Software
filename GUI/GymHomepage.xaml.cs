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
using GUI.ArticleGUIs;
using GUI.EmployeeGUIs;
using GUI.Order_GUIs;
using log4net;
using log4net.Config;
using log4net.Repository;
using loremipsum.Gym;
using loremipsum.Gym.Entities;

namespace GUI
{
    /// <summary>
    /// Interaktionslogik für GymHomepage.xaml
    /// </summary>
    public partial class GymHomepage : Page
    {

        
        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
       
        public GymHomepage()
        {
            InitializeComponent();         
        }

        private void Member_Click(object sender, RoutedEventArgs e)
        {
            GymPasswordPage gymPasswordPage = new GymPasswordPage("Member");
            NavigationService.Navigate(gymPasswordPage);
        }

        private void Contract_Click(object sender, RoutedEventArgs e)
        {         
            GymPasswordPage gymPasswordPage = new GymPasswordPage("Contract");
            NavigationService.Navigate(gymPasswordPage);
        }

        private void EmployeeButton_Click(object sender, RoutedEventArgs e)
        {           
            GymPasswordPage gymPasswordPage = new GymPasswordPage("Employee");
            NavigationService.Navigate(gymPasswordPage);
        }

        private void Inventar_Click(object sender, RoutedEventArgs e)
        {
            GymPasswordPage gymPasswordPage = new GymPasswordPage("Inventar");
            NavigationService.Navigate(gymPasswordPage);
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            GymPasswordPage gymPasswordPage = new GymPasswordPage("Order");
            NavigationService.Navigate(gymPasswordPage);
        }

        private void Logins_Click(object sender, RoutedEventArgs e)
        {
            GymPasswordPage gymPasswordPage = new GymPasswordPage("Login");
            NavigationService.Navigate(gymPasswordPage);

        }

        private void TrainingButton_Click(object sender, RoutedEventArgs e)
        {
            IDCheck iDCheck = new IDCheck("Training");
            NavigationService.Navigate(iDCheck);
        }

        private void DeleteEverythingButton_Click(object sender, RoutedEventArgs e)
        {
            DeletePage deletePage = new DeletePage("Home");
            NavigationService.Navigate(deletePage);
        }

        private void TrainingMembers_Loaded(object sender, RoutedEventArgs e)
        {
            TrainingMembers.ItemsSource = admin.ListTrainingMembers();
            CurrentlyTraining.Content = "Aktuell trainierende Mitglieder: "+ admin.ListTrainingMembers().Count;
        }

        private void Homepage_Loaded(object sender, RoutedEventArgs e)
        {
            int memberCount = admin.ListTrainingMembers().Count;

            if (memberCount <= 2)
            {
                CurrentlyTraining.Foreground = Brushes.White;
            } else if (memberCount <= 3 && memberCount > 2)
            {
                CurrentlyTraining.Foreground = Brushes.White;
            } else
            {
                CurrentlyTraining.Foreground = Brushes.White;
            }
        }
    }
}
