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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GUI
{
    /// <summary>
    /// Interaktionslogik für EditMemberIDCheck.xaml
    /// </summary>
    public partial class EditMemberIDCheck : Page
    {
        private IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];
        public EditMemberIDCheck()
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
                    EditMember editMember = new EditMember();
                    NavigationService.Navigate(editMember);
                }
            }
        }
    }
}
