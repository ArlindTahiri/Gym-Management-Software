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
    /// Interaktionslogik für DeleteMemberIDCheck.xaml
    /// </summary>
    public partial class DeleteMemberIDCheck : Page
    {

      
        IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];
        public DeleteMemberIDCheck()
        {
            InitializeComponent();
        }

        private void IDCheck_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            string content = IDCheck.Text;
           //braucht noch ne aushnahmenbehandlung
            if (e.Key == Key.Enter)
            {
                if (query.SearchMember(content) != null)
                {
                    MemberCache.cacheID = Int32.Parse(content);
                    DeleteMember deleteMember = new DeleteMember();
                    NavigationService.Navigate(deleteMember);
                }
            }
        }

       
    }
}
