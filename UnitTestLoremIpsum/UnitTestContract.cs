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


            //Test if you can upload the same contract multiple times:
            Assert.ThrowsException<Microsoft.EntityFrameworkCore.DbUpdateException>(() => Admin.AddContract(c1));


            //Test if you can upload different contract object but same properties.
            Assert.ThrowsException<Microsoft.EntityFrameworkCore.DbUpdateException>(() => Admin.AddContract(c2));


            //Add also the other contracts
            Admin.AddContract(c3);
            Admin.AddContract(c4);
        }

        [TestMethod]
        public void UpdateContract()
        {
            //Test if you can update Contract Properties
            Admin.UpdateContract(c3.ContractID,"Premium Pro Plan", 3499);
            Contract newContract = Query.GetContractDetails(c3.ContractID);
            Assert.IsTrue(newContract.Price == 3499 && newContract.ContractType.Equals("Premium Pro Plan"));
        }

        [TestMethod]
        public void DeleteContract()
        {
            //Test if you can delete one of the contracts
            Admin.DeleteContract(c1.ContractID);
            Assert.IsNull(Query.GetContractDetails(c1.ContractID));

            //Test if you can delete the same contract multiple times
            Assert.ThrowsException<Microsoft.EntityFrameworkCore.DbUpdateException>(() => Admin.DeleteContract(c1.ContractID));

            //Test if you can delete all of the rest contracts
            Admin.DeleteContracts();
            Assert.IsNull(Query.GetContractDetails(c3.ContractID));
            Assert.IsNull(Query.GetContractDetails(c4.ContractID));
        }

    }
}
