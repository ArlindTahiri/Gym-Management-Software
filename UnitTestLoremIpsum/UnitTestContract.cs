using loremipsum.Gym;
using loremipsum.Gym.Entities;
using loremipsum.Gym.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace UnitTestLoremIpsum
{
    [TestClass]
    public class UnitTestContract
    {
        private IProductAdmin Admin;
        private IProductModule Query;
        private Contract c1, c2;
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
            c1 = new Contract("Normal Plan", 10);
            c2 = new Contract("Normal Plan", 10);
        }

        [TestMethod]
        public void TestContract()
        {
            //Add the contract c1
            Admin.AddContract(c1);

            //Test if the contract is in the database
            Assert.IsTrue(Query.GetContractDetails(c1.ContractID).CompareTo(c1) == 0);

            //Add an contract c2 which is identical with c1
            Admin.AddContract(c2);
            Assert.IsTrue(Query.GetContractDetails(c2.ContractID) == null);

            //Update Contract Properties from c1
            Admin.UpdateContract(c1.ContractID, "Premium Pro Plan", 3499);

            //Test if contract c1 is with new properties in database
            Contract newContract = Query.GetContractDetails(c1.ContractID);
            Assert.IsTrue(newContract.Price == 3499 && newContract.ContractType.Equals("Premium Pro Plan"));

            //check if there are contracts in the IList
            IList<Contract> contracts = Admin.ListContracts();
            Assert.IsTrue(contracts.Count > 0);


            //add an member who has contract c1 and try to delete c1
            m1 = Admin.AddMember(c1.ContractID, "Martin", "Meyer", "Mohrenstrasse 54", 04161, "Leipzig", "Deutschland",
                    "martinmeyer@gmail.com", "DE94500105172327561324", new DateTime(1990, 11, 24));

            //delete contract c1
            Admin.DeleteContract(c1.ContractID);

            //check if contract c1 is still in database
            Assert.IsNotNull(Query.GetContractDetails(c1.ContractID));

            //delete member and than delete c1
            Admin.DeleteMember(m1.MemberID);
            Admin.DeleteContract(c1.ContractID);

            //check if c1 is still in database
            Assert.IsNull(Query.GetContractDetails(c1.ContractID));
        }
    }
}
