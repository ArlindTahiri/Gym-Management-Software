using loremipsum.Gym;
using loremipsum.Gym.Entities;
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

namespace GUI
{
    /// <summary>
    /// Interaktionslogik für MemberChangeOptions.xaml
    /// </summary>
    public partial class MemberChangeOptions : Page
    {

       
       
        public MemberChangeOptions()
        {
            InitializeComponent();
        }

        private void DeleteMember_Click(object sender, RoutedEventArgs e)
        {
           DeleteMemberIDCheck deleteMemberIDCheck = new DeleteMemberIDCheck();
            NavigationService.Navigate(deleteMemberIDCheck);
            
        }

        private void AddMember_Click(object sender, RoutedEventArgs e)
        {
            AddMember addMember = new AddMember();
            NavigationService.Navigate(addMember);
        }

        private void EditMember_Click(object sender, RoutedEventArgs e)
        {
            EditMemberIDCheck edit = new EditMemberIDCheck();
            NavigationService.Navigate(edit);
        }
    }
}
