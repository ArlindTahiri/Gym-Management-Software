using loremipsum.Gym.Entities;
using Microsoft.EntityFrameworkCore;

namespace loremipsum.Gym.Persistence
{
    public class GymPersistenceEF : IGymPersistence
    {

        //Member
        public void CreateMember(Member member)
        {
            using (GymContext db = new GymContext())
            {
                db.Members.Add(member);
                db.SaveChanges();
            }
        }

        public void DeleteMember(int memberID)
        {
            using (GymContext db = new GymContext())
            {

                Member member = db.Members
                    .Where(b => b.MemberID == memberID)
                    .Include(b => b.Orders)
                    .FirstOrDefault();

                if (member != null)
                {
                    db.Members.Remove(member);
                    db.SaveChanges();
                }
            }
        }

        public void DeleteMembers()
        {
            using (GymContext db = new GymContext())
            {

                IList<Member> members = FindMembers();
                db.Members.RemoveRange(members);
                db.SaveChanges();
            }
        }

        public Member FindMember(int memberID)
        {
            using (GymContext db = new GymContext())
            {
                Member member = db.Members
                    .Where(b => b.MemberID == memberID)
                    .Include(b => b.Orders)
                    .FirstOrDefault();

                return member;
            }
        }

        public IList<Member> FindMembers()
        {
            using (GymContext db = new GymContext())
            {
                IEnumerable<Member> members = db.Members
                    .Include(b => b.Orders)
                    .ToList();
                return (List<Member>)members;
            }
        }

        public void UpdateContractFromMember(Member member, Contract contract)
        {
            using (GymContext db = new GymContext())
            {
                Member m = db.Members
                    .Where(b => b.MemberID == member.MemberID)
                    .Include(b => b.Orders)
                    .FirstOrDefault();

                m.Contract = contract;
                db.SaveChanges();
            }
        }

        public void UpdateMember(Member member, string forename, string surname, string street, int postcalCode, string city, string country, string eMail, string iban, DateTime birthday)
        {
            using (GymContext db = new GymContext())
            {
                Member m = db.Members
                    .Where(b => b.MemberID == member.MemberID)
                    .Include(b => b.Orders)
                .FirstOrDefault();

                m.Forename = forename;
                m.Surname = surname;
                m.Street = street;
                m.PostcalCode = postcalCode;
                m.City = city;
                m.Country = country;
                m.EMail = eMail;
                m.Iban = iban;
                m.Birthday = birthday;

                db.SaveChanges();
            }
        }



        //Contract
        public void CreateContract(Contract contract)
        {
            using (GymContext db = new GymContext())
            {
                db.Contracts.Add(contract);
                db.SaveChanges();
            }
        }

        public void DeleteContract(int contractID)
        {
            using (GymContext db = new GymContext())
            {

                Contract contract = db.Contracts
                    .Where(b => b.ContractID == contractID)
                    .Include(b => b.Members)
                    .FirstOrDefault();

                if (contract != null)
                {
                    db.Contracts.Remove(contract);
                    db.SaveChanges();
                }
            }
        }

        public void DeleteContracts()
        {
            using (GymContext db = new GymContext())
            {

                IList<Contract> contracts = FindContracts();
                db.Contracts.RemoveRange(contracts);
                db.SaveChanges();
            }
        }

        public Contract FindContract(int contractID)
        {
            using (GymContext db = new GymContext())
            {

                Contract contract = db.Contracts
                    .Where(b => b.ContractID == contractID)
                    .Include(b => b.Members)
                    .FirstOrDefault();
                return contract;
            }
        }

        public IList<Contract> FindContracts()
        {
            using (GymContext db = new GymContext())
            {
                IEnumerable<Contract> contracts = db.Contracts
                    .Include(b => b.Members)
                    .ToList();
                return (List<Contract>)contracts;
            }
        }

        public void UpdateContract(Contract contract, string contractType, TimeSpan duration, int price)
        {
            using (GymContext db = new GymContext())
            {
                Contract c = db.Contracts
                    .Where(b => b.ContractID == contract.ContractID)
                    .Include(b => b.Members)
                    .FirstOrDefault();
                
                c.ContractType = contractType;
                c.Duration = duration;
                c.Price = price;

                db.SaveChanges();

            }
        }



        //Employee
        public void CreateEmployee(Employee employee)
        {
            using (GymContext db = new GymContext())
            {
                db.Employees.Add(employee);
                db.SaveChanges();
            }
        }

        public void DeleteEmployee(int employeeID)
        {
            using (GymContext db = new GymContext())
            {

                Employee employee = db.Employees
                    .Where(b => b.EmployeeID == employeeID)
                    .FirstOrDefault();
                if (employee != null)
                {
                    db.Employees.Remove(employee);
                    db.SaveChanges();
                }
            }
        }

        public void DeleteEmployees()
        {
            using (GymContext db = new GymContext())
            {

                IList<Employee> employees = FindEmployees();
                db.Employees.RemoveRange(employees);
                db.SaveChanges();
            }
        }

        public Employee FindEmployee(int employeeID)
        {
            using (GymContext db = new GymContext())
            {

                Employee employee = db.Employees
                    .Where(b => b.EmployeeID == employeeID)
                    .FirstOrDefault();
                return employee;
            }
        }

        public IList<Employee> FindEmployees()
        {
            using (GymContext db = new GymContext())
            {
                IEnumerable<Employee> employees = db.Employees
                    .ToList();
                return (List<Employee>)employees;
            }
        }

        public void UpdateEmployee(Employee employee, string forename, string surname, string street, int postcalCode, string city, string country, string eMail, string iban, DateTime birthday, string status)
        {
            using (GymContext db = new GymContext())
            {

                Employee e = db.Employees
                    .Where(b => b.EmployeeID == employee.EmployeeID)
                    .FirstOrDefault();

                e.Forename = forename;
                e.Surname = surname;
                e.Street = street;
                e.PostcalCode = postcalCode;
                e.City = city;
                e.Country = country;
                e.EMail = eMail;
                e.Iban= iban;
                e.Birthday = birthday;
                e.Status = status;

                db.SaveChanges();
            }
        }



        //Article
        public void CreateArticle(Article article)
        {
            using (GymContext db = new GymContext())
            {
                db.Articles.Add(article);
                db.SaveChanges();
            }
        }

        public void DeleteArticle(int articleID)
        {
            using (GymContext db = new GymContext())
            {

                Article article = db.Articles
                    .Where(b => b.ArticleID == articleID)
                    .Include(b => b.Orders)
                    .FirstOrDefault();
                if (article != null)
                {
                    db.Articles.Remove(article);
                    db.SaveChanges();
                }
            }
        }

        public void DeleteArticles()
        {
            using (GymContext db = new GymContext())
            {

                IList<Article> articles = FindArticles();
                db.Articles.RemoveRange(articles);
                db.SaveChanges();
            }
        }

        public Article FindArticle(int articleID)
        {
            using (GymContext db = new GymContext())
            {

                Article article = db.Articles
                    .Where(b => b.ArticleID == articleID)
                    .Include(b => b.Orders)
                    .FirstOrDefault();
                return article;
            }
        }

        public IList<Article> FindArticles()
        {
            using (GymContext db = new GymContext())
            {
                IEnumerable<Article> articles = db.Articles
                    .Include(b => b.Orders)
                    .ToList();
                return (List<Article>)articles;
            }
        }

        public void UpdateArticle(Article article, string articleName, int price, int actualStock, int targetStock)
        {
            using (GymContext db = new GymContext())
            {
                Article a = db.Articles
                    .Where(b => b.ArticleID == article.ArticleID)
                    .Include(b => b.Orders)
                    .FirstOrDefault();

                a.ArticleName = articleName;
                a.Price = price;
                a.ActualStock = actualStock;
                a.TargetStock = targetStock;

                db.SaveChanges();
            }
        }



        //Order
        public void CreateOrder(Order order)
        {
            using (GymContext db = new GymContext())
            {
                Article article = db.Articles
                    .Where(b => b.ArticleID == order.ArticleID)
                    .Include(b => b.Orders)
                    .FirstOrDefault();

                Member member = db.Members
                    .Where(b => b.MemberID == order.MemberID)
                    .Include(b => b.Orders)
                    .FirstOrDefault();

                article.ActualStock = article.ActualStock - order.Amount;

                member.CurrentBill = member.CurrentBill + order.Amount* FindArticle(order.ArticleID).Price;

                db.Orders.Add(order);
                db.SaveChanges();
            }
        }

        public void DeleteOrder(int orderID) //mistake order--> money back to member and articles get back
        {
            using (GymContext db = new GymContext())
            {
                Order order = db.Orders
                    .Where(b => b.OrderID == orderID)
                    .FirstOrDefault();

                Article article = db.Articles
                    .Where(b => b.ArticleID == order.ArticleID)
                    .Include(b => b.Orders)
                    .FirstOrDefault();

                article.ActualStock = article.ActualStock - order.Amount;

                Member member = db.Members
                    .Where(b => b.MemberID == order.MemberID)
                    .Include(b => b.Orders)
                    .FirstOrDefault();

                member.CurrentBill = member.CurrentBill - order.Amount * FindArticle(order.ArticleID).Price;

                db.Orders.Remove(order);
                db.SaveChanges();
                
            }
        }

        public void DeleteOrders()//this delete just deletes all orders to have a cleaner database --> no mistake --> no money back for members
        {
            using (GymContext db = new GymContext())
            {
                IList<Order> orders = FindOrders();
                db.Orders.RemoveRange(orders);
                db.SaveChanges();
            }
        }

        public Order FindOrder(int orderID)
        {
            using (GymContext db = new GymContext())
            {

                Order order = db.Orders
                    .Where(b => b.OrderID == orderID)
                    .FirstOrDefault();
                return order;
            }
        }

        public IList<Order> FindOrders()
        {
            using (GymContext db = new GymContext())
            {
                IEnumerable<Order> orders= db.Orders
                    .ToList();
                return (List<Order>) orders;
            }
        }

        public void UpdateOrder(Order order, Article newArticle, int amount)
        {
            using (GymContext db = new GymContext())
            {
                Article originalArticle = db.Articles
                    .Where(b => b.ArticleID == order.ArticleID)
                    .Include(b => b.Orders)
                    .FirstOrDefault();

                originalArticle.ActualStock = originalArticle.ActualStock + order.Amount;
                newArticle.ActualStock = newArticle.ActualStock - amount;

                Member member = db.Members
                    .Where(b => b.MemberID == order.MemberID)
                    .Include(b => b.Orders)
                    .FirstOrDefault();

                member.CurrentBill = member.CurrentBill + FindArticle(order.ArticleID).Price * order.Amount - newArticle.Price * amount;

                Order o = db.Orders
                    .Where(b => b.OrderID == order.OrderID)
                    .FirstOrDefault();

                o.ArticleID=newArticle.ArticleID;
                o.Amount=amount;

                db.SaveChanges();
                
            }
        }

        public void UpdateMemberFromOrder(Order order, Member member)
        {
            using (GymContext db = new GymContext())
            {
                Order o = db.Orders
                    .Where(b => b.OrderID == order.OrderID)
                    .FirstOrDefault();

                o.MemberID = member.MemberID;
                db.SaveChanges();
            }
        }



        //LogIn
        public void CreateLogIn(LogIn logIn)
        {
            using (GymContext db = new GymContext())
            {
                db.LogIns.Add(logIn);
                db.SaveChanges();
            }
        }

        public void DeleteLogIn(string logInName)
        {
            using (GymContext db = new GymContext())
            {
                LogIn logIn = db.LogIns
                    .Where(b => b.LogInName == logInName)
                    .FirstOrDefault();

                if (logIn != null)
                {
                    db.LogIns.Remove(logIn);
                    db.SaveChanges();
                }
            }
        }

        public IList<LogIn> FindLogIns()
        {
            using (GymContext db = new GymContext())
            {
                IEnumerable<LogIn> logIns = db.LogIns
                    .ToList();
                return (List<LogIn>)logIns;
            }
        }

        public LogIn FindLogIn(string logInName)
        {
            using (GymContext db = new GymContext())
            {

                LogIn logIn = db.LogIns
                    .Where(b => b.LogInName == logInName)
                    .FirstOrDefault();
                return logIn;
            }
        }

        public void DeleteLogIns()
        {
            using (GymContext db = new GymContext())
            {

                IList<LogIn> logIns = FindLogIns();
                db.LogIns.RemoveRange(logIns);
                db.SaveChanges();
            }
        }

        public void UpdateLogInName(LogIn logIn, string newLogInName, string logInPassword)
        {
            using (GymContext db = new GymContext())
            {
                LogIn l = db.LogIns
                    .Where(b => b.LogInName == logIn.LogInName)
                    .FirstOrDefault();

                l.LogInName=newLogInName;

                db.SaveChanges();
            }

        }

        public void UpdateLogInPassword(LogIn logIn, string newLogInPassword)
        {
            using (GymContext db = new GymContext())
            {
                LogIn l = db.LogIns
                    .Where(b => b.LogInName == logIn.LogInName)
                    .FirstOrDefault();

                l.LogInPassword=newLogInPassword;

                db.SaveChanges();
            }
        }

        public void UpdateLogInRank(LogIn logIn, int rank)
        {
            using (GymContext db = new GymContext())
            {
                LogIn l = db.LogIns
                    .Where(b => b.LogInName == logIn.LogInName)
                    .FirstOrDefault();

                l.Rank = rank;

                db.SaveChanges();
            }
        }

    }
}