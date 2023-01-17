using loremipsum.Gym;
using loremipsum.Gym.Entities;
using loremipsum.Gym.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestLoremIpsum
{
    [TestClass]
    public class UnitTestMember
    {
        public IProductAdmin Admin;
        public IProductModule Query;
        public Member m1, m2, m3, m4, m5;
        public Contract c1, c2;
        public Article a1;

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
            //add contract so we can add members
            c1 = new Contract("Premium Plan", 20);
            Admin.AddContract(c1);
            c2 = new Contract("Standart", 10);

            

            //add member so we don't have to add members in every TestMethod
            m1 = Admin.AddMember(c1.ContractID, "Martin", "Meyer", "Mohrenstrasse 54", 04161, "Leipzig", "Deutschland",
                    "martinmeyer@gmail.com", "DE94500105172327561324", new DateTime(1990, 11, 24));
            m4 = Admin.AddMember(c1.ContractID, "Jan", "Wechlser", "Alt-Moabbit 49", 04541, "Borna", "Deutschland", 
                    "janwechlser@yourrapide.com", "DE09500105174887754664", new DateTime(1996, 5, 14));
            m5 = Admin.AddMember(c1.ContractID, "Patrick", "Huber", "Brandenburgische Str 92", 55592, "Desloch", "Deutschland", 
                    "patrickhuber@rhyta.com", "DE22500105179467727673", new DateTime(1997, 7, 4));

            //add article so we can add orders
            a1 = new Article("Milk Shake", 3, 20, 20);
        }

        [TestMethod]
        public void AddMember()
        {
            //test if there is an contract
            Assert.IsTrue(c1.ContractType.Equals("Premium Plan") && c1.Price == 20);

            //Add member m2 and test if m2 is in the database
            m2 = Admin.AddMember(c1.ContractID, "Jennifer", "Meier", "Rhinstrasse 96", 80711, "München", "Deutschland",
                  "jennifer.meier@gmail.com", "DE90500105174767217514", new DateTime(1991, 11, 24));
            Assert.IsTrue(m2.ContractID == c1.ContractID && m2.Forename.Equals("Jennifer") && m2.Surname.Equals("Meier") && m2.Street.Equals("Rhinstrasse 96") &&
                    m2.PostcalCode == 80711 && m2.City.Equals("München") && m2.Country.Equals("Deutschland") &&
                    m2.EMail.Equals("jennifer.meier@gmail.com") && m2.Iban.Equals("DE90500105174767217514") && m2.Birthday.Equals(new DateTime(1991, 11, 24)));

            Console.WriteLine("Mitglied erfolgreich hinzugefügt");

            int invalid = -1;


            //Add member m3 who has no contract and test if m3 is in the database
            m3 = Admin.AddMember(invalid, "Anton", "Müller", "Rhinstrasse 90", 80711, "München", "Deutschland",
                   "anton.mueller@gmail.com", "DE90500105174767217514", new DateTime(1991, 1, 24));
            Assert.IsNull(m3);

        }

        [TestMethod]
        public void UpdateMember()
        {
            //Update member m1 and test if it has changed
            Admin.UpdateMember(m1.MemberID, "Swen", "Weber", "Grolmanstraße 94", 28217, "Bremen Steffensweg", "Deutschland",
                    "swenweber@teleworm.us", "DE94500105171116564145", new DateTime(2000, 2, 2));
            Assert.IsTrue(m1.Forename.Equals("Swen") && m1.Surname.Equals("Weber") && m1.Street.Equals("Grolmanstraße 94") &&
                    m1.PostcalCode == 28217 && m1.City.Equals("Bremen Steffensweg") && m1.Country.Equals("Deutschland") &&
                    m1.EMail.Equals("swenweber@teleworm.us") && m1.Iban.Equals("DE94500105171116564145") && m1.Birthday.Equals(new DateTime(2000, 2, 2)));

            //Update invalid member //how to test if it did something?
            int invalid = -1;
            Admin.UpdateMember(invalid, "Christina", "Eisenhauer", "Grolmanstraße 90", 28210, "Bremen", "Deutschland",
                    "christinaeisenhauer@teleworm.us", "DE25500105174647679764", new DateTime(2000, 3, 3));
        }

        [TestMethod]
        public void UpdateContractFromMember()
        {
            //Test if you can update the contract from m1 to c2
            Admin.UpdateContractFromMember(m1.MemberID, c2.ContractID);
            Assert.IsTrue(m1.ContractID == c2.ContractID);

            //Test if you can update the contract if (member == null || contract == null)
            int invalid = -1;
            Admin.UpdateContractFromMember(invalid, c2.ContractID);

            Admin.UpdateContractFromMember(m1.MemberID, invalid);
            Assert.IsTrue(m1.ContractID != invalid);

            Admin.UpdateContractFromMember(invalid, invalid);

            //Test if contract can't be checkouted-> contract should not change            
            Admin.UpdateContractFromMember(m1.MemberID, c1.ContractID);
            Assert.IsTrue(m1.ContractID != c1.ContractID);
        }

        [TestMethod]
        public void ListMembers()
        {
            //Test should not be null because we added some members
            Assert.IsNotNull(Admin.ListMembers());
        }

        [TestMethod]
        public void DeleteMember()
        {
            //Test if we can delete an member
            Admin.DeleteMember(m1.MemberID);
            Assert.IsNull(m1);

            //Test if we can delete m4 who is currently training
            Admin.InsertTrainingMember(m4.MemberID);
            Admin.DeleteMember(m4.MemberID);
            Assert.IsNotNull(m4);
            Admin.DeleteTrainingMember(m4.MemberID);

            //Test if we can delete an invalid member
            int invalid = -1;
            Admin.DeleteMember(invalid);

            //Test if we can delete m4 who has an order
            Admin.AddOrder(m4.MemberID, a1.ArticleID, 5);
            Admin.DeleteMember(m4.MemberID);
            Assert.IsNotNull(m4);
            Admin.DeleteOrders();
        }

        [TestMethod]
        public void DeleteMembers()
        {
            //Test if we can delete all members if someone currently are training
            Admin.InsertTrainingMember(m4.MemberID);
            Admin.DeleteMembers();
            Assert.IsNotNull(Admin.ListMembers());
            Admin.DeleteTrainingMember(m4.MemberID);

            //Test if we can delete all members if someone has an order
            Admin.AddOrder(m4.MemberID, a1.ArticleID, 5);
            Admin.DeleteMembers();
            Assert.IsNotNull(Admin.ListMembers());
            Admin.DeleteOrders();

            Admin.DeleteMembers();
            Assert.IsNull(Admin.ListMembers());
        }
    }
}
