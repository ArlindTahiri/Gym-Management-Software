using loremipsum.Gym.Persistence;
using loremipsum.Gym;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using loremipsum.Gym.Entities;
using System.Diagnostics.Contracts;
using System.Diagnostics.Metrics;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Contract = loremipsum.Gym.Entities.Contract;

namespace UnitTestLoremIpsum
{
    [TestClass]
    public class UnitTestOrders
    {
        private IProductAdmin Admin;
        private IProductModule Query;
        private Contract contract1, contract2;
        private Article article1, article2;
        private Employee employee1, employee2;


        [TestInitialize()]
        public void SetUp()
        {
            IGymPersistence persistence = new GymPersistenceEF();
            GymFactory factory = new GymFactory(persistence);
            Admin = factory.GetProductAdmin();
            Query = factory.GetProductModule();
        }

        public void GenerateTestData()
        {
            contract1 = new Contract("Premium Plan", 2999);
            contract2 = new Contract("Normal Plan", 1999);

            article1 = new Article("Protein Shake Vanille", 499, 10, 10);
            article2 = new Article("Energydrink", 299, 15, 15);

            employee1 = new Employee("Anton", "Zunhammer", "Lindenstraﬂe 3", 83374, "Traunwalchen", "Deutschland", 
                    "Anton.Zunhammer@gmail.com", "DE23423423423423423423", new DateTime(1999, 1, 1), " ");
            employee2 = new Employee("Nina", "Niedl", "Eichenweg 3", 83301, "Traunreut", "Deutschland", 
                    "Nina.Niedl@gmail.com", "DE11112222333344445555", new DateTime(1987, 2, 23), " ");

        }

        [TestMethod]
        public void TestOrder()
        {
            Admin.AddContract(contract1);
            Admin.AddArticle(article1);
            Member member1 = Admin.AddMember(contract1.ContractID, "Paul", "Peters", "Rosenheimer Straﬂe 32", 83059, "Kolbermoor", "Deutschland", 
                    "paul.peters@gmail.com", "DE90500105171113716976", new DateTime(2002, 10, 01));
            Assert.IsTrue(Query.GetContractDetails(contract1.ContractID) != null);
            Assert.IsTrue(Query.GetArticleDetails(article1.ArticleID) != null);
            Assert.IsTrue(Query.GetMemberDetails(member1.MemberID) != null);

            //create Order

            Order order1 = Admin.AddOrder(member1.MemberID, article1.ArticleID, 3);                          
            Assert.IsTrue(Query.GetOrderDetails(order1.OrderID) != null);

            //Edit Order
            Admin.UpdateArticleToOrder(order1.OrderID, article2.ArticleID, 4);
            //check if it changed

            //Delete Order

            if (Query.GetOrderDetails(order1.OrderID) != null)
            {
                Admin.DeleteOrder(order1.OrderID);
            }
            Assert.IsTrue(Query.GetOrderDetails(order1.OrderID) == null);           //check if amount from member is back to "normal"
        }

        [TestMethod]
        public void TestContract()
        {
            //create Contract
            Admin.AddContract(contract2);
            Assert.IsTrue(Query.GetContractDetails(contract2.ContractID) != null);

            //update contract
            Admin.UpdateContract(contract2.ContractID, "VIP Plan", 3499);           //check if it isnt the same contract as bevor

            //delete contract
            if (Query.GetContractDetails(contract2.ContractID) != null);
            {
                Admin.DeleteContract(contract2.ContractID);                         //check if member has this contract as well...
            }
            Assert.IsTrue(Query.GetContractDetails(contract2.ContractID) == null);
        }

        [TestMethod]
        public void TestMember()
        {

            //create Member
            Member member2 = Admin.AddMember(contract2.ContractID, "Lisa", "Berger", "Bergen 23", 83234, "Bergen", "Deutschland",
                    "lisa.berger@gmail.com", "DE01234567890123456789", new DateTime(2000, 4, 20));
            Assert.IsTrue(Query.GetMemberDetails(member2.MemberID) != null);

            //update Memberdata
            Admin.UpdateMember(member2.MemberID, "Paul", "Peters", "Rosenheimer Straﬂe 32", 83059, "Kolbermoor", "Deutschland",
                    "paul.peters@gmail.com", "DE90500105171113716976", new DateTime(2002, 10, 01));
            //check if it really changed

            //update contract from member
            Admin.UpdateContractFromMember(member2.MemberID, contract1.ContractID);
            //check if contract changed from member

            //delete member
            if(Query.GetMemberDetails(member2.MemberID) != null)                    //check if money from member is 0
            {
                Admin.DeleteMember(member2.MemberID);
            }
            Assert.IsTrue(Query.GetMemberDetails(contract1.ContractID) == null);
        }

        [TestMethod]
        public void TestArticle()
        {
            //add article
            Admin.AddArticle(article2);
            Assert.IsTrue(Query.GetArticleDetails(article2.ArticleID) != null);

            //update article
            Admin.UpdateArticle(article2.ArticleID, "Energydrink", 399, 20, 20);
            //check if it changed

            //delete article
            if(Query.GetArticleDetails(article1.ArticleID) != null)                 //check if there are articles left
            {
                Admin.DeleteArticle(article1.ArticleID);
            }
            Assert.IsTrue(Query.GetArticleDetails(article1.ArticleID) == null);
        }

        [TestMethod]
        public void DeleteEmployee()
        {
            //add employee
            Admin.AddEmployee(employee2);
            Assert.IsTrue(Query.GetEmployeeDetails(employee2.EmployeeID) != null);

            //edit employee
            Admin.UpdateEmployee(employee2.EmployeeID, "Nina", "Niedl", "Eichenweg 7", 83301, "Traunreut", "Deutschland",
                    "Nina.Niedl@gmail.com", "DE1111111111111111111111111", new DateTime(1987, 2, 23), " ");
            //check if it changed (Adress && IBAN)

            //delete employee
            if(Query.GetEmployeeDetails(employee1.EmployeeID) != null)
            {
                Admin.DeleteEmployee(employee1.EmployeeID);
            }
            Assert.IsTrue(Query.GetEmployeeDetails(employee1.EmployeeID) == null);
        }
    }
}