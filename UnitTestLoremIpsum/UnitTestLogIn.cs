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
        private LogIn l1, l2, l3, l4;


        [TestInitialize()]
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
            l1 = new LogIn("peterchef", "Passwort+1!", 1);
            l2 = new LogIn("peterchef", "Passwort+1!", 1);
            l3 = new LogIn("antonmitarbeiter", "Passwort+1!", 2);
            l4 = new LogIn("ninamitarbeiter", "Passwort+1!", 2);
        }

        [TestMethod]
        public void addLogIn()
        {
            //Add LogIn l1
            Admin.AddLogIn(l1);

            //Test if l1 is in the database
            Assert.IsTrue(Query.GetLogInDetails(l1.LogInName).CompareTo(l1) == 0);
        }

        [TestMethod]
        public void ListLogIn()
        {
            //add an LogIn and than check if there are LogIns
            Admin.AddLogIn(l1);
            IList<LogIn> logIns = Admin.ListLogIns();
            Assert.IsTrue(logIns.Count > 0);

            //now delete them and check again
            Admin.DeleteLogIns();
            logIns = Admin.ListLogIns();
            Assert.IsTrue(logIns.Count == 0);
        }

        [TestMethod]
        public void UpdateLogIn()   //ToDo
        {
            Admin.AddLogIn(l3);

            //Test if you can update LogIn Properties
            Admin.UpdateLogIn(l3.LogInName, "antoniomitarbeiter", "Passwort+2!",3);
            LogIn newLogIn = Query.GetLogInDetails(l3.LogInName);
            Assert.IsTrue(newLogIn.LogInName.Equals("antoniomitarbeiter") && newLogIn.LogInPassword.Equals("Passwort+1!lol") && newLogIn.Rank == 1);
        }

        [TestMethod]
        public void DeleteLogIn()   //ToDo
        {
            //Test if you can delete one of the LogIns
            Admin.DeleteLogIn(l1.LogInName);
            Assert.IsNull(Query.GetLogInDetails(l1.LogInName));

            //Test if you can delete all of the rest LogIns
            Admin.DeleteLogIns();
            Assert.IsNull(Query.GetLogInDetails(l3.LogInName));
            Assert.IsNull(Query.GetLogInDetails(l4.LogInName));
        }

        [TestMethod]
        public void DeleteLogIns()
        {
            //add multiple logins
            Admin.AddLogIn(l1);
            Admin.AddLogIn(l2);
            IList<LogIn> logIns = Admin.ListLogIns();
            Assert.IsTrue(logIns.Count > 0);

            //check if you can delete all logins
            Admin.DeleteLogIns();
            logIns = Admin.ListLogIns();
            Assert.IsTrue(logIns.Count == 0);
        }
    }
}
