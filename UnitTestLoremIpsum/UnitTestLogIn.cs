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
            Admin.DeleteLogIns();
            l1 = new LogIn("admin_test", "Passwort+1!", 1);
            l2 = new LogIn("admin_test", "Passwort+1!", 1);
            l3 = new LogIn("mitarbeiter_test", "Passwort+1!", 2);
        }
        [TestMethod]
        public void TestLogIn()
        {

            //Add LogIn l1
            Admin.AddLogIn(l1);

            //Test if l1 is in the database
            Assert.IsTrue(Query.GetLogInDetails(l1.LogInName).CompareTo(l1) == 0);

            //add an login l2 which is identical with l1
            Admin.AddLogIn(l2);
            LogIn newLogIn = Query.GetLogInDetails(l2.LogInName);
            Assert.IsNull(newLogIn);

            //check if there are LogIns in IList
            IList<LogIn> logIns = Admin.ListLogIns();
            Assert.IsTrue(logIns.Count > 0);

            //add and update login l3
            Admin.UpdateLogIn(l3.LogInName, "mitarbeiter_test", "passwort+2!", 2);
            
            //check if login l3 updated in database
            newLogIn = Query.GetLogInDetails(l3.LogInName);
            Assert.IsNull(newLogIn);
            Assert.IsTrue(newLogIn.LogInName.Equals("mitarbeiter2_test") && newLogIn.LogInPassword.Equals("passwort+2!") && newLogIn.Rank == 2);

            //update login l3, but the newLogInName is the same
            Admin.UpdateLogIn(l3.LogInName, l3.LogInName, "passwort+!3", 2);
            newLogIn = Query.GetLogInDetails(l3.LogInName);
            Assert.IsTrue(newLogIn.LogInName.Equals("mitarbeiter2_test") && newLogIn.LogInPassword.Equals("passwort+3!") && newLogIn.Rank == 2);

            //change an login which has an rank 1
            Admin.UpdateLogIn(l1.LogInName, "admin2_test", "passwort+2!", 1);
            newLogIn = Query.GetLogInDetails(l1.LogInName);
            Assert.IsTrue(newLogIn.LogInName.Equals("admin2_test") && newLogIn.LogInPassword.Equals("passwort+2!") && newLogIn.Rank == 1);

            //change the only login which has an rank 1 to rank != 1
            Admin.UpdateLogIn(l1.LogInName, "admin3_test", "passwort+3!", 2);
            newLogIn = Query.GetLogInDetails(l1.LogInName);
            Assert.IsFalse(newLogIn.LogInName.Equals("admin3_test") && newLogIn.LogInPassword.Equals("passwort+3!") && newLogIn.Rank == 2);


        }
    }
}
