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
        public Member m1, m2, m3;
        public Contract c1, c2;

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
            Admin.DeleteMembers();
            Admin.DeleteContracts();

            //add contract so we can add members
            c1 = new Contract("Premium Plan_tess", 20);
            c2 = new Contract("Standart_test", 10);
        }

        [TestMethod]
        public void TestMember()
        {
            //Add contract to add member m1
            Admin.AddContract(c1);
            c1 = Query.GetContractDetails(c1.ContractID);
            m1 = Admin.AddMember(c1.ContractID, "Martin_test", "Meyer", "Mohrenstrasse 54", 04161, "Leipzig", "Deutschland",
                    "martinmeyer@gmail.com", "DE94500105172327561324", new DateTime(1990, 11, 24));
            m1 = Query.GetMemberDetails(m1.MemberID);

            //Test if the member is in the database
            Assert.IsTrue(m1.ContractID == c1.ContractID && m1.Forename.Equals("Martin_test") && m1.Surname.Equals("Meyer") && m1.Street.Equals("Mohrenstrasse 54") &&
                    m1.PostcalCode == 04161 && m1.City.Equals("Leipzig") && m1.Country.Equals("Deutschland") &&
                    m1.EMail.Equals("martinmeyer@gmail.com") && m1.Iban.Equals("DE94500105172327561324") && m1.Birthday.Equals(new DateTime(1990, 11, 24)));

            //update member m1
            Admin.UpdateMember(m1.MemberID, "Jennifer_test2", "Meier", "Rhinstrasse 96", 80711, "München", "Deutschland",
                  "jennifer.meier@gmail.com", "DE90500105174767217514", new DateTime(1991, 11, 24));
            m1 = Query.GetMemberDetails(m1.MemberID);
            //check if the updated member is in the database
            Assert.IsTrue(m1.ContractID == c1.ContractID && m1.Forename.Equals("Jennifer_test2") && m1.Surname.Equals("Meier") && m1.Street.Equals("Rhinstrasse 96") &&
                    m1.PostcalCode == 80711 && m1.City.Equals("München") && m1.Country.Equals("Deutschland") &&
                    m1.EMail.Equals("jennifer.meier@gmail.com") && m1.Iban.Equals("DE90500105174767217514") && m1.Birthday.Equals(new DateTime(1991, 11, 24)));

            //update contract from member m1
            Admin.AddContract(c2);
            Admin.UpdateContractFromMember(m1.MemberID, c2.ContractID);
            m1 = Query.GetMemberDetails(m1.MemberID);
            Assert.IsTrue(m1.ContractID == c2.ContractID);

            //Test if you can update the contract if contract == null
            int invalid = -1;
            Admin.UpdateContractFromMember(m1.MemberID, invalid);
            m1 = Query.GetMemberDetails(m1.MemberID);
            Assert.IsTrue(m1.ContractID != invalid);

            //check if there are members in the IList
            IList<Member> members = Admin.ListMembers();
            Assert.IsTrue(members.Count > 0);

            //test if we can delete m1 who is currently training
            Admin.InsertTrainingMember(m1.MemberID);
            Admin.DeleteMember(m1.MemberID);
            Assert.IsNotNull(Query.GetMemberDetails(m1.MemberID));
            Admin.DeleteTrainingMember(m1.MemberID);

            //delete member m1
            Admin.DeleteMember(m1.MemberID);
            Assert.IsNull(Query.GetMemberDetails(m1.MemberID));

            Admin.DeleteContract(c1.ContractID);
            Admin.DeleteContract(c2.ContractID);
        }
    }
}
