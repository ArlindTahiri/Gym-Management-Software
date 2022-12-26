using loremipsum.Gym.Entities;

namespace loremipsum.Gym.Persistence
{
    public class GymPersistenceEF : IGymPersistence
    {
        public void CreateArticle(Article article)
        {
            using (GymContext db = new GymContext())
            {

                db.Articles.Add(article);
                db.SaveChanges();
            }
        }

        public void CreateContract(Contract contract)
        {
            using (GymContext db = new GymContext())
            {

                db.Contracts.Add(contract);
                db.SaveChanges();
            }
        }

        public void CreateEmployee(Employee employee)
        {
            using (GymContext db = new GymContext())
            {

                db.Employees.Add(employee);
                db.SaveChanges();
            }
        }

        public void CreateMember(Member member)
        {
            using (GymContext db = new GymContext())
            {

                db.Members.Add(member);
                db.SaveChanges();
            }
        }

        public void CreateOrder(Order order)
        {
            using (GymContext db = new GymContext())
            {

                db.Orders.Add(order);
                db.SaveChanges();
            }
        }

        public void DeleteArticle(int articleID)
        {
            using (GymContext db = new GymContext())
            {

                Article article = FindArticle(articleID);
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

        public void DeleteContract(int contractID)
        {
            using (GymContext db = new GymContext())
            {

                Contract contract = FindContract(contractID);
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

        public void DeleteEmployee(int employeeID)
        {
            using (GymContext db = new GymContext())
            {

                Employee employee = FindEmployee(employeeID);
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

        public void DeleteMember(int memberID)
        {
            using (GymContext db = new GymContext())
            {

                Member member = FindMember(memberID);
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

        public void DeleteOrder(int orderID)
        {
            using (GymContext db = new GymContext())
            {

                Order order = FindOrder(orderID);
                if (order != null)
                {
                    db.Remove<Order>(order);
                    db.SaveChanges();
                }
            }
        }

        public void DeleteOrders()
        {
            using (GymContext db = new GymContext())
            {

                IList<Order> orders = FindOrders();
                db.RemoveRange(orders);
                db.SaveChanges();
            }
        }

        public Article FindArticle(int articleID)
        {
            using (GymContext db = new GymContext())
            {

                Article article = db.Articles
                    .Where(b => b.ArticleID == articleID)
                    .FirstOrDefault();
                return article;
            }
        }

        public IList<Article> FindArticles()
        {
            using (GymContext db = new GymContext())
            {
                IEnumerable<Article> articles = db.Articles
                    .ToList();
                return (List<Article>)articles;
            }
        }

        public Contract FindContract(int contractID)
        {
            using (GymContext db = new GymContext())
            {

                Contract contract = db.Contracts
                    .Where(b => b.ContractID == contractID)
                    .FirstOrDefault();
                return contract;
            }
        }

        public IList<Contract> FindContracts()
        {
            using (GymContext db = new GymContext())
            {
                IEnumerable<Contract> contracts = db.Contracts
                    .ToList();
                return (List<Contract>)contracts;
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

        public Member FindMember(int memberID)
        {
            using (GymContext db = new GymContext())
            {
                Member member = db.Members
                    .Where(b => b.MemberID == memberID)
                    .FirstOrDefault();
                return member;
            }
        }

        public IList<Member> FindMembers()
        {
            using (GymContext db = new GymContext())
            {
                IEnumerable<Member> members= db.Members
                    .ToList();
                return (List<Member>)members;
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

        public void UpdateArticle(int articleid, int actualStock, int targetStock)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrder(Order order, int articleID)
        {
            throw new NotImplementedException();
        }

        public void UpdateContractFromMember(Member member, int contractID)
        {
            throw new NotImplementedException();
        }

        public void UpdateMemberFromOrder(Order order, int memberID)
        {
            throw new NotImplementedException();
        }
    }
}