using GUI.MemberGUIs;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GUI.EmployeeGUIs
{
    /// <summary>
    /// Interaktionslogik für EditEmployeeIDCheck.xaml
    /// </summary>
    public partial class EditEmployeeIDCheck : Page
    {
        private IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public EditEmployeeIDCheck()
        {
            InitializeComponent();

            ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
            var fileInfo = new FileInfo(@"log4net.config");
            XmlConfigurator.Configure(repository, fileInfo);

            log.Info("Opened EditEmployeeIDCheck Page");
        }

        private void IDCheck_KeyDown(object sender, KeyEventArgs e)
        {
            string content = IDCheck.Text;

            if(e.Key == Key.Enter)
            {
                if(query.GetEmployeeDetails(Int32.Parse(content))!=null)
                {
                    
                    EditEmployee editEmployee = new EditEmployee(Int32.Parse(content));
                    NavigationService.Navigate(editEmployee);
                } else
                {
                    log.Error("Inserted an invalid employeeID. The ID was: " + content);
                    WarningText.Text = "Die eingegebene ID ist ungültig. Bitte geben Sie eine existierende ID ein.";
                }
            }
        }

       
    }
}
