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
using System.Windows.Navigation;
using System.Windows.Shapes;
using GUI.ArticleGUIs;

namespace GUI.EmployeeGUIs
{
    /// <summary>
    /// Interaktionslogik für Page1.xaml
    /// </summary>
    public partial class EmployeePage : Page
    {
        public EmployeePage()
        {
            InitializeComponent();
        }

        private void EditEmployee_Click(object sender, RoutedEventArgs e)
        {

            EmployeeChangeOptions changeOptions = new EmployeeChangeOptions();
            NavigationService.Navigate(changeOptions);

        }
    }
}
