using log4net;
using log4net.Config;
using log4net.Repository;
using loremipsum.Gym;
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
    /// Interaktionslogik für ContractPage.xaml
    /// </summary>
    public partial class ContractPage : Page
    {

        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ContractPage()
        {
          InitializeComponent();

            ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
            var fileInfo = new FileInfo(@"log4net.config");
            XmlConfigurator.Configure(repository, fileInfo);

            log.Info("Opened Contract Page");
        }

        private void DeleteContractButton_Click(object sender, RoutedEventArgs e)
        {
            IDCheck iDCheck = new IDCheck("DeleteContract");
            NavigationService.Navigate(iDCheck);
        }

        private void AddContractButton_Click(object sender, RoutedEventArgs e)
        {
            log.Info("Clicked on the AddContract button");
            AddContract addContract = new AddContract();
            NavigationService.Navigate(addContract);
        }

        private void ContractInventory_Loaded(object sender, RoutedEventArgs e)
        {
            ContractInventory.ItemsSource = admin.ListContracts();
        }

        private void DeleteContractsButton_Click(object sender, RoutedEventArgs e)
        {
            DeletePage deletePage = new DeletePage("Contract");
            NavigationService.Navigate(deletePage);
        }

        private void ChangeContractButton_Click(object sender, RoutedEventArgs e)
        {
            IDCheck iDCheck = new IDCheck("EditContract");
            NavigationService.Navigate(iDCheck);
        }
    }
}
