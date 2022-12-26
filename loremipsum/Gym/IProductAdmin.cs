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


        //Contract
        void UpdateContractFromMember(int memberid, int contractID);

        void AddContract(Contract contract);

        void DeleteContract(int contractID);

        IList<Contract> ListContracts();

        void DeleteContracts();


        //Employee
        void AddEmployee(Employee employee);

        void DeleteEmployee(int employeeID);

        IList<Employee> ListEmployees();

        void DeleteEmployees();


        //Article
        void AddArticle(Article article);

        void DeleteArticle(int articleID);

        IList<Article> ListArticles();

        void UpdateArticle(int articleID, int acutalStock, int targetStock );

        void DeleteArticles();


        //Order
        void AddOrder(Order order);

        void DeleteOrder(int orderID);

        IList<Order> ListOrders();

        void DeleteOrders();

        void UpdateMemberFromOrder(int orderID, int memberID);

        void UpdateArticleToOrder(int orderID, int articleID, int amount);

        IList<Order> ListAllOrdersFromMember(int memberID);


    }
}
