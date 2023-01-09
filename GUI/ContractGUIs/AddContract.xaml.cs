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

namespace GUI.ContractGUIs
{
    /// <summary>
    /// Interaktionslogik für AddContract.xaml
    /// </summary>
    public partial class AddContract : Page
    {

        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public AddContract()
        {
            InitializeComponent();

            ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
            var fileInfo = new FileInfo(@"log4net.config");
            XmlConfigurator.Configure(repository, fileInfo);

            log.Info("Opened AddContract page");
        }

        private void AddContractButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ContractType.Text.IsNullOrEmpty() && !Price.Text.IsNullOrEmpty())
            {
                Contract newContract = new Contract(ContractType.Text, Int32.Parse(Price.Text));
                admin.AddContract(newContract);

                log.Info("Created the new contract: " + newContract.ToString() + "... and returned to GymHomepage");

                GymHomepage gymHomepage = new GymHomepage();
                NavigationService.Navigate(gymHomepage);
            }
            else
            {
                WarningLabel.Content = "Bitte geben Sie für alle Daten etwas ein!";
            }
        }

        private void Price_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextValidation.CheckIsNumeric(e);
        }

       
    }
}
