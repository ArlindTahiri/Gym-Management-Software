using loremipsum.Gym.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace loremipsum.Gym.Persistence
{
    public class GymPersistenceEF : IGymPersistence
    {

        #region Member
        /// <summary>
        /// Stores a member if there isn't a member with the same values.
        /// </summary>
        /// <param name="contract"></param>
        /// <param name="forename"></param>
        /// <param name="surname"></param>
        /// <param name="street"></param>
        /// <param name="postcalCode"></param>
        /// <param name="city"></param>
        /// <param name="country"></param>
        /// <param name="eMail"></param>
        /// <param name="iban"></param>
        /// <param name="birthday"></param>
        /// <returns>The created Member or null if there is already a member</returns>
        public Member CreateMember(Contract contract, string forename, string surname, string street, int postcalCode, string city, string country, string eMail, string iban, DateTime birthday)
        {
            using (GymContext db = new GymContext())
            {
                Member member = new Member(forename, surname, street, postcalCode, city, country, eMail, iban, birthday, contract.ContractID);
                Contract c = db.Contracts
                    .Where(b => b.ContractID == contract.ContractID)
                    .Include(b => b.Members)
                    .FirstOrDefault();

                IList<Member> members = FindMembers();
                foreach(Member m in members)
                {
                    if (m.CompareTo(member) == 0) { return null; }
                }

                if(c != null)
                {
                    member.TimeOfContractChange = DateTime.Now.Date;
                    db.Members.Add(member); //1:n relation
                    db.SaveChanges();
                    return member;
                }
                else { return null;}
            }
        }

        /// <summary>
        /// Deletes the member with given memberID, if the member exists.
        /// </summary>
        /// <param name="memberID"></param>
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

        /// <summary>
        /// Deletes all the members
        /// </summary>
        public void DeleteMembers()
        {
            using (GymContext db = new GymContext())
            {
                IList<Member> members = FindMembers();
                db.Members.RemoveRange(members);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Seaches the member.
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns>A Member or null if the database can't find the member</returns>
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

        /// <summary>
        /// Lists all the members.
        /// </summary>
        /// <returns>Return a list with all the members. The List can be empty.</returns>
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

        /// <summary>
        /// Updates the contract of a Member. And sets the attriubut TimeOfContractChange of this member to today.
        /// </summary>
        /// <param name="member"></param>
        /// <param name="contract"></param>
        public void UpdateContractFromMember(Member member, Contract contract)
        {
            using (GymContext db = new GymContext())
            {
                Member m = db.Members
                    .Where(b => b.MemberID == member.MemberID)
                    .Include(b => b.Orders)
                    .FirstOrDefault();
                
                m.TimeOfContractChange = DateTime.Now.Date;
                m.Contract = contract;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Updates the attributes of the member.
        /// </summary>
        /// <param name="member"></param>
        /// <param name="forename"></param>
        /// <param name="surname"></param>
        /// <param name="street"></param>
        /// <param name="postcalCode"></param>
        /// <param name="city"></param>
        /// <param name="country"></param>
        /// <param name="eMail"></param>
        /// <param name="iban"></param>
        /// <param name="birthday"></param>
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
        #endregion



        #region Contract
        /// <summary>
        /// Creates a contract only if the contract doesn't exist already.
        /// </summary>
        /// <param name="contract"></param>
        public void CreateContract(Contract contract)
        {
            using (GymContext db = new GymContext())
            {
                bool temp = true;
                IList<Contract> contracts = FindContracts();
                foreach (Contract c in contracts)
                {
                    if (c.CompareTo(contract) == 0) { temp=false; }
                }
                if (temp == true)
                {
                    db.Contracts.Add(contract);
                    db.SaveChanges();
                }
                
            }
        }

        /// <summary>
        /// Deletes a contract only if the contract with given contractID can be found.
        /// </summary>
        /// <param name="contractID"></param>
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

        /// <summary>
        /// Deletes all the Contract.
        /// </summary>
        public void DeleteContracts()
        {
            using (GymContext db = new GymContext())
            {
                IList<Contract> contracts = FindContracts();
                db.Contracts.RemoveRange(contracts);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Returns a contract or null if the contract with given contractID can't be found
        /// </summary>
        /// <param name="contractID"></param>
        /// <returns>Returns a contract or null</returns>
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

        /// <summary>
        /// Renturns a List of all the Contracts. This list can be empty, if no contracts exists.
        /// </summary>
        /// <returns>Renturns a List of all the Contracts. This list can be empty, if no contracts exists.</returns>
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

        /// <summary>
        /// Updates the contracts attributes.
        /// </summary>
        /// <param name="contract"></param>
        /// <param name="contractType"></param>
        /// <param name="price"></param>
        public void UpdateContract(Contract contract, string contractType, int price)
        {
            using (GymContext db = new GymContext())
            {
                Contract c = db.Contracts
                    .Where(b => b.ContractID == contract.ContractID)
                    .Include(b => b.Members)
                    .FirstOrDefault();
                
                c.ContractType = contractType;
                c.Price = price;

                db.SaveChanges();

            }
        }
        #endregion



        #region Employee
        /// <summary>
        /// Creates an Employee only if there doesnt already exist the same employee.
        /// </summary>
        /// <param name="employee"></param>
        public void CreateEmployee(Employee employee)
        {
            using (GymContext db = new GymContext())
            {
                bool temp = true;
                IList<Employee> employees = FindEmployees();
                foreach (Employee e in employees)
                {
                    if (e.CompareTo(employee) == 0) { temp = false; }
                }
                if (temp == true)
                {
                    db.Employees.Add(employee);
                    db.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Deletes an Employee with given EmployeeID if the employee exist.
        /// </summary>
        /// <param name="employeeID"></param>
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

        /// <summary>
        /// Deletes all the Employees.
        /// </summary>
        public void DeleteEmployees()
        {
            using (GymContext db = new GymContext())
            {
                IList<Employee> employees = FindEmployees();
                db.Employees.RemoveRange(employees);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Returns an Employeee with given employeeID. Can be null if not found.
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns>Returns an Employeee or null</returns>
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

        /// <summary>
        /// Returns a List of all the Employees. This list can be empty.
        /// </summary>
        /// <returns>Returns a List of all the Employees. This list can be empty.</returns>
        public IList<Employee> FindEmployees()
        {
            using (GymContext db = new GymContext())
            {
                IEnumerable<Employee> employees = db.Employees
                    .ToList();

                return (List<Employee>)employees;
            }
        }

        /// <summary>
        /// Updates the attributes of the employee.
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="forename"></param>
        /// <param name="surname"></param>
        /// <param name="street"></param>
        /// <param name="postcalCode"></param>
        /// <param name="city"></param>
        /// <param name="country"></param>
        /// <param name="eMail"></param>
        /// <param name="iban"></param>
        /// <param name="birthday"></param>
        public void UpdateEmployee(Employee employee, string forename, string surname, string street, int postcalCode, string city, string country, string eMail, string iban, DateTime birthday)
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
                
                db.SaveChanges();
            }
        }
        #endregion



        #region Article
        /// <summary>
        /// Creates an Article if there doesn't already exist the article with the same attributes.
        /// </summary>
        /// <param name="article"></param>
        public void CreateArticle(Article article)
        {
            using (GymContext db = new GymContext())
            {
                bool temp = true;
                IList<Article> articles = FindArticles();
                foreach (Article a in articles)
                {
                    if (a.CompareTo(article) == 0) { temp = false; }
                }
                if (temp == true)
                {
                    db.Articles.Add(article);
                    db.SaveChanges();
                }
                
            }
        }

        /// <summary>
        /// Deletes an Article with given articleID, if it exists.
        /// </summary>
        /// <param name="articleID"></param>
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

        /// <summary>
        /// Deletes all the Articles.
        /// </summary>
        public void DeleteArticles()
        {
            using (GymContext db = new GymContext())
            {
                IList<Article> articles = FindArticles();
                db.Articles.RemoveRange(articles);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Returns an Article with given articleID or null, if there doesn't exist this articleID.
        /// </summary>
        /// <param name="articleID"></param>
        /// <returns>Returns an Article with given articleID or null, if there doesn't exist this articleID.</returns>
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

        /// <summary>
        /// Returns a List with all the Articles. This list can be empty.
        /// </summary>
        /// <returns>Returns a List with all the Articles. This list can be empty.</returns>
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

        /// <summary>
        /// Updates the attributes of the article
        /// </summary>
        /// <param name="article"></param>
        /// <param name="articleName"></param>
        /// <param name="price"></param>
        /// <param name="actualStock"></param>
        /// <param name="targetStock"></param>
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
        #endregion



        #region Order
        /// <summary>
        /// Creates an Order with an article and the amount of such article for the member a. Therefor the actualstock of the article decreases by the amount of the order. The currentbill of the member increases by the price of the order.
        /// </summary>
        /// <param name="member"></param>
        /// <param name="article"></param>
        /// <param name="amount"></param>
        /// <returns>Returns the created Order</returns>
        public Order CreateOrder(Member member, Article article, int amount)
        {
            using (GymContext db = new GymContext())
            {
                Order order = new Order(member.MemberID, article.ArticleID, amount);

                Article a = db.Articles
                    .Where(b => b.ArticleID == article.ArticleID)
                    .Include(b => b.Orders)
                    .FirstOrDefault();

                Member m = db.Members
                    .Where(b => b.MemberID == member.MemberID)
                    .Include(b => b.Orders)
                    .FirstOrDefault();
                
                db.Orders.Add(order);
                a.ActualStock = a.ActualStock - amount;
                m.CurrentBill = m.CurrentBill + amount * a.Price;
                db.SaveChanges();

                return order;
            }
        }

        /// <summary>
        /// Deletes an Order, which a mistake happened. Therefore checks if the order exists, if true the articles of the order are being returned and the bill of the member decreases by the price of the order.
        /// </summary>
        /// <param name="orderID"></param>
        public void DeleteOrder(int orderID) //return of articles --> mistake order --> article.actualamount increases and currentbill of member decreased by amount of order
        {
            using (GymContext db = new GymContext())
            {
                Order order = db.Orders
                    .Where(b => b.OrderID == orderID)
                    .FirstOrDefault();

                if(order != null)
                {
                    Article article = db.Articles
                        .Where(b => b.ArticleID == order.ArticleID)
                        .Include(b => b.Orders)
                        .FirstOrDefault();

                    Member member = db.Members
                        .Where(b => b.MemberID == order.MemberID)
                        .Include(b => b.Orders)
                        .FirstOrDefault();

                    article.ActualStock = article.ActualStock + order.Amount;
                    member.CurrentBill = member.CurrentBill - order.Amount * article.Price;

                    db.Orders.Remove(order);
                    db.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Deletes all the orders for the database. So the currentbill of the members will be set to 0.
        /// </summary>
        public void DeleteOrders()//this just deletes all orders after checkout --> no mistake --> article.acutalamount stays same, but member.currentbill goes back to 0
        {
            using (GymContext db = new GymContext())
            {
                IList<Order> orders = FindOrders();
                IList<Member> members = FindMembers();
                foreach (Member member in members)
                {
                    Member memberEF = db.Members
                        .Where(b => b.MemberID == member.MemberID)
                        .Include(b => b.Orders)
                        .FirstOrDefault();

                    member.CurrentBill = 0;
                    db.SaveChanges();
                }
                db.Orders.RemoveRange(orders);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes all the orders of a member with given memberID. Currentbill will be set to 0;
        /// </summary>
        /// <param name="ordersofMember"></param>
        /// <param name="memberID"></param>
        public void DeleteOrders(IList<Order> ordersofMember, int memberID)
        {
            using (GymContext db = new GymContext())
            {
                Member memberEF = db.Members
                        .Where(b => b.MemberID == memberID)
                        .Include(b => b.Orders)
                        .FirstOrDefault();
                memberEF.CurrentBill = 0;

                db.Orders.RemoveRange(ordersofMember);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Returns an order with given orderID or null if it can't be found.
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns>Returns an order with given orderID or null if it can't be found.</returns>
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

        /// <summary>
        /// Returns a list of all the orders. This list can be empty.
        /// </summary>
        /// <returns>Returns a list of all the orders. This list can be empty.</returns>
        public IList<Order> FindOrders()
        {
            using (GymContext db = new GymContext())
            {
                IEnumerable<Order> orders= db.Orders
                    .ToList();
                return (List<Order>) orders;
            }
        }

        /// <summary>
        /// Updates an order. Therefore the old articles are returned and the currentbill of the old member will corrected by deducting the price of the order and the actualamount of the old article will be increased by the amount of the old order..
        /// The actualamount of the newarticle is being deducted by the new amount. The currentbill of the new member increases by the price of the new order. 
        /// </summary>
        /// <param name="order"></param>
        /// <param name="passedMember"></param>
        /// <param name="passedArticle"></param>
        /// <param name="amount"></param>
        public void UpdateOrder(Order order, Member passedMember, Article passedArticle, int amount)
        {
            using (GymContext db = new GymContext())
            {
                Article oldArticle = db.Articles
                    .Where(b => b.ArticleID == order.ArticleID)
                    .Include(b => b.Orders)
                    .FirstOrDefault();

                Article newArticle = db.Articles
                    .Where(b => b.ArticleID == passedArticle.ArticleID)
                    .Include(b => b.Orders)
                    .FirstOrDefault();

                oldArticle.ActualStock = oldArticle.ActualStock + order.Amount;
                db.SaveChanges();
                newArticle.ActualStock = newArticle.ActualStock - amount;



                Member oldMember = db.Members
                    .Where(b => b.MemberID == order.MemberID)
                    .Include(b => b.Orders)
                    .FirstOrDefault();

                Member newMember = db.Members
                    .Where(b => b.MemberID == passedMember.MemberID)
                    .Include(b => b.Orders)
                    .FirstOrDefault();

                oldMember.CurrentBill = oldMember.CurrentBill - oldArticle.Price * order.Amount;
                db.SaveChanges();
                newMember.CurrentBill = newMember.CurrentBill + newArticle.Price * amount;



                Order o = db.Orders
                    .Where(b => b.OrderID == order.OrderID)
                    .FirstOrDefault();

                o.ArticleID=newArticle.ArticleID;
                o.MemberID=newMember.MemberID;
                o.Amount=amount;

                db.SaveChanges();
            }
        }
        #endregion



        #region LogIn
        /// <summary>
        /// Creates an LogIn.
        /// </summary>
        /// <param name="logIn"></param>
        public void CreateLogIn(LogIn logIn)
        {
            using (GymContext db = new GymContext())
            {
                db.LogIns.Add(logIn);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes a login. Therefore checks if the login exists.
        /// </summary>
        /// <param name="logInName"></param>
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

        /// <summary>
        /// Returns a List of all the LogIns. This list can by empty.
        /// </summary>
        /// <returns>Returns a List of all the LogIns. This list can by empty.</returns>
        public IList<LogIn> FindLogIns()
        {
            using (GymContext db = new GymContext())
            {
                IEnumerable<LogIn> logIns = db.LogIns
                    .ToList();

                return (List<LogIn>)logIns;
            }
        }

        /// <summary>
        /// Returns a LogIn or null, if the loginName doesnt exists.
        /// </summary>
        /// <param name="logInName"></param>
        /// <returns>Returns a LogIn or null, if the loginName doesnt exists.</returns>
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

        /// <summary>
        /// Deletes all the Logins.
        /// </summary>
        public void DeleteLogIns()
        {
            using (GymContext db = new GymContext())
            {
                IList<LogIn> logIns = FindLogIns();
                db.LogIns.RemoveRange(logIns);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Updates the key(loginName) and password and rank of the login.
        /// </summary>
        /// <param name="logIn"></param>
        /// <param name="newLogInName"></param>
        /// <param name="newLogInPassword"></param>
        /// <param name="rank"></param>
        public void UpdateLogIn(LogIn logIn, string newLogInName, string newLogInPassword, int rank)
        {
            using (GymContext db = new GymContext())
            {
                LogIn oldLogIn = db.LogIns
                    .Where(b => b.LogInName == logIn.LogInName)
                    .FirstOrDefault();

                db.LogIns.Remove(oldLogIn);
                db.SaveChanges();

                LogIn newLogIn = new LogIn(newLogInName, newLogInPassword, rank);
                db.LogIns.Add(newLogIn);
                db.SaveChanges();
            }
        }
        #endregion



        #region Checkout
        /// <summary>
        /// Updates the attribut TimeOfContractChange of every member to the first day of the month.
        /// </summary>
        public void UpdateMembersTimeOfContractChange()
        {
            IList<Member> members = FindMembers();
            DateTime firstDayOfCurrentMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).Date;

            using (GymContext db = new GymContext())
            {
                foreach (Member member in members)
                {
                    Member memberEF = db.Members
                    .Where(b => b.MemberID == member.MemberID)
                    .Include(b => b.Orders)
                    .FirstOrDefault();

                    if (memberEF != null)
                    {
                        memberEF.TimeOfContractChange = firstDayOfCurrentMonth;
                        db.SaveChanges();
                    }
                }
            }
        }

        /// <summary>
        /// Updates the TimeOfContractChange of a specific member to today. This is being used when changing a contract of a member.
        /// </summary>
        /// <param name="member"></param>
        public void UpdateMemberTimeOfContractChange(Member member)
        {
            using (GymContext db = new GymContext())
            {
                Member memberEF = db.Members
                    .Where(b => b.MemberID == member.MemberID)
                    .Include(b => b.Orders)
                    .FirstOrDefault();

                if (memberEF != null)
                {
                    memberEF.TimeOfContractChange = DateTime.Now.Date;
                    db.SaveChanges();
                }
            }
        }
        #endregion

    }
}