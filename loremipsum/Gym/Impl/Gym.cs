using loremipsum.Gym.Entities;
using System.Diagnostics.Metrics;

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
        public Member AddMember(int contractID, string forename, string surname, string street, int postcalCode, string city, string country, string eMail, string iban, DateTime birthday)
        {
            Contract contract = persistence.FindContract(contractID);
            if (contract != null)
            {
                return persistence.CreateMember(contract, forename, surname, street, postcalCode, city, country, eMail, iban, birthday);
            }
            else { return null; }
            
        }

        public void DeleteMember(int memberID)
        {
            
            if (ListAllOrdersFromMember(memberID) == null || ListAllOrdersFromMember(memberID).Count == 0)
            {
                persistence.DeleteMember(memberID);//only delete member if the member has 0 orders
            }
        }

        public void DeleteMembers()
        {   
            if(ListOrders()==null || ListOrders().Count == 0)
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
            Contract contract = persistence.FindContract(contractID);
            Member member = persistence.FindMember(memberID);
            if (member != null)
            {
                persistence.UpdateContractFromMember(member, contract);//only update if they already exists
            }
        }

        public void UpdateMember(int memberID, string forename, string surname, string street, int postcalCode, string city, string country, string eMail, string iban, DateTime birthday)
        {
            Member member = persistence.FindMember(memberID);
            if(member != null)
            {
                persistence.UpdateMember(member, forename, surname, street, postcalCode, city, country, eMail, iban, birthday);
            }
        }



        //Contract
        public void AddContract(Contract contract)
        {
            persistence.CreateContract(contract);
        }

        public void DeleteContract(int contractID)
        {
            bool temp = true;
            IList<Member> result = persistence.FindMembers();
            foreach(Member c in result)
            {
                if(c.ContractID == contractID)
                {
                    temp = false;//if some member still has the contract dont delete the contract!
                }
            }
            if(temp == true)
            {
                persistence.DeleteContract(contractID);
            }
            
        }

        public void DeleteContracts()
        {
            IList<Member> result = persistence.FindMembers();
            if(result == null || result.Count == 0)
            {
                persistence.DeleteContracts();//only delete contracts if there are no members because members can only exists with contract
            }
            
        }

        public IList<Contract> ListContracts()
        {
            IList<Contract> result = persistence.FindContracts();
            return result;
        }

        public void UpdateContract(int contractID, string contractType, int price)
        {
            Contract contract = persistence.FindContract(contractID);
            if(contract != null)
            {
                persistence.UpdateContract(contract, contractType, price);
            }
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

        public void UpdateEmployee(int employeeID, string forename, string surname, string street, int postcalCode, string city, string country, string eMail, string iban, DateTime birthday)
        {
            Employee employee= persistence.FindEmployee(employeeID);
            if(employee != null)
            {
                persistence.UpdateEmployee(employee, forename, surname, street, postcalCode, city, country, eMail, iban, birthday);
            }
        }



        //Article
        public void AddArticle(Article article)
        {
           persistence.CreateArticle(article);
        }

        public void DeleteArticle(int articleID)
        {
            bool temp = true;
            IList<Order> result = persistence.FindOrders();
            foreach (Order c in result)
            {
                if (c.ArticleID == articleID)
                {
                    temp = false;//if some article still is in at least one order!
                }
            }
            if(temp == true)
            {
                persistence.DeleteArticle(articleID);
            }
        }

        public void DeleteArticles()
        {
            IList<Order> result = persistence.FindOrders();
            if (result == null || result.Count == 0)
            {
                persistence.DeleteArticles();//only delete all articles if there are no orders because orders can only exists with article
            }
        }

        public IList<Article> ListArticles()
        {
            IList<Article> result = persistence.FindArticles();
            return result;
        }

        public void UpdateArticle(int articleID, string articleName, int price, int actualStock, int targetStock)
        {
            Article article = persistence.FindArticle(articleID);
            if(article != null)
            {
                persistence.UpdateArticle(article, articleName, price, actualStock, targetStock);
            }
        }



        //Order
        public Order AddOrder(int memberID, int articleID, int amount)
        {
            //order still exists with the orderID, but never was saved
            Article a1 = persistence.FindArticle(articleID);
            Member m1 = persistence.FindMember(memberID);
            if (a1 != null && m1 != null)
            {
                if(a1.ActualStock>amount)
                {
                    return persistence.CreateOrder(m1, a1, amount);//only save the order if the article&member exists and the amount is lower than actualstock
                }
                else { return null;}
            }
            else { return null;}
        }

        public void DeleteOrder(int orderID)//return of articles --> mistake order --> article.actualamount increases
        {
            persistence.DeleteOrder(orderID);
        }

        public void DeleteOrders()//this just deletes all orders after checkout --> no mistake --> article.acutalamount stays same, but member.currentbill goes back to 0
        {
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
                //retun all orders of member can be none or many orders
                return result;
            }
            //no member exists
            return null;
        }

        public void UpdateOrder(int orderID, int memberID, int articleID, int amount)
        {
            Order order = persistence.FindOrder(orderID);
            Member member = persistence.FindMember(memberID);
            Article article = persistence.FindArticle(articleID);

            if (order != null && member != null && article != null)
            {
                if (article.ActualStock > amount)
                {
                    persistence.UpdateOrder(order, member, article, amount);
                }
            }
        }


        //LogIn
        public void AddLogIn(LogIn logIn)
        {
            persistence.CreateLogIn(logIn);
        }

        public void DeleteLogIn(string logInName)
        {
            persistence.DeleteLogIn(logInName);
        }

        public IList<LogIn> ListLogIns()
        {
            IList<LogIn> result = persistence.FindLogIns();
            return result;
        }

        public void DeleteLogIns()
        {
            persistence.DeleteLogIns();
        }

        public void UpdateLogIn(string logInName, string newLogInName, string newLogInPassword, int rank)
        {
            LogIn logIn = persistence.FindLogIn(logInName);
            LogIn logInNew = persistence.FindLogIn(newLogInName);

            if(logIn != null && logInNew != null)
            {
                persistence.UpdateLogIn(logIn, newLogInName, newLogInPassword, rank);
            }
            
        }


        #endregion


        # region IProductModule

        //Member
        public Member GetMemberDetails(int memberID)
        {
            return persistence.FindMember(memberID);
        }



        //Employee
        public Employee GetEmployeeDetails(int employeeID)
        {
            return persistence.FindEmployee(employeeID);
        }



        //Contract
        public Contract GetContractDetails(int contractID)
        {
            return persistence.FindContract(contractID);
        }



        //Article
        public Article GetArticleDetails(int articleID)
        {
            return persistence.FindArticle(articleID);
        }



        //Order
        public Order GetOrderDetails(int orderID)
        {
           return persistence.FindOrder(orderID);
        }



        //LogIn
        public LogIn GetLogInDetails(string logInName)
        {
            return persistence.FindLogIn(logInName);
        }

        #endregion

    }
}
