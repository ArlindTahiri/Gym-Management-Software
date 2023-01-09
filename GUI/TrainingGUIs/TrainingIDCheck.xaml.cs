using log4net;
using log4net.Config;
using log4net.Repository;
using loremipsum.Gym;
using loremipsum.Gym.Entities;
using Microsoft.IdentityModel.Tokens;
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

namespace GUI.TrainingGUIs
{
    /// <summary>
    /// Interaktionslogik für TrainingIDCheck.xaml
    /// </summary>
    public partial class TrainingIDCheck : Page
    {

        private readonly IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];
        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public TrainingIDCheck()
        {
            InitializeComponent();

            ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
            var fileInfo = new FileInfo(@"log4net.config");
            XmlConfigurator.Configure(repository, fileInfo);

            log.Info("Opened ChangeArticleIDCheck Page");
        }

        private void IDCheck_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (!IDCheck.Text.IsNullOrEmpty())
                {
                    if (query.GetMemberDetails(Int32.Parse(IDCheck.Text)) != null)
                    {
                        int content = Int32.Parse(IDCheck.Text);
                        Member searchMember = query.GetMemberDetails(content);

                        if (!admin.ListTrainingMembersID().Contains(searchMember.MemberID))
                        {
                            admin.InsertTrainingMember(content);
                        }
                        else
                        {
                            admin.DeleteTrainingMember(content);
                        }
                    }
                    else
                    {
                        log.Error("Inserted an invalid articleID. The ID was; " + IDCheck.Text);
                        WarningText.Text = "Die eingebene ID ist ungültig. Bitte geben Sie eine gültige ID ein.";
                    }
                }
                else
                {
                    log.Error("Inserted an invalid articleID. The ID was; " + IDCheck.Text);
                    WarningText.Text = "Die eingebene ID ist ungültig. Bitte geben Sie eine gültige ID ein.";
                }
            }
        }

        private void IDCheck_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextValidation.CheckIsNumeric(e);
        }
    }
}
