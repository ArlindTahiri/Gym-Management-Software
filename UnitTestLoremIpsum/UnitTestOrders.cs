using loremipsum.Gym.Persistence;
using loremipsum.Gym;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using loremipsum.Gym.Entities;

namespace UnitTestLoremIpsum
{
    [TestClass]
    public class UnitTestOrders
    {
        private IProductAdmin Admin;
        private IProductModule Query;

        [TestInitialize()]
        public void SetUp()
        {
            IGymPersistence persistence = new GymPersistenceEF();
            GymFactory factory = new GymFactory(persistence);
            Admin = factory.GetProductAdmin();
            Query = factory.GetProductModule();
            
        }


        [TestMethod]
        public void CreateOrder()
        {
            Contract contract = new Contract("Premium Plan", 2999);
            Admin.AddContract(contract);
            Article aProteinShakeVanille = new Article("Protein Shake Vanille", 499, 10, 10);
            Admin.AddArticle(aProteinShakeVanille);

            Member mPaulPeters = new Member("Paul", "Peters", "Rosenheimer Straﬂe 32", 83059, "Kolbermoor", "Deutschland", "paul.peters@gmail.com", "DE90500105171113716976", new DateTime(2002, 10, 01));
            Admin.AddMember(mPaulPeters); //still can't be added to database. Problem: 1:n relation to book
            

            Order order = new Order(mPaulPeters.MemberID, aProteinShakeVanille.ArticleID, 5);
            Admin.AddOrder(order);
            Assert.IsTrue(Query.GetOrderDetails(order.OrderID) != null);
        }
    }
}