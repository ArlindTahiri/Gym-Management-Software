using GUI.EmployeeGUIs;
using log4net;
using log4net.Config;
using log4net.Repository;
using loremipsum.Gym;
using loremipsum.Gym.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Printing;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GUI.EmployeeGUIs
{
    /// <summary>
    /// Interaktionslogik für EditEmployee.xaml
    /// </summary>
    public partial class EditEmployee : Page
    {
        private readonly IProductAdmin admin = Application.Current.Properties["IProductAdmin"] as IProductAdmin;
        private readonly IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];
        private int employeeID;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public EditEmployee(int employeeID)
        {
            InitializeComponent();
            this.employeeID = employeeID;


            ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
            var fileInfo = new FileInfo(@"log4net.config");
            XmlConfigurator.Configure(repository, fileInfo);

            log.Info("Opened EditEmployeeIDCheck Page");
        }

        private void EditEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ForeName.Text.IsNullOrEmpty() && !Surname.Text.IsNullOrEmpty() && !Adress.Text.IsNullOrEmpty() && !PostalCode.Text.IsNullOrEmpty()
               && !City.Text.IsNullOrEmpty() && !Country.Text.IsNullOrEmpty() && !Email.Text.IsNullOrEmpty() && !Iban.Text.IsNullOrEmpty() && !Birthday.Text.IsNullOrEmpty())
            {
                log.Info("Updated the old employee: " + query.GetEmployeeDetails(employeeID).ToString() +
               " to: " + employeeID + " " + ForeName.Text + " " + Surname.Text +" "+Adress.Text+" "+PostalCode.Text+" "+City.Text+
               " "+ Country.Text+" "+ Email.Text+" "+Iban.Text+" "+Birthday.Text+"... and returned to GymHomepage");

                admin.UpdateEmployee(employeeID, ForeName.Text, Surname.Text, Adress.Text, Int32.Parse(PostalCode.Text), City.Text, Country.Text, Email.Text, Iban.Text, DateTime.Parse(Birthday.Text));
                GymHomepage home = new GymHomepage();
                NavigationService.Navigate(home);
            } else
            {
                WarningLabel.Content = "Bitte geben Sie für alle Daten etwas ein!";
            }
        }

        private void CheckIsNumeric(TextCompositionEventArgs e)
        {
            int result;

            if (!(int.TryParse(e.Text, out result) || e.Text == "."))
            {
                e.Handled = true;
            }
        }

        private void PostalCode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumeric(e);
        }
    }
}
