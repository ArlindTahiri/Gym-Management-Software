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
                    db.Remove<Member>(member);
                    db.SaveChanges();
                }
            }
        }

        public void DeleteMembers()
        {
            using (GymContext db = new GymContext())
            {

                IList<Member> members = FindMembers();
                db.RemoveRange(members);
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
                member.Contract = contract;
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
                    db.Remove<Contract>(contract);
                    db.SaveChanges();
                }
            }
        }

        public void DeleteContracts()
        {
            using (GymContext db = new GymContext())
            {

                IList<Contract> contracts = FindContracts();
                db.RemoveRange(contracts);
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
                    db.Remove<Employee>(employee);
                    db.SaveChanges();
                }
            }
        }

        public void DeleteEmployees()
        {
            using (GymContext db = new GymContext())
            {

                IList<Employee> employees = FindEmployees();
                db.RemoveRange(employees);
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
                    db.Remove<Article>(article);
                    db.SaveChanges();
                }
            }
        }

        public void DeleteArticles()
        {
            using (GymContext db = new GymContext())
            {

                IList<Article> articles = FindArticles();
                db.RemoveRange(articles);
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

        public void UpdateArticle(Article article, int actualStock, int targetStock)
        {
            using (GymContext db = new GymContext())
            {
                article.ActualStock = actualStock;
                article.TargetStock = targetStock;
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

                article.ActualStock = article.ActualStock - order.Amount;
                db.Orders.Add(order);
                db.SaveChanges();
            }
        }

        public void DeleteOrder(int orderID)
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
                
                db.Orders.Remove(order);
                db.SaveChanges();
                
            }
        }

        public void DeleteOrders()
        {
            using (GymContext db = new GymContext())
            {

                IList<Order> orders = FindOrders();
                foreach (Order order in orders)
                {
                    FindArticle(order.ArticleID).ActualStock = FindArticle(order.ArticleID).ActualStock + order.Amount;
                }
                db.RemoveRange(orders);
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
                newArticle.ActualStock = newArticle.ActualStock - order.Amount;

                db.SaveChanges();
                
            }
        }

        public void UpdateMemberFromOrder(Order order, Member member)
        {
            using (GymContext db = new GymContext())
            {
                order.MemberID = member.MemberID;
                db.SaveChanges();
            }
        }

    }
}