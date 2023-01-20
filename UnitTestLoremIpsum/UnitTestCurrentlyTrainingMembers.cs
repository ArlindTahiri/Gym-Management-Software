using loremipsum.Gym;
using loremipsum.Gym.Entities;
using loremipsum.Gym.Persistence;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestLoremIpsum
{
    [TestClass]
    public class UnitTestCurrentlyTrainingMembers
    {
        public IProductAdmin Admin;
        public IProductModule Query;
        public Member m1;
        public Contract c1;

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
            //add contract and members
            c1 = new Contract("Standart_test", 10);
            Admin.AddContract(c1);
            m1 = Admin.AddMember(c1.ContractID, "Martina_test", "Meyer", "Mohrenstrasse 54", 04161, "Leipzig", "Deutschland",
                    "martinmeyer@gmail.com", "DE94500105172327561324", new DateTime(1990, 11, 24));

        }

        [TestMethod]
        public void TestCurrentlyTrainingMembers()
        {
            //save how many members are currently training
            IList<Member> members = Admin.ListTrainingMembers();
            int count = members.Count;

            //add member m1 to training
            Admin.InsertTrainingMember(m1.MemberID);

            //check if count is +1
            members = Admin.ListTrainingMembers();
            Assert.IsTrue(members.Count == count + 1);
            
            //check if member m1 is currently training
            IList<int> membersID = Admin.ListTrainingMembersID();
            
            //member m1 stop training
            Admin.DeleteTrainingMember(m1.MemberID);

            //check if current training members == count
            members = Admin.ListTrainingMembers();
            Assert.IsTrue(members.Count == count);

            //delete every entity we created in this test
            Admin.DeleteMember(m1.MemberID);
            Admin.DeleteContract(c1.ContractID);
        }
    }
}
