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
    /// Interaktionslogik für ContractIDCheck.xaml
    /// </summary>
    public partial class ContractIDCheck : Page
    {

        IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ContractIDCheck()
        {
            InitializeComponent();

            ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
            var fileInfo = new FileInfo(@"log4net.config");
            XmlConfigurator.Configure(repository, fileInfo);

            log.Info("Opened ContractIDCheck page");
        }

        private void IDCheck_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
               if(query.GetContractDetails(Int32.Parse(IDCheck.Text))!=null)
                {
                    log.Info("Inserted a valid contractID. The ID was: "+IDCheck.Text);
                    DeleteContract deleteContract = new DeleteContract(int.Parse(IDCheck.Text));   
                    NavigationService.Navigate(deleteContract);
                } else
                {
                    log.Error("Inserted an invalid contractID. The ID was: " + IDCheck.Text);
                    WarningText.Text = "Die eingegebene ID ist ungültig. Bitte geben Sie eine existierende ID ein.";
                }
            }
        }
    }
}
