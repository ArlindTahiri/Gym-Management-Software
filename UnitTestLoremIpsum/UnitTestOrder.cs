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
        private Order o1, o2;
        private Contract c1;
        private Member m1, m2;
        private Article a1, a2;

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
            c1 = new Contract("Premium Plan_test", 2999);

            a1 = new Article("Protein Shake Vanille", 499, 10, 10);
            a2 = new Article("Energ bar", 199, 40, 40);
        }

        [TestMethod]
        public void TestOrder()
        {
            //add contract, member and article to create an order
            Admin.AddContract(c1);
            m1 = Admin.AddMember(c1.ContractID, "Paul_test", "Peters", "Rosenheimer Straﬂe 32", 83059, "Kolbermoor", "Deutschland",
                    "paul.peters@gmail.com", "DE90500105171113716976", new DateTime(2002, 10, 01));
            Admin.AddArticle(a1);

            //add order o1
            o1 = Admin.AddOrder(m1.MemberID, a1.ArticleID, 5);

            //Test if the order is in the database
            o1 = Query.GetOrderDetails(o1.OrderID);
            Assert.IsTrue(o1.MemberID == m1.MemberID && o1.ArticleID == a1.ArticleID && o1.Amount == 5);

            //Test if you can add an order where the amount is more than there articles are in current stock
            o2 = Admin.AddOrder(m1.MemberID, a1.ArticleID, 20);
            Assert.IsNull(o2);

            //check if there are orders
            IList<Order> orders = Admin.ListOrders();
            Assert.IsTrue(orders.Count > 0);

            //check if m1 has orders
            orders = Admin.ListAllOrdersFromMember(m1.MemberID);
            Assert.IsTrue(orders.Count == 1);

            //add article a2 and member m2 and than update order o1
            Admin.AddArticle(a2);
            m2 = Admin.AddMember(c1.ContractID, "Stephan_test", "Mahler", "Kurfuerstendamm 54", 85605, "Aschheim", "Deutschland",
                "stephanmahler@dayrep.com", "DE89500105178259939697", new DateTime(1988, 11, 10));
            Admin.UpdateOrder(o1.OrderID, m2.MemberID, a2.ArticleID, 3);

            //test if the updated order is in the database
            o1 = Query.GetOrderDetails(o1.OrderID);
            Assert.IsTrue(o1.MemberID == m2.MemberID && o1.ArticleID == a2.ArticleID && o1.Amount == 3);

            //check if m1 has orders
            orders = Admin.ListAllOrdersFromMember(m1.MemberID);
            Assert.IsTrue(orders.Count == 0);

            //check the amount o1 article a1 and a2
            a1 = Query.GetArticleDetails(a1.ArticleID);
            Assert.IsTrue(a1.ActualStock == 10);

            a2 = Query.GetArticleDetails(a2.ArticleID);
            Assert.IsTrue(a2.ActualStock == 37);

            //test if you can update an order to an amount which is more than the actual stock
            Admin.UpdateOrder(o1.OrderID, m2.MemberID, a2.ArticleID, 50);
            o1 = Query.GetOrderDetails(o1.OrderID);
            Assert.IsFalse(o1.Amount == 50);

            //delete order o1
            Admin.DeleteOrder(o1.OrderID);
            Assert.IsNull(Query.GetOrderDetails(o1.OrderID));

            //check if m2 has orders
            orders = Admin.ListAllOrdersFromMember(m2.MemberID);
            Assert.IsTrue(orders.Count == 0);

            //delete every entity we created in this test
            Admin.DeleteMember(m1.MemberID);
            Admin.DeleteMember(m2.MemberID);
            Admin.DeleteArticle(a1.ArticleID);
            Admin.DeleteArticle(a2.ArticleID);
            Admin.DeleteContract(c1.ContractID);
        }
    }
}