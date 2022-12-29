using loremipsum.Gym.Entities;

namespace loremipsum.Gym
{
    public interface IGymPersistence
    {
        //Member
        void UpdateContractFromMember(Member member, Contract contract);

        void CreateMember(Member member);

        void DeleteMember(int memberID);

        void DeleteMembers();

        Member FindMember(int memberID);

        IList<Member> FindMembers();

        void UpdateMember(Member member, string forename, string surname, string street, int postcalCode, string city, string country, string eMail, int iban, DateTime birthday);



        //Contract
        void CreateContract(Contract contract);

        void DeleteContract(int contractID);

        void DeleteContracts();

        Contract FindContract(int contractID);

        IList<Contract> FindContracts();

        void UpdateContract(Contract contract, string contractType, TimeSpan duration, int price);



        //Employee
        void CreateEmployee(Employee employee);

        void DeleteEmployee(int employeeID);

        void DeleteEmployees();

        Employee FindEmployee(int employeeID);

        IList<Employee> FindEmployees();

        void UpdateEmployee(Employee employee, string forename, string surname, string street, int postcalCode, string city, string country, string eMail, int iban, DateTime birthday, string status);



        //Article
        void CreateArticle(Article article);

        void DeleteArticle(int articleID);

        void DeleteArticles();

        Article FindArticle(int articleID);

        IList<Article> FindArticles();

        void UpdateArticle(Article article, string articleName, int price, int actualStock, int targetStock);



        //Order
        void CreateOrder(Order order);

        void DeleteOrder(int orderID);

        void DeleteOrders();

        Order FindOrder(int orderID);

        IList<Order> FindOrders();

        void UpdateMemberFromOrder(Order order, Member member);

        void UpdateOrder(Order order, Article article, int amount);



        //LogIn
        void CreateLogIn(LogIn logIn);

        void DeleteLogIn(string logInName);

        IList<LogIn> FindLogIns();

        LogIn FindLogIn(string logInName);

        void DeleteLogIns();

        void UpdateLogInName(LogIn logIn, string newLogInName, string logInPassword);

        void UpdateLogInPassword(LogIn logIn, string newLogInPassword);

        void UpdateLogInRank(LogIn logIn, int rank);
    }
}
