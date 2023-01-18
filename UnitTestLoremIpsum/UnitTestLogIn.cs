using loremipsum.Gym;
using loremipsum.Gym.Entities;
using loremipsum.Gym.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestLoremIpsum
{
    [TestClass]
    public class UnitTestLogIn
    {
        private IProductAdmin Admin;
        private IProductModule Query;
        private LogIn l1, l2, l3;


        [TestInitialize]
        public void SetUp()
        {
            IGymPersistence persistence = new GymPersistenceEF();
            GymFactory factory = new GymFactory(persistence);
            Admin = factory.GetProductAdmin();
            Query = factory.GetProductModule();
            GenerateTestData();
        }

        public void GenerateTestData()
        {
            l1 = new LogIn("admin_test", "Passwort+1!", 1);
            l2 = new LogIn("mitarbeiter_test", "Passwort+1!", 2);
            l3 = new LogIn("admin1_test", "Passwort+1!", 1);
        }
        [TestMethod]
        public void TestLogIn()
        {

            //Add LogIn l1
            Admin.AddLogIn(l1);

            //Test if l1 is in the database
            Assert.IsTrue(Query.GetLogInDetails(l1.LogInName).CompareTo(l1) == 0);
            Assert.IsNotNull(Query.GetLogInDetails(l1.LogInName));

            //check if there are logIns in the IList
            IList<LogIn> logIns = Admin.ListLogIns();
            Assert.IsTrue(logIns.Count > 0);

            //add and update login l2
            Admin.AddLogIn(l2);
            Admin.UpdateLogIn(l2.LogInName, "mitarbeiter_test", "passwort+2!", 2);

            //check if login l2 updated in database
            LogIn newLogIn = Query.GetLogInDetails(l2.LogInName);
            Assert.IsNotNull(Query.GetLogInDetails(l2.LogInName));
            Assert.IsTrue(newLogIn.LogInName.Equals("mitarbeiter_test") && newLogIn.LogInPassword.Equals("passwort+2!") && newLogIn.Rank == 2);

            //change an login which has an rank 1
            Admin.UpdateLogIn(l1.LogInName, "admin_test", "passwort+2!", 1);
            newLogIn = Query.GetLogInDetails(l1.LogInName);
            Assert.IsTrue(newLogIn.LogInName.Equals("admin_test") && newLogIn.LogInPassword.Equals("passwort+2!") && newLogIn.Rank == 1);

            //add an second login with rank = 1 and try to change one of them to rank 2
            Admin.AddLogIn(l3);
            Admin.UpdateLogIn(l3.LogInName, "admin1_test", "passwort+2!", 2);
            newLogIn = Query.GetLogInDetails(l3.LogInName);
            Assert.IsTrue(newLogIn.LogInName.Equals("admin1_test") && newLogIn.LogInPassword.Equals("passwort+2!") && newLogIn.Rank == 2);

            //delete login l2
            Admin.DeleteLogIn(l2.LogInName);
            Assert.IsNull(Query.GetLogInDetails(l2.LogInName));

            //change rank from l3 to 1 and than delete l3 
            Admin.UpdateLogIn(l3.LogInName, l3.LogInName, l3.LogInPassword, 1);
            Admin.DeleteLogIn(l3.LogInName);
            Assert.IsNull(Query.GetLogInDetails(l3.LogInName));

            //delete l1 (it is only possible because there is still in db a LogIn with rank == 1
            Admin.DeleteLogIn(l1.LogInName);
        }
    }
}