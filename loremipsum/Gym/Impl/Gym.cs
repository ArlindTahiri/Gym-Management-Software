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
           persistence.CreateArticle(article);
        }

        public void AddContract(Contract contract)
        {
            persistence.CreateContract(contract);
        }

        public void AddEmployee(Employee employee)
        {
            persistence.CreateEmployee(employee);
        }

        public void AddMember(Member member, int contractID)
        {
            persistence.CreateMember(member);
        }

        public void AddMemberToOrder(int orderID, int memberID)
        {
          Order order = persistence.FindOrder(orderID);
            if (order != null) 
                persistence.UpdateMemberFromOrder(order, memberID);
        }

        public void AddOrder(Order order)
        {
           persistence.CreateOrder(order);
        }

        public void RemoveArticle(int articleID, int memberID)
        {
            persistence.DeleteArticle(articleID);
        }

        public void DeleteArticles()
        {
            persistence.DeleteArticles();
        }

        public void DeleteContract(int contractID)
        {
           persistence.DeleteContract(contractID);
        }

        public void DeleteContracts()
        {
            persistence.DeleteContracts();  
        }

        public void DeleteEmployee(int employeeID)
        {
            persistence.DeleteEmployee(employeeID); 
        }

        public void DeleteEmployees()
        {
           persistence.DeleteEmployees();
        }

        public void DeleteMember(int memberID)
        {
           persistence.DeleteMember(memberID);
        }

        public void DeleteMembers()
        {
           persistence.DeleteMembers();
        }

        public void DeleteOrder(int orderID)
        {
            persistence.DeleteOrder(orderID);
        }

        public void DeleteOrders()
        {
            persistence.DeleteOrders();
        }

        public IList<Order> ListAllOrdersFromMember(int memberID)
        {
            Member member = persistence.FindMember(memberID);

            IList<Order> result = (IList<Order>)persistence.FindOrder(memberID);
            return result;
        }

        public IList<Article> ListArticles()
        {
            IList<Article> result = persistence.FindArticles();
            return result;
        }

        public IList<Contract> ListContracts()
        {
           IList<Contract> result = persistence.FindContracts();
            return result;
        }

        public IList<Employee> ListEmployees()
        {
            IList<Employee> result = persistence.FindEmployees();
            return result;
        }

        public IList<Member> ListMembers()
        {
            IList<Member> result = persistence.FindMembers();
            return result;
        }

        public IList<Order> ListOrders()
        {
            IList<Order> result = persistence.FindOrders();
            return result;
        }

        public void UpdateContractFromMember(int memberID, int contractID)
        {
            persistence.FindMember(memberID).ContractID = contractID;
        }

        public void UpdateArticle(int articleID, int actualStock, int targetStock)
        {
            persistence.FindArticle(articleID).ActualStock = actualStock;
            persistence.FindArticle(articleID).TargetStock= targetStock;
        }

        public void AddArticleToOrder(int orderID, int articleID)
        {
            Order order = persistence.FindOrder(orderID);
            if(order!= null) 
                persistence.UpdateOrder(order, articleID);
        }

        #endregion


        # region IProductModule

        public IDictionary<Article, int> SearchArticle(string searchTerm)
        {
           IList<Article> articles = ListArticles();
            IDictionary<Article, int> result = new SortedList<Article, int>();

            foreach(Article a in articles)
            {
                if (a.ArticleName.IndexOf(searchTerm) != -1)
                    result.Add(a, GetArticlesTargetStock(a.TargetStock));
            }
            return result;
        }

        public IDictionary<Contract, int> SearchContract(string searchTerm)
        {
            IList<Contract> contracts = ListContracts();
            IDictionary<Contract, int> result = new SortedList<Contract, int>();

            foreach (Contract c in contracts)
            {
                if (c.ContractType.IndexOf(searchTerm) != -1)
                    result.Add(c, c.ContractID);
            }
            return result;
        }

        public IDictionary<Employee, int> SearchEmployee(string searchTerm)
        {
            IList<Employee> employees = ListEmployees();
            IDictionary<Employee, int> result = new SortedList<Employee, int>();

            foreach (Employee e in employees)
            {
                if (e.Surname.IndexOf(searchTerm) != -1)
                    result.Add(e, e.EmployeeID);
            }
            return result;
        }

        public IDictionary<Member, int> SearchMember(string searchTerm)
        {
            IList<Member> members = ListMembers();
            IDictionary<Member, int> result = new SortedList<Member, int>();

            foreach (Member m in members)
            {
                if (m.Surname.IndexOf(searchTerm) != -1)
                    result.Add(m, m.MemberID);
            }
            return result;
        }

        public IDictionary<Order, int> SearchOrder(string searchTerm)
        {
            IList<Order> orders = ListOrders();
            IDictionary<Order, int> result = new SortedList<Order, int>();

            foreach (Order o in orders)
            {
                if (o.Member.Surname.IndexOf(searchTerm) != -1)
                    result.Add(o, o.MemberID);
            }
            return result;
        }

        public Article GetArticleDetails(int articleID)
        {
           return persistence.FindArticle(articleID);
        }

        public int GetArticlesActualStock(int articleID)
        {
            return persistence.FindArticle(articleID).ActualStock;
        }

        public int GetArticlesTargetStock(int articleID)
        {
            return persistence.FindArticle(articleID).TargetStock;
        }

        public Contract GetContractDetails(int contractID)
        {
            return persistence.FindContract(contractID);
        }

        public Employee GetEmployeeDetails(int employeeID)
        {
           return persistence.FindEmployee(employeeID);
        }

        public Member GetMemberDetails(int memberID)
        {
           return persistence.FindMember(memberID);
        }

        public Order GetOrderDetails(int orderID)
        {
           return persistence.FindOrder(orderID);
        }
        
        #endregion

    }
}
