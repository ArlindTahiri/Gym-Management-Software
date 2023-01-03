using GUI.MemberGUIs;
using loremipsum.Gym;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GUI.EmployeeGUIs
{
    /// <summary>
    /// Interaktionslogik für DeleteEmployeeIDCheck.xaml
    /// </summary>
    public partial class DeleteEmployeeIDCheck : Page
    {

        IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];

        public DeleteEmployeeIDCheck()
        {
            InitializeComponent();
        }

        private void IDCheck_KeyDown(object sender, KeyEventArgs e)
        {
            string content = IDCheck.Text;
            if (e.Key == Key.Enter)
            {
                if (query.SearchEmployee(content) != null)
                {
                    EmployeeCache.cacheID = Int32.Parse(content);
                    DeleteEmployee DeleteEmployee = new DeleteEmployee();
                    NavigationService.Navigate(DeleteEmployee);
                }
            }
        }
    }
}
