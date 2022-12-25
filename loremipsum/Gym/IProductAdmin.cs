using loremipsum.Gym.Entities;

namespace loremipsum.Gym
{
    public interface IProductAdmin
    {
        void AddMember(Member member, int contractID);

        void DeleteMember(int memberID);

        IList<Member> ListMembers();

        void DeleteMembers();

        void UpdateContractFromMember(int memberid, int contractID);

        void AddContract(Contract contract);

        void DeleteContract(int contractID);

        IList<Contract> ListContracts();

        void DeleteContracts();

        void AddEmployee(Employee employee);

        void DeleteEmployee(int employeeID);

        IList<Employee> ListEmployees();

        void DeleteEmployees();

        void AddArticle(Article article);

        void RemoveArticle(int articleID, int memberID);

        IList<Article> ListArticles();

        void UpdateArticle(int articleID, int acutalStock, int targetStock );

        void DeleteArticles();

        void AddOrder(Order order);

        void DeleteOrder(int orderID);

        IList<Order> ListOrders();

        void DeleteOrders();

        void AddMemberToOrder(int orderID, int memberID);

        void AddArticleToOrder(int orderID, int articleID);

        IList<Order> ListAllOrdersFromMember(int memberID);


    }
}
