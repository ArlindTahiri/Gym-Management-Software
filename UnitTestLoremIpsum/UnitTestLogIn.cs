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
            l1 = new LogIn("peterchef", "Passwort+1!", 1);
            l2 = new LogIn("peterchef", "Passwort+1!", 1);
            l3 = new LogIn("antonmitarbeiter", "Passwort+1!", 2);
            l4 = new LogIn("ninamitarbeiter", "Passwort+1!", 2);
        }

        [TestMethod]
        public void CreateLogIn()
        {
            //Add the LogIn c1
            Admin.AddLogIn(l1);


            //Test if the LogIn is in the database
            Assert.IsTrue(Query.GetLogInDetails(l1.LogInName).CompareTo(l1) == 0);


            //Test if you can upload the same LogIn multiple times:
            Assert.ThrowsException<Microsoft.EntityFrameworkCore.DbUpdateException>(() => Admin.AddLogIn(l1));


            //Test if you can upload different LogIn object but same properties.
            Assert.ThrowsException<Microsoft.EntityFrameworkCore.DbUpdateException>(() => Admin.AddLogIn(l2));


            //Add also the other LogIn
            Admin.AddLogIn(l3);
            Admin.AddLogIn(l4);
        }

        [TestMethod]
        public void UpdateLogIn()
        {
            //Test if you can update LogIn Properties
            Admin.UpdateLogIn(l3.LogInName, "antoniomitarbeiter", "Passwort+1!lol",3);
            LogIn newLogIn = Query.GetLogInDetails(l3.LogInName);
            Assert.IsTrue(newLogIn.LogInName.Equals("antoniomitarbeiter") && newLogIn.LogInPassword.Equals("Passwort+1!lol") && newLogIn.Rank == 3);
        }

        [TestMethod]
        public void DeleteLogIn()
        {
            //Test if you can delete one of the LogIns
            Admin.DeleteLogIn(l1.LogInName);
            Assert.IsNull(Query.GetLogInDetails(l1.LogInName));

            //Test if you can delete the same LogIn multiple times
            Assert.ThrowsException<Microsoft.EntityFrameworkCore.DbUpdateException>(() => Admin.DeleteLogIn(l1.LogInName));

            //Test if you can delete all of the rest LogIns
            Admin.DeleteLogIns();
            Assert.IsNull(Query.GetLogInDetails(l3.LogInName));
            Assert.IsNull(Query.GetLogInDetails(l4.LogInName));
        }

    }
}
