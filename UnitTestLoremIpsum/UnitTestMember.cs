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
    internal class UnitTestMember
    {
        private IProductAdmin Admin;
        private IProductModule Query;
        private Member m1, m2, m3, m4;
        private Contract c1, c2;

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
            c1 = new Contract("Premium Plan", 2999);
            c2 = new Contract("Normal Plan", 1999);

           // m1 = new Member("Jennifer", "Meier", "Rhinstrasse 96", 80711, "München", "Deutschland", "jennifer.meier@gmail.com", "DE90500105174767217514", new DateTime(1991, 11, 24), c1.ContractID);
           // m2 = new Member("Stephan", "Mahler", "Kurfuerstendamm 54", 85605, "Aschheim", "Deutschland", "stephanmahler@dayrep.com", "DE89500105178259939697", new DateTime(1988, 11, 10),c1.ContractID);
           // m3 = new Member("Jan", "Wechlser", "Alt-Moabbit 49", 04541, "Borna", "Deutschland", "janwechlser@yourrapide.com", "DE09500105174887754664", new DateTime(1996, 5, 14), c2.ContractID);
           // m4 = new Member("Patrick", "Huber", "Brandenburgische Str 92", 55592, "Desloch", "Deutschland", "patrickhuber@rhyta.com", "DE22500105179467727673", new DateTime(1997, 7, 4), c2.ContractID);
        }

        [TestMethod]
        public void createMember()
        {
            //Add the member m1 & test if m1 is in the database
            Member m1 = Admin.AddMember(c1.ContractID, "Jennifer", "Meier", "Rhinstrasse 96", 80711, "München", "Deutschland",
                    "jennifer.meier@gmail.com", "DE90500105174767217514", new DateTime(1991, 11, 24));
            Assert.IsTrue(Query.GetMemberDetails(m1.MemberID).CompareTo(m1) == 0);

            //Test if you can upload the same member multiple times
            Assert.ThrowsException<Microsoft.EntityFrameworkCore.DbUpdateException>(() => 
                Admin.AddMember(c1.ContractID, "Jennifer", "Meier", "Rhinstrasse 96", 80711, "München", "Deutschland",
                    "jennifer.meier@gmail.com", "DE90500105174767217514", new DateTime(1991, 11, 24)));

            //Add member m2 and m3
            Member m2 = Admin.AddMember(c1.ContractID, "Stephan", "Mahler", "Kurfuerstendamm 54", 85605, "Aschheim", "Deutschland",
                "stephanmahler@dayrep.com", "DE89500105178259939697", new DateTime(1988, 11, 10));
            Member m3 = Admin.AddMember(c2.ContractID, "Jan", "Wechlser", "Alt-Moabbit 49", 04541, "Borna", "Deutschland",
                "janwechlser@yourrapide.com", "DE09500105174887754664", new DateTime(1996, 5, 14));
        }

        [TestMethod]
        public void updateMember()
        {
            //Test if you can update Member properties
            Admin.UpdateMember(m1.MemberID, "Stephan", "Mahler", "Kurfuerstendamm 54", 85605, "Aschheim", "Deutschland",
                "stephanmahler@dayrep.com", "DE89500105178259939697", new DateTime(1988, 11, 10));
            Member newMember = Query.GetMemberDetails(m1.MemberID);
            Assert.IsTrue(newMember.Forename.Equals("Stephan") && newMember.Surname.Equals("Mahler") && newMember.Street.Equals("Kurfuerstendamm 54") &&
                    newMember.PostcalCode == 85605 && newMember.City.Equals("Aschheim") && newMember.Country.Equals("Deutschland") &&
                    newMember.EMail.Equals("stephanmahler@dayrep.com") && newMember.Iban.Equals("DE89500105178259939697") && newMember.Birthday.Equals(new DateTime(1988, 11, 10)));

            //Test if you can update the contract from member
            Admin.UpdateContractFromMember(m1.MemberID, c2.ContractID);
            Assert.IsTrue(m1.ContractID.Equals(c2.ContractID));
        }

        public void deleteMember()
        {
            //Test if you can delete m1
            Admin.DeleteMember(m1.MemberID);
            Assert.IsNull(Query.GetMemberDetails(m1.MemberID));

            //Test if you can delete the same member multiple times
            Assert.ThrowsException<Microsoft.EntityFrameworkCore.DbUpdateException>(() => Admin.DeleteMember(m1.MemberID));

            //Test if you can delete all members
            Admin.DeleteMembers();
            Assert.IsNull(Query.GetMemberDetails(m2.MemberID));
            Assert.IsNull(Query.GetMemberDetails(m3.MemberID));
        }
    }
}
