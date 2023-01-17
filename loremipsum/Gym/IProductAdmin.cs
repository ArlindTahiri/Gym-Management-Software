using loremipsum.Gym.Entities;

namespace loremipsum.Gym
{
    public interface IProductAdmin
    {
        //Member
        Member AddMember(int contractID, string forename, string surname, string street, int postcalCode, string city, string country, string eMail, string iban, DateTime birthday);

        bool DeleteMember(int memberID);

        IList<Member> ListMembers();

        bool DeleteMembers();

        void UpdateMember(int memberID, string forename, string surname, string street, int postcalCode, string city, string country, string eMail, string iban, DateTime birthday);

        bool UpdateContractFromMember(int memberID, int contractID);

        //Contract
        void AddContract(Contract contract);

        void DeleteContract(int contractID);

        IList<Contract> ListContracts();

        void DeleteContracts();

        void UpdateContract(int contractID, string contractType, int price);


        //Employee
        void AddEmployee(Employee employee);

        void DeleteEmployee(int employeeID);

        IList<Employee> ListEmployees();

        void DeleteEmployees();

        void UpdateEmployee(int employeeID, string forename, string surname, string street, int postcalCode, string city, string country, string eMail, string iban, DateTime birthday);


        //Article
        void AddArticle(Article article);

        void DeleteArticle(int articleID);

        IList<Article> ListArticles();

        void UpdateArticle(int articleID, string articleName, int price, int actualStock, int targetStock);

        void DeleteArticles();


        //Order
        Order AddOrder(int memberID, int articleID, int amount);

        void DeleteOrder(int orderID);

        IList<Order> ListOrders();

        void DeleteOrders();

        void UpdateOrder(int orderID, int memberID, int articleID, int amount);

        IList<Order> ListAllOrdersFromMember(int memberID);


        //LogIn
        void AddLogIn(LogIn logIn);

        void DeleteLogIn(string logInName);

        IList<LogIn> ListLogIns();

        void DeleteLogIns();

        void UpdateLogIn(string logInName, string newLogInName, string newlogInPassword, int rank);


        //Checkout
        bool CheckoutMemberForChangingContract(Member member);

        bool CheckOutMembers();

        bool CheckoutMemberForOrders(Member member);

        //CurrentlyTrainingMembers
        void InsertTrainingMember(int memberID);

        void DeleteTrainingMember(int memberID);

        IList<Member> ListTrainingMembers();

        IList<int> ListTrainingMembersID();
    }
}
