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

namespace GUI.EmployeeGUIs
{
    /// <summary>
    /// Interaktionslogik für AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Page
    {
        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public AddEmployee()
        {
            InitializeComponent();

            ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
            var fileInfo = new FileInfo(@"log4net.config");
            XmlConfigurator.Configure(repository, fileInfo);

            log.Info("Opened AddEmployee Page");
        }

        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ForeName.Text.IsNullOrEmpty() && !Surname.Text.IsNullOrEmpty() && !Adress.Text.IsNullOrEmpty() && !PostalCode.Text.IsNullOrEmpty()
                && !City.Text.IsNullOrEmpty() && !Country.Text.IsNullOrEmpty() && !Email.Text.IsNullOrEmpty() && !Iban.Text.IsNullOrEmpty() && !Birthday.Text.IsNullOrEmpty())
            {

                Employee employee = new(ForeName.Text, Surname.Text, Adress.Text, Int32.Parse(PostalCode.Text), City.Text, Country.Text, Email.Text, Iban.Text, DateTime.Parse(Birthday.Text));

                admin.AddEmployee(employee);
                GymHomepage home = new GymHomepage();
                NavigationService.Navigate(home);
                log.Info("Added employee: " + employee.ToString() + "... and returned to homepage");
            }
            else
            {
                WarningLabel.Content = "Bitte geben Sie für alle Daten etwas ein!";
            }
        }


        private void PostalCode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextValidation.CheckIsNumeric(e);
        }
    }
}
