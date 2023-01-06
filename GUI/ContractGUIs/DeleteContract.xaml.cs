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
    /// Interaktionslogik für DeleteContract.xaml
    /// </summary>
    public partial class DeleteContract : Page
    {

        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        private readonly IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private int contractID;
        public DeleteContract(int contractID)
        {
            InitializeComponent();
            this.contractID = contractID;

            ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
            var fileInfo = new FileInfo(@"log4net.config");
            XmlConfigurator.Configure(repository, fileInfo);

            log.Info("Opened DeleteContract Page");
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            log.Info("Deleted the contract: " + query.GetContractDetails(contractID).ToString() + "... and returned to GymHomepage");
            admin.DeleteContract(contractID);

            GymHomepage gymHomepage = new GymHomepage();
            NavigationService.Navigate(gymHomepage);
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            log.Info("Aborted the deleteContract option and returned to GymHomepage");
            GymHomepage gymHomepage = new GymHomepage();
            NavigationService.Navigate(gymHomepage);
        }
    }
}
