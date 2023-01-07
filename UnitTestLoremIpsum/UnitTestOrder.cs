using loremipsum.Gym.Persistence;
using loremipsum.Gym;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using loremipsum.Gym.Entities;
using System.Numerics;

namespace UnitTestLoremIpsum
{
    [TestClass]
    public class UnitTestOrders
    {
        private IProductAdmin Admin;
        private IProductModule Query;
        private Order o1, o2, o3;
        private Contract contract;
        private Member m1, m2;
        private Article a1, a2;

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
            contract = new Contract("Premium Plan", 2999);

            a1 = new Article("Protein Shake Vanille", 499, 10, 10);
            a2 = new Article("Energ bar", 199, 40, 40);

            Member m1 = Admin.AddMember(contract.ContractID, "Paul", "Peters", "Rosenheimer Straﬂe 32", 83059, "Kolbermoor", "Deutschland",
                    "paul.peters@gmail.com", "DE90500105171113716976", new DateTime(2002, 10, 01));
            Member m2 = Admin.AddMember(contract.ContractID, "Stephan", "Mahler", "Kurfuerstendamm 54", 85605, "Aschheim", "Deutschland",
                "stephanmahler@dayrep.com", "DE89500105178259939697", new DateTime(1988, 11, 10));


        }

        [TestMethod]
        public void CreateOrder()
        {
            //add contract and article  to create an order
            Admin.AddContract(contract);
            Admin.AddArticle(a1);
            Admin.AddArticle(a2);

            Assert.IsTrue(Query.GetContractDetails(contract.ContractID) != null);
            Assert.IsTrue(Query.GetArticleDetails(a1.ArticleID) != null);
            Assert.IsTrue(Query.GetMemberDetails(m1.MemberID) != null);

            //add order && test if order is in database
            Order o1 = Admin.AddOrder(m1.MemberID, a1.ArticleID, 5);
            Assert.IsTrue(Query.GetOrderDetails(o1.OrderID).CompareTo(o1) == 0);

            //test if you can upload multiple times the same order
            Assert.ThrowsException<Microsoft.EntityFrameworkCore.DbUpdateException>(() => Admin.AddOrder(m1.MemberID, a1.ArticleID, 5));

            //test if you can add an order which ned more articles than there are in stock
            Assert.ThrowsException<Microsoft.EntityFrameworkCore.DbUpdateException>(() => Admin.AddOrder(m2.MemberID, a2.ArticleID, 100));

            //Add orders o2, o3
            Order o2 = Admin.AddOrder(m2.MemberID, a2.ArticleID, 10);
            Order o3 = Admin.AddOrder(m1.MemberID, a1.ArticleID, 15);

        }

        [TestMethod]
        public void UpdateOrder()
        {
            //Test if you can update an Member from an Order
            Admin.UpdateOrder(o1.OrderID, m2.MemberID, o1.ArticleID, 10);
            Assert.IsTrue(o1.MemberID == m2.MemberID);
        }

        [TestMethod]
        public void DeleteOrder()
        {
            //Test if you can delete o1
            Admin.DeleteOrder(o1.OrderID);
            Assert.IsNull(Query.GetOrderDetails(o1.OrderID));

            //Test if you can delete the same order multiple times
            Assert.ThrowsException<Microsoft.EntityFrameworkCore.DbUpdateException>(() => Admin.DeleteOrder(o1.OrderID));

            //Test if you can delete all orders
            Admin.DeleteOrders();
            Assert.IsNull(Query.GetOrderDetails(o2.OrderID));
            Assert.IsNull(Query.GetOrderDetails(o3.OrderID));
        }
    }
}