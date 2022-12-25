using loremipsum.Gym.Entities;

namespace loremipsum.Gym.Impl
{
    public class Gym : IProductAdmin, IProductModule
    {
        private readonly IGymPersistence persistence;

        public Gym(IGymPersistence persistence)
        {
            this.persistence = persistence;
        }

        # region IProductAdmin
        public void AddArticle(Article article)
        {
            throw new NotImplementedException();
        }

        public void AddContract(Contract contract)
        {
            throw new NotImplementedException();
        }

        public void AddEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void AddMember(Member member, int contractID)
        {
            throw new NotImplementedException();
        }

        public void AddMemberToOrder(int orderID, int memberID)
        {
            throw new NotImplementedException();
        }

        public void AddOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void RemoveArticle(int articleID, int memberID)
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

        public IList<Order> ListAllOrdersFromMember(int memberID)
        {
            throw new NotImplementedException();
        }

        public IList<Article> ListArticles()
        {
            throw new NotImplementedException();
        }

        public IList<Contract> ListContracts()
        {
            throw new NotImplementedException();
        }

        public IList<Employee> ListEmployees()
        {
            throw new NotImplementedException();
        }

        public IList<Member> ListMembers()
        {
            throw new NotImplementedException();
        }

        public IList<Order> ListOrders()
        {
            throw new NotImplementedException();
        }

        public void UpdateContractFromMember(int memberid, int contractID)
        {
            throw new NotImplementedException();
        }

        public void UpdateArticle(int articleID, int acutalStock, int targetStock)
        {
            throw new NotImplementedException();
        }

        public void AddArticleToOrder(int orderID, int articleID)
        {
            throw new NotImplementedException();
        }

        #endregion


        # region IProductModule

        public IDictionary<Article, int> SearchArticle(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public IDictionary<Contract, int> SearchContract(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public IDictionary<Employee, int> SearchEmployee(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public IDictionary<Member, int> SearchMember(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public IDictionary<Order, int> SearchOrder(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public Article GetArticleDetails(int articleID)
        {
            throw new NotImplementedException();
        }

        public int GetArticlesActualStock(int articleID)
        {
            throw new NotImplementedException();
        }

        public int GetArticlesTargetStock(int articleID)
        {
            throw new NotImplementedException();
        }

        public Contract GetContractDetails(int contractID)
        {
            throw new NotImplementedException();
        }

        public Employee GetEmployeeDetails(int employeeID)
        {
            throw new NotImplementedException();
        }

        public Member GetMemberDetails(int memberID)
        {
            throw new NotImplementedException();
        }

        public Order GetOrderDetails(int orderID)
        {
            throw new NotImplementedException();
        }
        
        #endregion

    }
}
