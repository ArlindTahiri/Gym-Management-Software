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
    internal class UnitTestMember
    {
        private IProductAdmin Admin;
        private IProductModule Query;
        private Member m1, m2, m3, m4, m5;
        private Contract c1, c2, c3;
        private Article a1;

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
            c3 = null;
            m5 = null;
            a1 = new Article("Standart", 10, 10, 10);
           // m1 = new Member("Jennifer", "Meier", "Rhinstrasse 96", 80711, "München", "Deutschland", "jennifer.meier@gmail.com", "DE90500105174767217514", new DateTime(1991, 11, 24), c1.ContractID);
           // m2 = new Member("Stephan", "Mahler", "Kurfuerstendamm 54", 85605, "Aschheim", "Deutschland", "stephanmahler@dayrep.com", "DE89500105178259939697", new DateTime(1988, 11, 10),c1.ContractID);
           // m3 = new Member("Jan", "Wechlser", "Alt-Moabbit 49", 04541, "Borna", "Deutschland", "janwechlser@yourrapide.com", "DE09500105174887754664", new DateTime(1996, 5, 14), c2.ContractID);
           // m4 = new Member("Patrick", "Huber", "Brandenburgische Str 92", 55592, "Desloch", "Deutschland", "patrickhuber@rhyta.com", "DE22500105179467727673", new DateTime(1997, 7, 4), c2.ContractID);
        }

        [TestMethod]
        public void CreateMember()
        {
            //Add the member m1 & test if m1 is in the database
            Member m1 = Admin.AddMember(c1.ContractID, "Jennifer", "Meier", "Rhinstrasse 96", 80711, "München", "Deutschland",
                    "jennifer.meier@gmail.com", "DE90500105174767217514", new DateTime(1991, 11, 24));
            Assert.IsTrue(Query.GetMemberDetails(m1.MemberID).CompareTo(m1) == 0);
            Console.WriteLine("Mitglied erfolgreich hinzugefügt");


            //Test if you can upload a member who has an invalid contract
            Member m4 = Admin.AddMember(c3.ContractID, "Patrick", "Huber", "Brandenburgische Str 92", 55592, "Desloch", "Deutschland",
                    "patrickhuber@rhyta.com", "DE22500105179467727673", new DateTime(1997, 7, 4));
            Assert.IsNull(Query.GetMemberDetails(m4.MemberID));
            

            //Add member m2 and m3
            Member m2 = Admin.AddMember(c1.ContractID, "Stephan", "Mahler", "Kurfuerstendamm 54", 85605, "Aschheim", "Deutschland",
                "stephanmahler@dayrep.com", "DE89500105178259939697", new DateTime(1988, 11, 10));
            Member m3 = Admin.AddMember(c2.ContractID, "Jan", "Wechlser", "Alt-Moabbit 49", 04541, "Borna", "Deutschland",
                "janwechlser@yourrapide.com", "DE09500105174887754664", new DateTime(1996, 5, 14));
        }

        [TestMethod]
        public void UpdateMember()
        {
            //Test if you can update Member properties
            Admin.UpdateMember(m1.MemberID, "Stephan", "Mahler", "Kurfuerstendamm 54", 85605, "Aschheim", "Deutschland",
                "stephanmahler@dayrep.com", "DE89500105178259939697", new DateTime(1988, 11, 10));
            Member newMember = Query.GetMemberDetails(m1.MemberID);
            Assert.IsTrue(newMember.Forename.Equals("Stephan") && newMember.Surname.Equals("Mahler") && newMember.Street.Equals("Kurfuerstendamm 54") &&
                    newMember.PostcalCode == 85605 && newMember.City.Equals("Aschheim") && newMember.Country.Equals("Deutschland") &&
                    newMember.EMail.Equals("stephanmahler@dayrep.com") && newMember.Iban.Equals("DE89500105178259939697") && newMember.Birthday.Equals(new DateTime(1988, 11, 10)));


            //Test if you can update an invalid member
            Admin.UpdateMember(m5.MemberID, "Florian", "Wirth", "Mühlenstrasse 53", 96018, "Bamberg", "Deutschland", 
                    "florianwirth@gmail.com", "DE03500105179299611495", new DateTime(1977, 10, 10));
            newMember = Query.GetMemberDetails(m5.MemberID);
            Assert.IsFalse(newMember.Forename.Equals("Florian") && newMember.Surname.Equals("Wirth") && newMember.Street.Equals("Mühlenstrasse 53") &&
                    newMember.PostcalCode == 96018 && newMember.City.Equals("Bamberg") && newMember.Country.Equals("Deutschland") &&
                    newMember.EMail.Equals("florianwirth@gmail.com") && newMember.Iban.Equals("DE03500105179299611495") && newMember.Birthday.Equals(new DateTime(1977, 10, 10)));
            

            //Test if you can update the contract from member
            Admin.UpdateContractFromMember(m1.MemberID, c2.ContractID);
            Assert.IsTrue(m1.ContractID.Equals(c2.ContractID));


            //Test if you can update the contract from an invalid member or an member with an invalid contract
            Admin.UpdateContractFromMember(m5.MemberID, c1.ContractID);
            Assert.IsFalse(m5.ContractID.Equals(c1.ContractID));
            
            Admin.UpdateContractFromMember(m1.MemberID, c3.ContractID);
            Assert.IsFalse(m1.ContractID.Equals(c3.ContractID));
        }

        public void DeleteMember()
        {
            //Test if you can delete m1
            Admin.DeleteMember(m1.MemberID);
            Assert.IsNull(Query.GetMemberDetails(m1.MemberID));


            //Test if you can delete an member who is training right now
            Admin.InsertTrainingMember(m2.MemberID);
            Admin.DeleteMember(m2.MemberID);
            Assert.IsNotNull(Query.GetMemberDetails(m2.MemberID));


            //Test if you can delete an member who has an order right now
            Admin.AddOrder(m3.MemberID, a1.ArticleID, 5);
            Admin.DeleteMember(m3.MemberID);
            Assert.IsNotNull(Query.GetMemberDetails(m3.MemberID));


            //Test if you can delete all members even if some are currently training
            Admin.DeleteMembers();
            Assert.IsNotNull(Query.GetMemberDetails(m2.MemberID));

            Admin.DeleteTrainingMember(m2.MemberID);


            //Test if you can delete all member even if they currently have active orders
            Admin.DeleteMembers();

            Assert.IsNull(Query.GetMemberDetails(m2.MemberID));
            Assert.IsNull(Query.GetMemberDetails(m3.MemberID));
            Assert.IsNull(Query.GetMemberDetails(m4.MemberID));
            Assert.IsNull(Query.GetMemberDetails(m5.MemberID));
        }
    }
}
