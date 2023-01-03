using loremipsum.Gym.Entities;

namespace loremipsum.Gym
{
    public interface IProductAdmin
    {
        //Member
        void AddMember(Member member);

        void DeleteMember(int memberID);

        IList<Member> ListMembers();

        void DeleteMembers();

        void UpdateMember(int memberID, string forename, string surname, string street, int postcalCode, string city, string country, string eMail, string iban, DateTime birthday);


        //Contract
        void UpdateContractFromMember(int memberid, int contractID);

        void AddContract(Contract contract);

        void DeleteContract(int contractID);

        IList<Contract> ListContracts();

        void DeleteContracts();

        void UpdateContract(int contractID, string contractType, TimeSpan duration, int price);


        //Employee
        void AddEmployee(Employee employee);

        void DeleteEmployee(int employeeID);

        IList<Employee> ListEmployees();

        void DeleteEmployees();

        void UpdateEmployee(int employeeID, string forename, string surname, string street, int postcalCode, string city, string country, string eMail, string iban, DateTime birthday, string status);


        //Article
        void AddArticle(Article article);

        void DeleteArticle(int articleID);

        IList<Article> ListArticles();

        void UpdateArticle(int articleID, string articleName, int price, int actualStock, int targetStock);

        void DeleteArticles();


        //Order
        void AddOrder(Order order);

        void DeleteOrder(int orderID);

        IList<Order> ListOrders();

        void DeleteOrders();

        void UpdateMemberFromOrder(int orderID, int memberID);

        void UpdateArticleToOrder(int orderID, int articleID, int amount);

        IList<Order> ListAllOrdersFromMember(int memberID);


        //LogIn
        void AddLogIn(LogIn logIn);

        void DeleteLogIn(string logInName);

        IList<LogIn> ListLogIns();

        void DeleteLogIns();

        void UpdateLogInName(string logInName, string newLogInName, string logInPassword);

        void UpdateLogInPassword(string logInName, string newLogInPassword);

        void UpdateLogInRank(string logInName, int rank);
    }
}
