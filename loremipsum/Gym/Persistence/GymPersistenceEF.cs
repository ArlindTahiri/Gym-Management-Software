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
            throw new NotImplementedException();
        }

        public void DeleteArticles()
        {
            throw new NotImplementedException();
        }

        public void DeleteContract(int contractID)
        {
            throw new NotImplementedException();
        }

        public void DeleteContracts()
        {
            throw new NotImplementedException();
        }

        public void DeleteEmployee(int employeeID)
        {
            throw new NotImplementedException();
        }

        public void DeleteEmployees()
        {
            throw new NotImplementedException();
        }

        public void DeleteMember(int memberID)
        {
            throw new NotImplementedException();
        }

        public void DeleteMembers()
        {
            throw new NotImplementedException();
        }

        public void DeleteOrder(int orderID)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrders()
        {
            throw new NotImplementedException();
        }

        public Article FindArticle(int articleID)
        {
            throw new NotImplementedException();
        }

        public IList<Article> FindArticles()
        {
            throw new NotImplementedException();
        }

        public Contract FindContract(int contractID)
        {
            throw new NotImplementedException();
        }

        public IList<Contract> FindContracts()
        {
            throw new NotImplementedException();
        }

        public Employee FindEmployee(int employeeID)
        {
            throw new NotImplementedException();
        }

        public IList<Employee> FindEmployees()
        {
            throw new NotImplementedException();
        }

        public Member FindMember(int memberID)
        {
            throw new NotImplementedException();
        }

        public IList<Member> FindMembers()
        {
            throw new NotImplementedException();
        }

        public Order FindOrder(int orderID)
        {
            throw new NotImplementedException();
        }

        public IList<Order> FindOrders()
        {
            throw new NotImplementedException();
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
