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
using System.Security.Cryptography.X509Certificates;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GUI.EmployeeGUIs
{
    /// <summary>
    /// Interaktionslogik für AddAndEditEmployee.xaml
    /// </summary>
    public partial class AddAndEditEmployee : Page
    {
        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];
        private Employee employee;
        private int ID;
       
        public AddAndEditEmployee() 
        { 
            InitializeComponent();
        }
        public AddAndEditEmployee(int ID)
        {
            InitializeComponent();
            this.ID = ID;
            this.employee = query.GetEmployeeDetails(ID);
            Name.Text = employee.Forename;
            Surname.Text = employee.Surname;
            Adress.Text = employee.Street;
            PostalCode.Text = employee.PostcalCode.ToString();
            City.Text = employee.City;
            Country.Text = employee.Country;
            EMail.Text = employee.EMail;
            Iban.Text = employee.Iban;
            Birthday.Text = employee.Birthday.ToString("dd.MM.yyyy");

            
        }

       
        private void PostalCode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextValidation.CheckIsNumeric(e);
        }

        private void CreateEmployee(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if (ID == 0)
                {
                    if (!Name.Text.IsNullOrEmpty() && !Surname.Text.IsNullOrEmpty() && !Adress.Text.IsNullOrEmpty() && !PostalCode.Text.IsNullOrEmpty()
                    && !City.Text.IsNullOrEmpty() && !Country.Text.IsNullOrEmpty() && !EMail.Text.IsNullOrEmpty() && !Iban.Text.IsNullOrEmpty() && !Birthday.Text.IsNullOrEmpty())
                    {
                        if (TextValidation.CheckIsPostalCode(PostalCode.Text))
                        {
                            if (TextValidation.CheckIsMail(EMail.Text))
                            {
                                if (TextValidation.CheckIsIban(Iban.Text))
                                {
                                    if (TextValidation.CheckIsDate(Birthday.Text))
                                    {
                                        Employee employee = new(Name.Text, Surname.Text, Adress.Text, Int32.Parse(PostalCode.Text), City.Text, Country.Text, EMail.Text, Iban.Text, DateTime.Parse(Birthday.Text));

                                        admin.AddEmployee(employee);
                                        GymHomepage home = new GymHomepage();
                                        NavigationService.Navigate(home);
                                        log.Info("Added employee: " + employee.ToString() + "... and returned to homepage");
                                    }
                                    else
                                    {
                                        WarningLabel.Content = "Bitte geben Sie einen gültigen Geburtstag ein";
                                    }
                                }
                                else
                                {
                                    WarningLabel.Content = "Bitte geben Sie eine gültige IBan ein";
                                }
                            }
                            else
                            {
                                WarningLabel.Content = "Bitte geben Sie eine gültige E-Mail Adresse ein.";
                            }
                        }
                        else
                        {
                            WarningLabel.Content = "Bitte geben Sie eine gültige Postleitzahl ein";
                        }
                    }
                    else
                    {
                        WarningLabel.Content = "Bitte geben Sie für alle Daten etwas ein!";
                    }
                } else
                {
                    if (!Name.Text.IsNullOrEmpty() && !Surname.Text.IsNullOrEmpty() && !Adress.Text.IsNullOrEmpty() && !PostalCode.Text.IsNullOrEmpty()
              && !City.Text.IsNullOrEmpty() && !Country.Text.IsNullOrEmpty() && !EMail.Text.IsNullOrEmpty() && !Iban.Text.IsNullOrEmpty() && !Birthday.Text.IsNullOrEmpty())
                    {
                        if (TextValidation.CheckIsPostalCode(PostalCode.Text))
                        {
                            if (TextValidation.CheckIsMail(EMail.Text))
                            {
                                if (TextValidation.CheckIsIban(Iban.Text))
                                {
                                    if (TextValidation.CheckIsDate(Birthday.Text))
                                    {
                                       
                                        admin.UpdateEmployee(ID, Name.Text, Surname.Text, Adress.Text, Int32.Parse(PostalCode.Text), City.Text, Country.Text, EMail.Text, Iban.Text, DateTime.Parse(Birthday.Text));
                                        GymHomepage home = new GymHomepage();
                                        NavigationService.Navigate(home);
                                    }
                                    else
                                    {
                                        WarningLabel.Content = "Bitte geben Sie einen gültigen Geburtstag ein";
                                    }
                                }
                                else
                                {
                                    WarningLabel.Content = "Bitte geben Sie eine gültige IBan ein";
                                }
                            }
                            else
                            {
                                WarningLabel.Content = "Bitte geben Sie eine gültige E-Mail Adresse ein.";
                            }
                        }
                        else
                        {
                            WarningLabel.Content = "Bitte geben Sie eine gültige Postleitzahl ein";
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
    }


