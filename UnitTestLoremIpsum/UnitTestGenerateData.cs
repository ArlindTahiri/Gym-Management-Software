using log4net;
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
    public class UnitTestGenerateData
    {
        private IProductAdmin Admin;
        private IProductModule Query;
        private Article a1, a2, a3, a4, a5, a6;
        private Contract c1, c2, c3, c4;
        private Employee e1, e2, e3, e4, e5;
        private Member m1, m2, m3, m4, m5, m6;
        private Order o1, o2, o3, o4, o5, o6;
        private LogIn l1, l2, l3, l4, l5;


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
            //delete all entities in database
            IList<int> currentlytrainingmember = Admin.ListTrainingMembersID();
            foreach (int memberID in currentlytrainingmember)
            {
                Admin.DeleteTrainingMember(memberID);
            }
            if (Admin.CheckOutMembers())
            {
                Admin.DeleteEmployees();
                Admin.DeleteOrders();
                if (Admin.DeleteMembers())
                {
                    Admin.DeleteArticles();
                    Admin.DeleteMembers();
                    Admin.DeleteContracts();
                    Admin.DeleteLogIns();
                }

            }

            //LogIns
            l1 = new LogIn("admin", "admin",1);
            l2 = new LogIn("user451", "blaurot!3",2);
            l3 = new LogIn("user342", "tischge!st",2);
            l4 = new LogIn("user023", "@land3mine",2);
            l5 = new LogIn("user394", "langhantel",2);

            //Articles
            a1 = new Article("Crunchy Riegel", 999, 20, 20);
            a2 = new Article("Vegan Protein (500g)", 1299, 35, 35);
            a3 = new Article("Crunchy Riegel Multipack", 589, 15, 15);
            a4 = new Article("Creatine Caps 130Stk", 2499, 50, 50);
            a5 = new Article("Protein Bar",1999, 20, 20);
            a6 = new Article("Fitness Müsli", 699, 24, 24);

            //Employees
            e1 = new Employee("Luca", "Wagner", "Mohrenstrasse 12", 83022, "Rosenheim", "Deutschland", "lucawagner@gmail.com", "DE20500105173684112312", new DateTime(2003,02,20));
            e2 = new Employee("Robert", "Zweig", "Eichendorffstr. 97", 83023, "Rosenheim", "Deutschland", "robert.zweig@yourrapide.com", "DE63500105178436376278", new DateTime(2001, 11,5));
            e3 = new Employee("Ralf", "Achen", "Günzelstrasse 30", 83043, "Bad Aibling", "Deutschland", "ralfachen@t-online.de", "DE82500105174947414946", new DateTime(1996, 1,7));
            e4 = new Employee("Kathrin", "Trommler", "Brandenburgische Str 64", 83064, "Raubling", "Deutschland", "katrintrommler@gmai.com", "DE97500105177596284468", new DateTime(1991,6,1));
            e5 = new Employee("Andrea", "Nadel", "Schaarsteinweg 74", 83539, "Pfaffing", "Deutschland", "andreanadel@t-online.de", "DE05500105171195661293", new DateTime(1995,6,14));

            //Contracts
            c1 = new Contract("Standart", 1999);
            c2 = new Contract("Standart Plus",2499);
            c3 = new Contract("Premium", 2999);
            c4 = new Contract("All inclusive", 3499);
        }

        [TestMethod]
        public void AddData()
        {
            //add logins
            Admin.AddLogIn(l1);
            Admin.AddLogIn(l2);
            Admin.AddLogIn(l3);
            Admin.AddLogIn(l4);
            Admin.AddLogIn(l5);

            //check if 5 logins are in database
            IList<LogIn> logIns= Admin.ListLogIns();
            Assert.IsTrue(logIns.Count == 5);


            //add articles
            Admin.AddArticle(a1);
            Admin.AddArticle(a2);
            Admin.AddArticle(a3);
            Admin.AddArticle(a4);
            Admin.AddArticle(a5);
            Admin.AddArticle(a6);

            //check if 6 articles are in database
            IList<Article> articles = Admin.ListArticles();
            Assert.IsTrue(articles.Count == 6);


            //add employees
            Admin.AddEmployee(e1);
            Admin.AddEmployee(e2);
            Admin.AddEmployee(e3);
            Admin.AddEmployee(e4);
            Admin.AddEmployee(e5);

            //check if 5 employees are in database
            IList<Employee> employees = Admin.ListEmployees();
            Assert.IsTrue(employees.Count == 5);


            //add contracts
            Admin.AddContract(c1);
            Admin.AddContract(c2);
            Admin.AddContract(c3);
            Admin.AddContract(c4);

            //check if 4 contracts are in database
            IList<Contract> contracts = Admin.ListContracts();
            Assert.IsTrue(contracts.Count == 4);


            //add members
            m1 = Admin.AddMember(c1.ContractID, "Martin", "Maier", "Mohrenstrasse 54", 83052, "Brückmühl", "Deutschland",
                    "martinmaier@gmail.com", "DE94500105172327561324", new DateTime(1990, 11, 24));
            m2 = Admin.AddMember(c2.ContractID, "Ralph", "Wechlser", "Alt-Moabbit 49", 83043, "Bad Aibling", "Deutschland",
                    "ralphwechlser@yourrapide.com", "DE09500105174887754664", new DateTime(1996, 5, 14));
            m3 = Admin.AddMember(c4.ContractID, "Lucas", "Huber", "Brandenburgische Str 92", 83088, "Kiefersfelden", "Deutschland",
                    "lucashuber@rhyta.com", "DE22500105179467727673", new DateTime(1997, 7, 4));
            m4 = Admin.AddMember(c3.ContractID, "Sven", "Frueh", "Mohrenstrasse 54", 83052, "Brückmühl", "Deutschland",
                    "svenfrueh@gmail.com", "DE94500105172327561324", new DateTime(1980, 10, 24));
            m5 = Admin.AddMember(c1.ContractID, "Marcel", "Kohl", "Alt-Moabbit 39", 83043, "Bad Aibling", "Deutschland",
                    "marcelkohl@yourrapide.com", "DE09500105174887754664", new DateTime(2000, 7, 24));
            m6 = Admin.AddMember(c2.ContractID, "Mario", "Baer", "Brandenburger Str 32", 83088, "Kiefersfelden", "Deutschland",
                    "mariobear@rhyta.com", "DE22500105179467727673", new DateTime(1995, 12, 21));

            //check if 6 members are in database
            IList<Member> members = Admin.ListMembers();
            Assert.IsTrue(members.Count == 6);

            //add orders
            o1 = Admin.AddOrder(m1.MemberID, a1.ArticleID, 3);
            o2 = Admin.AddOrder(m2.MemberID, a4.ArticleID, 6);
            o3 = Admin.AddOrder(m1.MemberID, a3.ArticleID, 10);
            o4 = Admin.AddOrder(m4.MemberID, a2.ArticleID, 5);
            o5 = Admin.AddOrder(m5.MemberID, a6.ArticleID, 20);
            o6 = Admin.AddOrder(m6.MemberID, a6.ArticleID, 4);
        }
    }
}
