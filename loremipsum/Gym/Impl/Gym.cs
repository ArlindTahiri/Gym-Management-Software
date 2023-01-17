using loremipsum.Gym.Entities;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace loremipsum.Gym.Impl
{
    public class Gym : IProductAdmin, IProductModule
    {
        private readonly IGymPersistence persistence;
        private IList<int> currentlyTrainingMembersID = new List<int>();

        public Gym(IGymPersistence persistence)
        {
            this.persistence = persistence;
        }

        #region IProductAdmin

        #region Member
        /// <summary>
        /// AddMember checks if a contract with given contractID exists, because of the 1:n relation, if yes it will forward to the persistence. 
        /// </summary>
        /// <param name="contractID"></param>
        /// <param name="forename"></param>
        /// <param name="surname"></param>
        /// <param name="street"></param>
        /// <param name="postcalCode"></param>
        /// <param name="city"></param>
        /// <param name="country"></param>
        /// <param name="eMail"></param>
        /// <param name="iban"></param>
        /// <param name="birthday"></param>
        /// <returns>Either a member will be returned or null.</returns>
        public Member AddMember(int contractID, string forename, string surname, string street, int postcalCode, string city, string country, string eMail, string iban, DateTime birthday)
        {
            Contract contract = persistence.FindContract(contractID);
            if (contract != null)
            {
                return persistence.CreateMember(contract, forename, surname, street, postcalCode, city, country, eMail, iban, birthday);
            }
            else { return null; }
            
        }

        /// <summary>
        /// DeleteMember checks if a member with given memberID exisits and if said member isnt training right now, if true it will checkout all the orders of said member because of the 1:n relation.
        /// After this checkout of orders, this member has 0 orders. Now they will checkout the members contract. And after that the memberID will be forwared to persistence where said member will be deleted.
        /// </summary>
        /// <param name="memberID"></param>
        public bool DeleteMember(int memberID)
        {
            Member member = persistence.FindMember(memberID);
            bool temp = false;
            if(member != null && !ListTrainingMembersID().Contains(memberID))
            {
                
                if(CheckoutMemberForOrders(member) == true)
                {
                    if (ListAllOrdersFromMember(memberID) == null || ListAllOrdersFromMember(memberID).Count == 0)
                    {
                        if (CheckoutMemberForChangingContract(member) == true)
                        {
                            persistence.DeleteMember(memberID);//only delete member if the member has 0 orders
                            temp = true;
                        }
                    }  
                }
            }
            return temp;
            
        }

        /// <summary>
        /// DeleteMembers checkouts all members orders, because of 1:n relation, and contracts. After that it will check if there are any members currently training, if false then it will forward it to persistence to delete all members. 
        /// </summary>
        public bool DeleteMembers()
        {
            bool temp= false;
            if (CheckOutMembers() == true)
            {
                if (ListTrainingMembersID().Count == 0 && (ListOrders() == null || ListOrders().Count == 0))
                {
                    persistence.DeleteMembers();//only delete if there are no orders
                    temp = true;
                }
            }
            return temp;
            
        }

        /// <summary>
        /// ListMembers Forwards the query to persistence
        /// </summary>
        /// <returns>Returns a list of all the members.</returns>
        public IList<Member> ListMembers()
        {
            IList<Member> result = persistence.FindMembers();
            return result;
        }

        /// <summary>
        /// UpdateContractFromMember updates the contract from a member, which is a relation. It will check if the member and the new contract exist. After that the members contract will be checkouted. And then it will forward to the persitence.
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="contractID"></param>
        public bool UpdateContractFromMember(int memberID, int contractID)
        {
            Contract contract = persistence.FindContract(contractID);
            Member member = persistence.FindMember(memberID);
            bool temp = false;
            if (member != null && contract != null)
            {
                if (CheckoutMemberForChangingContract(member) == true)
                {
                    persistence.UpdateContractFromMember(member, contract);//only update if they already exists
                    temp = true;
                }
            }
            return temp;
        }

        /// <summary>
        /// UpdateMember updates the attributes, which don't have a relation, of a member. Therefor it just checks if such member exists, if true, it will forward it to persistence.
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="forename"></param>
        /// <param name="surname"></param>
        /// <param name="street"></param>
        /// <param name="postcalCode"></param>
        /// <param name="city"></param>
        /// <param name="country"></param>
        /// <param name="eMail"></param>
        /// <param name="iban"></param>
        /// <param name="birthday"></param>
        public void UpdateMember(int memberID, string forename, string surname, string street, int postcalCode, string city, string country, string eMail, string iban, DateTime birthday)
        {
            Member member = persistence.FindMember(memberID);
            if(member != null)
            {
                persistence.UpdateMember(member, forename, surname, street, postcalCode, city, country, eMail, iban, birthday);
            }
        }
        #endregion



        #region Contract
        /// <summary>
        /// AddContract will forward it to persistence to store that contract.
        /// </summary>
        /// <param name="contract"></param>
        public void AddContract(Contract contract)
        {
            persistence.CreateContract(contract);
        }

        /// <summary>
        /// DeleteContract checks if a Member has this contract, because of the 1:n relation, if noone has this contract, it will be forwarded to the persistence to delete this contract.
        /// </summary>
        /// <param name="contractID"></param>
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

        /// <summary>
        /// DeleteContracts checks if there are any members, because of the 1:n relation, if no member exists it will be forwarded to persistence where all contracts will be deleted.
        /// </summary>
        public void DeleteContracts()
        {
            IList<Member> result = persistence.FindMembers();
            if(result == null || result.Count == 0)
            {
                persistence.DeleteContracts();//only delete contracts if there are no members because members can only exists with contract
            }
            
        }

        /// <summary>
        /// ListContracts lists all contracts. Therefore it forward this query to the persistence.
        /// </summary>
        /// <returns>Returns a List with all Contracts</returns>
        public IList<Contract> ListContracts()
        {
            IList<Contract> result = persistence.FindContracts();
            return result;
        }

        /// <summary>
        /// UpdatContract updates the attributes, which don't have a relation, of the contract with given contractID. Therefore it will check if this contract exists, if true it will forward it to persistence to update the attributes.
        /// </summary>
        /// <param name="contractID"></param>
        /// <param name="contractType"></param>
        /// <param name="price"></param>
        public void UpdateContract(int contractID, string contractType, int price)
        {
            Contract contract = persistence.FindContract(contractID);
            if(contract != null)
            {
                persistence.UpdateContract(contract, contractType, price);
            }
        }
        #endregion



        #region Employee
        /// <summary>
        /// AddEmployee will forward it to persistence to store that Employee.
        /// </summary>
        /// <param name="employee"></param>
        public void AddEmployee(Employee employee)
        {
            persistence.CreateEmployee(employee);
        }

        /// <summary>
        /// DeleteEmployee forwards it to the persistence to delete this Employee.
        /// </summary>
        /// <param name="employeeID"></param>
        public void DeleteEmployee(int employeeID)
        {
            persistence.DeleteEmployee(employeeID);
        }

        /// <summary>
        /// DeleteEmployeeds forwards it to the persistence to delete all Employees.
        /// </summary>
        public void DeleteEmployees()
        {
            persistence.DeleteEmployees();
        }

        /// <summary>
        /// ListEmployees lists all Employees. Therefore it forward this query to the persistence.
        /// </summary>
        /// <returns>Returns a List with all Employees</returns>
        public IList<Employee> ListEmployees()
        {
            IList<Employee> result = persistence.FindEmployees();
            return result;
        }

        /// <summary>
        /// UpdateEmployee updates the attributes, which don't have a relation, of the Employee with given employeeeID. Therefore it will check if this Employee exists, if true it will forward it to persistence to update the attributes.
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="forename"></param>
        /// <param name="surname"></param>
        /// <param name="street"></param>
        /// <param name="postcalCode"></param>
        /// <param name="city"></param>
        /// <param name="country"></param>
        /// <param name="eMail"></param>
        /// <param name="iban"></param>
        /// <param name="birthday"></param>
        public void UpdateEmployee(int employeeID, string forename, string surname, string street, int postcalCode, string city, string country, string eMail, string iban, DateTime birthday)
        {
            Employee employee= persistence.FindEmployee(employeeID);
            if(employee != null)
            {
                persistence.UpdateEmployee(employee, forename, surname, street, postcalCode, city, country, eMail, iban, birthday);
            }
        }
        #endregion



        #region Article
        /// <summary>
        /// AddArticle will forward it to persistence to store that Article.
        /// </summary>
        /// <param name="article"></param>
        public void AddArticle(Article article)
        {
            if(article.ActualStock>=0 && article.TargetStock > 0)
            {
                persistence.CreateArticle(article);
            }
        }

        /// <summary>
        /// DeleteArticle checks if a order with this articleID exists, if false it will forward it to the persistence to delete this Article.
        /// </summary>
        /// <param name="articleID"></param>
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

        /// <summary>
        /// DeleteArticles checks if a order exists, if false it will forward it to the persistence to delete all Articles.
        /// </summary>
        public void DeleteArticles()
        {
            IList<Order> result = persistence.FindOrders();
            if (result == null || result.Count == 0)
            {
                persistence.DeleteArticles();//only delete all articles if there are no orders because orders can only exists with article
            }
        }

        /// <summary>
        /// ListArticles lists all Articles. Therefore it forward this query to the persistence.
        /// </summary>
        /// <returns>Returns a List with all Articles</returns>
        public IList<Article> ListArticles()
        {
            IList<Article> result = persistence.FindArticles();
            return result;
        }

        /// <summary>
        /// UpdateArticle updates the attributes, which don't have a relation, of the Article with given articleID. Therefore it will check if this Article exists, if true it will forward it to persistence to update the attributes.
        /// </summary>
        /// </summary>
        /// <param name="articleID"></param>
        /// <param name="articleName"></param>
        /// <param name="price"></param>
        /// <param name="actualStock"></param>
        /// <param name="targetStock"></param>
        public void UpdateArticle(int articleID, string articleName, int price, int actualStock, int targetStock)
        {
            Article article = persistence.FindArticle(articleID);
            if(article != null && actualStock>=0 && targetStock>0)
            {
                persistence.UpdateArticle(article, articleName, price, actualStock, targetStock);
            }
        }
        #endregion



        #region Order

        /// <summary>
        /// AddOrder checks if the given memberID and the given articleID exist, after that it will check if actualamount of this article is higher than the amount this order has. If true, it will be forwarded to persistence to create that order.
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="articleID"></param>
        /// <param name="amount"></param>
        /// <returns>Returns the created Order or null</returns>
        public Order AddOrder(int memberID, int articleID, int amount)
        {
            //order still exists with the orderID, but never was saved
            Article a1 = persistence.FindArticle(articleID);
            Member m1 = persistence.FindMember(memberID);
            if(amount>0)
            {
                if (a1 != null && m1 != null)
                {
                    if (a1.ActualStock >= amount)
                    {
                        return persistence.CreateOrder(m1, a1, amount);//only save the order if the article&member exists and the amount is lower than actualstock
                    }
                    else { return null; }
                }
                else { return null; }
            }
            else { return null; }
        }

        /// <summary>
        /// DeleteOrder deletes a specific order, where a mistake happened. It will forward it to persistence, where the acutalamount of the article of the order increases by the amount of the order and the member currentbill will be decreased by the price of the order. 
        /// </summary>
        /// <param name="orderID"></param>
        public void DeleteOrder(int orderID)//return of articles --> mistake order --> article.actualamount increases
        {
            persistence.DeleteOrder(orderID);
        }

        /// <summary>
        /// DeleteOrders deletes the all the orders for the checkout. It will forward it to persistence, where actualamount of the article stays the same and the currentbill of the members will be set to 0.
        /// </summary>
        public void DeleteOrders()//this just deletes all orders after checkout --> no mistake --> article.acutalamount stays same, but member.currentbill goes back to 0
        {
            persistence.DeleteOrders();
        }

        /// <summary>
        /// ListOrders lists all Orders. Therefore it forward this query to the persistence.
        /// </summary>
        /// <returns>Returns a List of all Orders</returns>
        public IList<Order> ListOrders()
        {
            IList<Order> result = persistence.FindOrders();
            return result;
        }

        /// <summary>
        /// ListOrders lists all Orders of a member. Therefore it will check if this member exists and then get all the orders and then check which orders are from given member.
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns>Returns a List of all the Orders of a Member or null if the member doesnt exists.</returns>
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

        /// <summary>
        /// Updates the attributes of Order with given orderID. Checks if orderID, memberID and articleID exists. If true it will check if the actualamount of the new article is smaller then the amount of the updated order.
        /// </summary>
        /// <param name="orderID">Order which should be updated</param>
        /// <param name="memberID">The new MemberID of the updated order</param>
        /// <param name="articleID">The new ArticleID of the updated order</param>
        /// <param name="amount">The new Amount of the updated order</param>
        public void UpdateOrder(int orderID, int memberID, int articleID, int amount)
        {
            Order order = persistence.FindOrder(orderID);
            Member member = persistence.FindMember(memberID);
            Article article = persistence.FindArticle(articleID);
            if(amount>0)
            {
                if (order != null && member != null && article != null)
                {
                    if (order.ArticleID == articleID)
                    {
                        if (article.ActualStock + order.Amount >= amount)
                        {
                            persistence.UpdateOrder(order, member, article, amount);
                        }
                    }
                    else
                    {
                        if (article.ActualStock >= amount)
                        {
                            persistence.UpdateOrder(order, member, article, amount);
                        }
                    }

                }
            }
        }
        #endregion



        #region LogIn
        /// <summary>
        /// Insert new login. Checks if given Login.LogInName already exists, if false then the LogIn will be forwarded to persistence to store.
        /// </summary>
        /// <param name="logIn"></param>
        public void AddLogIn(LogIn logIn)
        {
            if (persistence.FindLogIn(logIn.LogInName) == null)
            {
                persistence.CreateLogIn(logIn);
            }
        }

        /// <summary>
        /// Deletes LogIn with given logInName by forwarding it to persistence.
        /// </summary>
        /// <param name="logInName"></param>
        public void DeleteLogIn(string logInName)
        {
            LogIn temp = persistence.FindLogIn(logInName);
            if(temp != null)
            {
                IList<LogIn> logIns = persistence.FindLogIns();
                IList<string> result = new List<string>();
                foreach (LogIn login in logIns)
                {
                    if (login.Rank == 1) {result.Add(login.LogInName);}
                }
                if (result.Count > 1)
                {
                    persistence.DeleteLogIn(logInName);
                }
            }
        }

        /// <summary>
        /// Forwards this query to persistence.
        /// </summary>
        /// <returns>List of all LogIns</returns>
        public IList<LogIn> ListLogIns()
        {
            IList<LogIn> result = persistence.FindLogIns();
            return result;
        }

        /// <summary>
        /// Deletes all LogIns by forwarding it to persistence.
        /// </summary>
        public void DeleteLogIns()
        {
            persistence.DeleteLogIns();
        }

        /// <summary>
        /// Updates the attributs of a LogIn. Checks if the old logInName exists and if the new logInName doesnt exists, if true it will forward it to persistence to update the login.
        /// </summary>
        /// <param name="logInName"></param>
        /// <param name="newLogInName"></param>
        /// <param name="newLogInPassword"></param>
        /// <param name="rank"></param>
        public void UpdateLogIn(string logInName, string newLogInName, string newLogInPassword, int rank)
        {
            LogIn logIn = persistence.FindLogIn(logInName);
            LogIn logInNew = persistence.FindLogIn(newLogInName);
            IList<LogIn> logIns = persistence.FindLogIns();
            IList<LogIn> result= new List<LogIn>();
            foreach(LogIn login in logIns)
            {
                if (login.Rank == 1) { result.Add(login); }
            }

            if (result.Count > 1)
            {
                if (logIn != null && logInNew == null || logIn != null && logIn.LogInName == logInNew.LogInName)
                {
                    persistence.UpdateLogIn(logIn, newLogInName, newLogInPassword, rank);
                }
            }
            else
            {
                if (logIn != null && logInNew == null && rank == 1 || logIn != null && logIn.LogInName == logInNew.LogInName && rank == 1)
                {
                    persistence.UpdateLogIn(logIn, newLogInName, newLogInPassword, rank);
                }
            }

            
            
        }
        #endregion



        #region Checkout
        /// <summary>
        /// Checkouts the current contract of a member and stores it in a MemberBills.csv file for sending it to the bank to make the transactions.
        /// Therefore it will check if the currentMonth is the same as the stored Month in given member, which stores the last time when the member did the checkout or the last contractChange. If true, it will calculate the exact price which the member has to pay for the contract based on days of the month.
        /// </summary>
        /// <param name="member"></param>
        /// <returns>Returns true if successful otherwise false</returns>
        public bool CheckoutMemberForChangingContract(Member member)
        {
            Contract contract = persistence.FindContract(member.ContractID);

            DateTime currentDate = DateTime.Now.Date;
            int daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
            try
            {
                if (currentDate.Month == member.TimeOfContractChange.Month)//only let member change their Contract when we already did checkout.(check if TimeOfContractChange has the same month as todays month.)
                {
                    int price = (int)((double)currentDate.Day / daysInMonth * contract.Price);
                    string FileUrl = "MemberBills.csv";
                    if (!File.Exists(FileUrl))
                    {
                        using (StreamWriter sw = File.CreateText(FileUrl))
                        {
                            sw.WriteLine("Datum,MemberID,TransaktionsID,Preis,Transaktionsart,IBan,Anzahl");
                            sw.WriteLine(DateTime.Now.ToString() + "," + member.MemberID + "," + member.ContractID + "," + (double)price / 100 + "," + "Vertrag" + "," + member.Iban + ",");
                        }
                    }
                    else
                    {
                        using (StreamWriter sw = File.AppendText(FileUrl))
                        {
                            sw.WriteLine(DateTime.Now.ToString() + "," + member.MemberID + "," + member.ContractID + "," + (double)price / 100 + "," + "Vertrag" + "," + member.Iban + ",");
                        }
                    }

                    //set TimeOfContractChange to today
                    persistence.UpdateMemberTimeOfContractChange(member);
                    return true;
                }
                else { return false; }
            }
            catch (IOException) { return false; }
            
        }

        /// <summary>
        /// Checkouts all the orders and contracts of a member for the last month and saves them in a MemberBills.csv File. Therefor it calculates the right amount the member has to pay for the contract based on the time when the member had the last contractchange,
        /// started at the gym or the first date of the old month. After this contract checkout the attribut TimeofContractChange of all the members will be set to the first of the current month.
        /// After that all the orders will be checkouted and stored in the MemberBills.csv File. Then all the orders will be deleted.
        /// </summary>
        public bool CheckOutMembers()
        {
            IList<Member> members = persistence.FindMembers();
            IList<Order> orders = persistence.FindOrders();
            DateTime currentDate = DateTime.Now.Date;
            DateTime lastMonth = DateTime.Now.Date.AddMonths(-1);
            int daysInLastMonth = DateTime.DaysInMonth(lastMonth.Year, lastMonth.Month);
            string FileUrl = "MemberBills.csv";
            IList<Member> membersChangeTimeOfContractChange = new List<Member>();
            //now write all contracts in csv
            try
            {
                foreach (Member member in members)
                {
                    if (member.TimeOfContractChange.Month == lastMonth.Month)
                    {
                        int price = (int)((double)(daysInLastMonth - (member.TimeOfContractChange.Day - 1)) / daysInLastMonth * persistence.FindContract(member.ContractID).Price);
                        if (!File.Exists(FileUrl))
                        {
                            using (StreamWriter sw = File.CreateText(FileUrl))
                            {
                                sw.WriteLine("Datum,MemberID,TransaktionsID,Preis,Transaktionsart,IBan,Anzahl");
                                sw.WriteLine(DateTime.Now.ToString() + "," + member.MemberID + "," + member.ContractID + "," + price + "," + "Vertrag" + "," + member.Iban + ",");
                            }
                        }
                        else
                        {
                            using (StreamWriter sw = File.AppendText(FileUrl))
                            {
                                sw.WriteLine(DateTime.Now.ToString() + "," + member.MemberID + "," + member.ContractID + "," + price + "," + "Vertrag" + "," + member.Iban + ",");
                            }
                        }
                        membersChangeTimeOfContractChange.Add(member);
                    }


                    //done with writing all of the contracts in csv
                }
                //now set the TimeOfContractChange of each Member, which got checkouted to the first of the month
                persistence.UpdateMembersTimeOfContractChange(membersChangeTimeOfContractChange);

                //now write all orders in csv
                foreach (Order order in orders)
                {
                    using (StreamWriter sw = File.AppendText(FileUrl))
                    {
                        sw.WriteLine(DateTime.Now.ToString() + "," + order.MemberID + "," + order.OrderID + "," + persistence.FindArticle(order.ArticleID).Price * order.Amount + "," + "Bestellung" + "," + persistence.FindMember(order.MemberID).Iban + "," + order.Amount);
                    }
                }
                //done with writing all of the orders in csv
                //now set CurrentBill to right one --> 0 for every member
                DeleteOrders();
                return true;
            }
            catch (IOException) { return false; }
            
        }

        /// <summary>
        /// Checkouts all the orders of a Member. Therefor it will get all the orders of this member and then store them in the MemberBills.csv file. After that all the orders of this member will be removed.
        /// </summary>
        /// <param name="member"></param>
        public bool CheckoutMemberForOrders(Member member)
        {
            DateTime currentDate = DateTime.Now.Date;
            string FileUrl = "MemberBills.csv";
            IList<Order> ordersFromMember = ListAllOrdersFromMember(member.MemberID);

            try
            {
                //now write all orders in csv
                foreach (Order order in ordersFromMember)
                {
                    if (!File.Exists(FileUrl))
                    {
                        using (StreamWriter sw = File.CreateText(FileUrl))
                        {
                            sw.WriteLine("Datum,MemberID,TransaktionsID,Preis,Transaktionsart,IBan,Anzahl");
                            sw.WriteLine(DateTime.Now.ToString() + "," + order.MemberID + "," + order.OrderID + "," + persistence.FindArticle(order.ArticleID).Price * order.Amount + "," + "Bestellung" + "," + member.Iban + "," + order.Amount);
                        }
                    }
                    else
                    {
                        using (StreamWriter sw = File.AppendText(FileUrl))
                        {
                            sw.WriteLine(DateTime.Now.ToString() + "," + order.MemberID + "," + order.OrderID + "," + persistence.FindArticle(order.ArticleID).Price * order.Amount + "," + "Bestellung" + "," + member.Iban + "," + order.Amount);
                        }
                    }
                }
                persistence.DeleteOrders(ordersFromMember, member.MemberID);
                return true;
            }
            catch (IOException) { return false; }
            
        }
        #endregion



        #region CurrentlyTrainingMembers
        /// <summary>
        /// Inserts a MemberID in a CurrentTrainingMembers.xml file. Therefore it will check if the member exists only then it will insert the id.
        /// </summary>
        /// <param name="memberID"></param>
        public void InsertTrainingMember(int memberID)
        {
            Member member = persistence.FindMember(memberID);
            if (member != null)
            {
                currentlyTrainingMembersID = ListTrainingMembersID();
                currentlyTrainingMembersID.Add(member.MemberID);
                SaveTrainingMember();
            }
        }

        /// <summary>
        /// Deletes a MemberID out of a CurrentTrainingMembers.xml file. Therefore it will check if the member exists only then it will delete the id out of the file.
        /// </summary>
        /// <param name="memberID"></param>
        public void DeleteTrainingMember(int memberID)
        {
            Member member = persistence.FindMember(memberID);
            if (member != null)
            {
                currentlyTrainingMembersID = ListTrainingMembersID();
                currentlyTrainingMembersID.Remove(member.MemberID);
                SaveTrainingMember();
            }
        }

        /// <summary>
        /// Stores all the MemberIDs in the CurrentTrainingMembers.xml file.
        /// </summary>
        public void SaveTrainingMember()
        {
            string FileUrl = "CurrentTrainingMembers.xml";
            XmlSerializer ser = new XmlSerializer(typeof(int[]));
            using (Stream writer = File.Create(FileUrl))
            {
                ser.Serialize(writer, currentlyTrainingMembersID.ToArray());
            }
        }

        /// <summary>
        /// Returns a List of all the memberIDs, which are currently training
        /// </summary>
        /// <returns>Returns a List of all the memberIDs, which are currently training</returns>
        public IList<int> ListTrainingMembersID()
        {
            string FileUrl = "CurrentTrainingMembers.xml";
            int[] members;
            if (File.Exists(FileUrl))
            {
                XmlSerializer deser = new XmlSerializer(typeof(int[]));
                using (Stream reader = File.OpenRead(FileUrl))
                {
                    members = (int[])deser.Deserialize(reader);
                }
                currentlyTrainingMembersID = members.ToList();
                
            }
            return currentlyTrainingMembersID;
        }

        /// <summary>
        /// Returns a list of all the members, which are currently training. Therefore it will get all MemberIDs out of the File and then search in persistence for the member.
        /// </summary>
        /// <returns>Returns a list of all the members, which are currently training.</returns>
        public IList<Member> ListTrainingMembers()
        {
            currentlyTrainingMembersID = ListTrainingMembersID();
            IList<Member> result = new List<Member>();
            foreach (int memberID in currentlyTrainingMembersID)
            {
                result.Add(persistence.FindMember(memberID));
            }
            return result;
        }
        #endregion

        #endregion


        #region IProductModule

        //Member
        /// <summary>
        /// Returns a member if the memberID exists or null
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns>Returns a member if the memberID exists or null</returns>
        public Member GetMemberDetails(int memberID)
        {
            return persistence.FindMember(memberID);
        }



        //Employee
        /// <summary>
        /// Returns an Employee if the employeeID exists or null
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns>Returns an Employee if the employeeID exists or null</returns>
        public Employee GetEmployeeDetails(int employeeID)
        {
            return persistence.FindEmployee(employeeID);
        }



        //Contract
        /// <summary>
        /// Returns an Contract if the contractID exists or null
        /// </summary>
        /// <param name="contractID"></param>
        /// <returns>Returns an Contract if the contractID exists or null</returns>
        public Contract GetContractDetails(int contractID)
        {
            return persistence.FindContract(contractID);
        }



        //Article
        /// <summary>
        /// Returns an Article if the articleID exists or null
        /// </summary>
        /// <param name="articleID"></param>
        /// <returns>Returns an Article if the articleID exists or null</returns>
        public Article GetArticleDetails(int articleID)
        {
            return persistence.FindArticle(articleID);
        }



        //Order
        /// <summary>
        /// Returns an Order if the orderID exists or null
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns>Returns an Order if the orderID exists or null</returns>
        public Order GetOrderDetails(int orderID)
        {
           return persistence.FindOrder(orderID);
        }



        //LogIn
        /// <summary>
        /// Returns an LogIn if the logInName exists or null
        /// </summary>
        /// <param name="logInName"></param>
        /// <returns>Returns an LogIn if the logInName exists or null</returns>
        public LogIn GetLogInDetails(string logInName)
        {
            return persistence.FindLogIn(logInName);
        }

        #endregion

    }
}
