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

        //Member
        public void AddMember(Member member)
        {
            Contract contract = persistence.FindContract(member.ContractID);
            if (contract != null)
            {
                persistence.CreateMember(member);
            }
        }

        public void DeleteMember(int memberID)
        {
            if (ListAllOrdersFromMember(memberID) == null)
            {
                persistence.DeleteMember(memberID);//only delete member if the member has 0 orders
            }
        }

        public void DeleteMembers()
        {   
            if(ListOrders()==null)
            {
                persistence.DeleteMembers();//only delete if there are no orders
            }
        }

        public IList<Member> ListMembers()
        {
            IList<Member> result = persistence.FindMembers();
            return result;
        }

        public void UpdateContractFromMember(int memberID, int contractID)
        {
            Member member = persistence.FindMember(memberID);
            Contract contract = persistence.FindContract(contractID);
            if (member != null & contract != null)
            {
                persistence.UpdateContractFromMember(member, contract);//only update if they already exists
            }

        }



        //Contract
        public void AddContract(Contract contract)
        {
            persistence.CreateContract(contract);
        }

        public void DeleteContract(int contractID)
        {
            IList<Member> result = persistence.FindMembers();
            foreach(Member c in result)
            {
                if(c.ContractID == contractID)
                {
                    break;//if some member still has the contract dont delete the contract!
                }
            }
            persistence.DeleteContract(contractID);
        }

        public void DeleteContracts()
        {
            IList<Member> result = persistence.FindMembers();
            if(result == null)
            {
                persistence.DeleteContracts();//only delete contracts if there are no members because members can only exists with contract
            }
            
        }

        public IList<Contract> ListContracts()
        {
            IList<Contract> result = persistence.FindContracts();
            return result;
        }



        //Employee
        public void AddEmployee(Employee employee)
        {
            persistence.CreateEmployee(employee);
        }

        public void DeleteEmployee(int employeeID)
        {
            persistence.DeleteEmployee(employeeID);
        }

        public void DeleteEmployees()
        {
            persistence.DeleteEmployees();
        }

        public IList<Employee> ListEmployees()
        {
            IList<Employee> result = persistence.FindEmployees();
            return result;
        }



        //Article
        public void AddArticle(Article article)
        {
           persistence.CreateArticle(article);
        }

        public void DeleteArticle(int articleID)
        {
            IList<Order> result = persistence.FindOrders();
            foreach (Order c in result)
            {
                if (c.ArticleID == articleID)
                {
                    break;//if some article still is in at least one order!
                }
            }
            persistence.DeleteArticle(articleID);
        }

        public void DeleteArticles()
        {
            IList<Order> result = persistence.FindOrders();
            if (result == null)
            {
                persistence.DeleteArticles();//only delete all articles if there are no orders because orders can only exists with article
            }
        }

        public IList<Article> ListArticles()
        {
            IList<Article> result = persistence.FindArticles();
            return result;
        }

        public void UpdateArticle(int articleID, int actualStock, int targetStock)
        {
            Article article = persistence.FindArticle(articleID);
            if(article != null)
            {
                persistence.UpdateArticle(article, actualStock, targetStock);
            }
        }



        //Order
        public void AddOrder(Order order)
        {
            //order still exists with the orderID, but never was saved
            Article a1 = persistence.FindArticle(order.ArticleID);
            Member m1 = persistence.FindMember(order.MemberID);
            if (a1 != null & m1 != null)
            {
                if(a1.ActualStock>order.Amount)
                {
                    persistence.CreateOrder(order);//only save the order if the article&member exists and the amount is lower than actualstock
                }
            }
        }

        public void DeleteOrder(int orderID)//mistake order--> money back to member
        {
            Order order = persistence.FindOrder(orderID);
            Article originalArticle = persistence.FindArticle(order.ArticleID);
            Member member = persistence.FindMember(order.MemberID);
            if (originalArticle != null & order != null & member != null)//can only be deleted if article still exists and member still exists
            {
                persistence.DeleteOrder(orderID);
            }

        }

        public void DeleteOrders()//this delete just deletes all orders to have a cleaner database --> no mistake --> no money back for members
        {
            /* if the articles need to be returned then:
            IList<Order> orders = persistence.FindOrders();
            IList<Article> articles = persistence.FindArticles();

              
            if(orders.Select(i => i.ArticleID).Intersect(articles.Select(b => b.ArticleID)) == null)
            {
                persistence.DeleteOrders();// check if all the articles of the orders still exist in articles table and only then delete
            }
            */
            persistence.DeleteOrders();
        }

        public IList<Order> ListOrders()
        {
            IList<Order> result = persistence.FindOrders();
            return result;
        }

        public IList<Order> ListAllOrdersFromMember(int memberID)
        {
            Member member = persistence.FindMember(memberID);
            if(member != null)
            {
                IList<Order> temp = persistence.FindOrders();
                IList<Order> result = new List<Order>();
                foreach (Order order in temp)
                {
                    if(order.MemberID == memberID)
                    {
                        result.Add(order);
                    }
                }
                //retun all orders of member
                return result;
            }
            //no member exists
            return null;
        }

        public void UpdateMemberFromOrder(int orderID, int memberID)
        {
            Order order = persistence.FindOrder(orderID);
            Member member = persistence.FindMember(memberID);

            if (order != null & member != null) 
                persistence.UpdateMemberFromOrder(order, member);
        }

        public void UpdateArticleToOrder(int orderID, int articleID, int amount)
        {
            Order order = persistence.FindOrder(orderID);
            Article newArticle = persistence.FindArticle(articleID);
            Article originalArticle = persistence.FindArticle(order.ArticleID);//if orginal article doesnt exists anymore dont go on.
            //no need for comparision if article new and original are the same, because it will handle it right
            if (order != null & newArticle != null & originalArticle != null)
            {
                if(newArticle.ActualStock>amount)
                {
                    persistence.UpdateOrder(order, newArticle, amount);//only go further if article doesnt
                }
                
            }
                
        }

        #endregion


        # region IProductModule

        //Member
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

        public Member GetMemberDetails(int memberID)
        {
            return persistence.FindMember(memberID);
        }



        //Employee
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

        public Employee GetEmployeeDetails(int employeeID)
        {
            return persistence.FindEmployee(employeeID);
        }



        //Contract
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

        public Contract GetContractDetails(int contractID)
        {
            return persistence.FindContract(contractID);
        }



        //Article
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

        public Article GetArticleDetails(int articleID)
        {
            return persistence.FindArticle(articleID);
        }

        public int GetArticlesActualStock(int articleID)
        {
            Article article = persistence.FindArticle(articleID);
            if (article == null) { return 0; }
            else { return article.ActualStock; } 
        }

        public int GetArticlesTargetStock(int articleID)
        {
            Article article = persistence.FindArticle(articleID);
            if (article == null) { return 0; }
            else { return article.TargetStock; }
        }



        //Order
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

        public Order GetOrderDetails(int orderID)
        {
           return persistence.FindOrder(orderID);
        }
        
        #endregion

    }
}
