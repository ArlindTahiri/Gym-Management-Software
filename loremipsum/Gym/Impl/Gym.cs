using loremipsum.Gym.Entities;
using static System.Reflection.Metadata.BlobBuilder;
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
            Member member = persistence.FindMember(memberID);
            if(member != null && !ListTrainingMembersID().Contains(memberID))
            {
                CheckoutMemberForOrders(member);
                if (ListAllOrdersFromMember(memberID) == null || ListAllOrdersFromMember(memberID).Count == 0)
                {
                    if (CheckoutMemberForChangingContract(member) == true)
                    {
                        persistence.DeleteMember(memberID);//only delete member if the member has 0 orders
                    }
                }
            }
            
        }

        public void DeleteMembers()
        {
            CheckOutMembers();
            if(ListTrainingMembersID().Count==0 && (ListOrders() == null || ListOrders().Count == 0))
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
                if (CheckoutMemberForChangingContract(member) == true)
                {
                    persistence.UpdateContractFromMember(member, contract);//only update if they already exists
                }
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
        #endregion



        #region Contract
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
        #endregion



        #region Employee
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
        #endregion



        #region Article
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
        #endregion



        #region Order
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
        #endregion



        #region LogIn
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



        #region Checkout
        public bool CheckoutMemberForChangingContract(Member member)
        {
            Contract contract = persistence.FindContract(member.ContractID);

            DateTime currentDate = DateTime.Now.Date;
            int daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);

            if(currentDate.Month == member.TimeOfContractChange.Month)//only let member change their Contract when we already did checkout.(check if TimeOfContractChange has the same month as todays month.)
            {
                int price = (int)((double)currentDate.Day / daysInMonth * contract.Price);
                string FileUrl = "MemberBills.csv";
                if (!File.Exists(FileUrl))
                {
                    using (StreamWriter sw = File.CreateText(FileUrl))
                    {
                        sw.WriteLine("Datum,MemberID,TransaktionsID,Preis,Transaktionsart,IBan,Anzahl");
                        sw.WriteLine(currentDate.ToString() + "," + member.MemberID + "," + member.ContractID + "," + (double)price / 100 + "," + "Vertrag" + "," + member.Iban + ",");
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(FileUrl))
                    {
                        sw.WriteLine(currentDate.ToString() + "," + member.MemberID + "," + member.ContractID + "," + (double)price / 100 + "," + "Vertrag" + "," + member.Iban + ",");
                    }
                }

                //set TimeOfContractChange to today
                persistence.UpdateMemberTimeOfContractChange(member);
                return true;
            }
            else { return false; }
        }

        public void CheckOutMembers()
        {
            IList<Member> members = persistence.FindMembers();
            IList<Order> orders = persistence.FindOrders();
            DateTime currentDate = DateTime.Now.Date;
            DateTime lastMonth = DateTime.Now.Date.AddMonths(-1);
            int daysInLastMonth = DateTime.DaysInMonth(lastMonth.Year, lastMonth.Month);
            string FileUrl = "MemberBills.csv";
            //now write all contracts in csv
            foreach (Member member in members)
            {
                int price = (int)((double)(daysInLastMonth-(member.TimeOfContractChange.Day-1)) / daysInLastMonth * persistence.FindContract(member.ContractID).Price);
                if (!File.Exists(FileUrl))
                {
                    using (StreamWriter sw = File.CreateText(FileUrl))
                    {
                        sw.WriteLine("Datum,MemberID,TransaktionsID,Preis,Transaktionsart,IBan,Anzahl");
                        sw.WriteLine(currentDate.ToString() + "," + member.MemberID + "," + member.ContractID + "," + (double)price / 100 + "," + "Vertrag" + "," + member.Iban + ",");
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(FileUrl))
                    {
                        sw.WriteLine(currentDate.ToString() + "," + member.MemberID + "," + member.ContractID + "," + (double)price / 100 + "," + "Vertrag" + "," + member.Iban + ",");
                    }
                }

                //done with writing all of the contracts in csv
            }
            //now set the TimeOfContractChange of each Member of first of month
            persistence.UpdateMembersTimeOfContractChange();

            //now write all orders in csv
            foreach (Order order in orders)
            {
                using (StreamWriter sw = File.AppendText(FileUrl))
                {
                    sw.WriteLine(currentDate.ToString() + "," + order.MemberID + "," + order.OrderID + "," + persistence.FindArticle(order.ArticleID).Price * order.Amount + "," + "Bestellung" + "," + persistence.FindMember(order.MemberID).Iban + "," + order.Amount);
                }
            }
            //done with writing all of the orders in csv
            //now set CurrentBill to right one --> 0 for every member
            DeleteOrders();
        }

        public void CheckoutMemberForOrders(Member member)
        {
            DateTime currentDate = DateTime.Now.Date;
            string FileUrl = "MemberBills.csv";
            IList<Order> ordersFromMember = ListAllOrdersFromMember(member.MemberID);

            //now write all orders in csv
            foreach (Order order in ordersFromMember)
            {
                if (!File.Exists(FileUrl))
                {
                    using (StreamWriter sw = File.CreateText(FileUrl))
                    {
                        sw.WriteLine("Datum,MemberID,TransaktionsID,Preis,Transaktionsart,IBan,Anzahl");
                        sw.WriteLine(currentDate.ToString() + "," + order.MemberID + "," + order.OrderID + "," + persistence.FindArticle(order.ArticleID).Price * order.Amount + "," + "Bestellung" + "," + member.Iban + "," + order.Amount);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(FileUrl))
                    {
                        sw.WriteLine(currentDate.ToString() + "," + order.MemberID + "," + order.OrderID + "," + persistence.FindArticle(order.ArticleID).Price * order.Amount + "," + "Bestellung" + "," + member.Iban + "," + order.Amount);
                    }
                }

            }
        }
        #endregion



        #region CurrentlyTrainingMembers
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

        public void SaveTrainingMember()
        {
            string FileUrl = "CurrentTrainingMembers.xml";
            XmlSerializer ser = new XmlSerializer(typeof(int[]));
            using (Stream writer = File.Create(FileUrl))
            {
                ser.Serialize(writer, currentlyTrainingMembersID.ToArray());
            }
        }

        public IList<int> ListTrainingMembersID()
        {
            currentlyTrainingMembersID = new List<int>();
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
                return currentlyTrainingMembersID;
            }
            return new List<int>();
        }

        public IList<Member> ListTrainingMembers()
        {
            currentlyTrainingMembersID = ListTrainingMembersID();
            IList<Member> result = new List<Member>();
            foreach (int i in currentlyTrainingMembersID)
            {
                result.Add(persistence.FindMember(i));
            }
            return result;
        }
        #endregion

        #endregion


        #region IProductModule

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
