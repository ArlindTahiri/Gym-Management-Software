using Microsoft.Data.SqlClient;
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

namespace GUI.MemberGUIs
{
    /// <summary>
    /// Interaktionslogik für MemberPage.xaml
    /// </summary>
    public partial class MemberPage : Page
    {
        public MemberPage()
        {
            InitializeComponent();

        }

        private void EditMember_Click(object sender, RoutedEventArgs e)
        {
           
            MemberChangeOptions changeOptions = new MemberChangeOptions();
            NavigationService.Navigate(changeOptions);


        }
    }
}
