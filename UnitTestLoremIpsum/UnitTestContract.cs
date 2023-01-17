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
    public class UnitTestContract
    {
        private IProductAdmin Admin;
        private IProductModule Query;
        private Contract c1, c2, c3, c4;
        private Member m1;


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
            c1 = new Contract("Normal Plan", 1999);
            c2 = new Contract("Normal Plan", 1999);
            c3 = new Contract("Premium Plan", 2999);
            c4 = new Contract("Premium Plus Plan", 3999);
        }

        [TestMethod]
        public void CreateContract()
        {
            //Add the contract c1
            Admin.AddContract(c1);

            //Test if the contract is in the database
            Assert.IsTrue(Query.GetContractDetails(c1.ContractID).CompareTo(c1) == 0);
        }

        [TestMethod]
        public void ListContracts()
        {
            //add an contract and than check if there are contracts
            Admin.AddContract(c1);
            IList<Contract> contracts = Admin.ListContracts();
            Assert.IsTrue(contracts.Count > 0);

            //now delete them and check again
            Admin.DeleteContracts();
            contracts = Admin.ListContracts();
            Assert.IsTrue(contracts.Count == 0);
        }

        [TestMethod]
        public void UpdateContract()
        {
            Admin.AddContract(c3);
            //Test if you can update Contract Properties
            Admin.UpdateContract(c3.ContractID,"Premium Pro Plan", 3499);
            Contract newContract = Query.GetContractDetails(c3.ContractID);
            Assert.IsTrue(newContract.Price == 3499 && newContract.ContractType.Equals("Premium Pro Plan"));
        }

        [TestMethod]
        public void DeleteContract()
        {
            //add an contract and than check if you can delete it
            Admin.AddContract(c1);
            Assert.IsNotNull(Query.GetContractDetails(c1.ContractID));
            Admin.DeleteContract(c1.ContractID);
            Assert.IsNull(Query.GetArticleDetails(c1.ContractID));

            //add an member who has the contract c2 and than try to delete c2
            Admin.AddContract(c2);
            m1 = Admin.AddMember(c2.ContractID, "Martin", "Meyer", "Mohrenstrasse 54", 04161, "Leipzig", "Deutschland",
                    "martinmeyer@gmail.com", "DE94500105172327561324", new DateTime(1990, 11, 24));
            Admin.DeleteContract(c2.ContractID);
            Assert.IsNotNull(Query.GetContractDetails(c2.ContractID));

            Admin.DeleteMember(m1.MemberID);
            Admin.DeleteContract(c2.ContractID);
        }

        [TestMethod]
        public void deleteContracts()
        {
            //add contracts and members
            Admin.AddContract(c1);
            Admin.AddContract(c2);
            m1 = Admin.AddMember(c2.ContractID, "Martin", "Meyer", "Mohrenstrasse 54", 04161, "Leipzig", "Deutschland",
                    "martinmeyer@gmail.com", "DE94500105172327561324", new DateTime(1990, 11, 24));
            
            //check if you can delete all contracts even if members exists
            Admin.DeleteContracts();
            IList<Contract> contracts = Admin.ListContracts();
            Assert.IsTrue(contracts.Count > 0);

            //delete all members and try again
            Admin.DeleteMembers();
            Admin.DeleteContracts();
            contracts = Admin.ListContracts();
            Assert.IsTrue(contracts.Count == 0);
        }
    }
}
