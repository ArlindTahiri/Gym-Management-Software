using loremipsum.Gym.Entities;

namespace loremipsum.Gym
{
    public interface IGymPersistence
    {
        //Member
        void UpdateContractFromMember(Member member, int contractID);

        void CreateMember(Member member);

        void DeleteMember(int memberID);

        void DeleteMembers();

        Member FindMember(int memberID);

        IList<Member> FindMembers();



        //Contract
        void CreateContract(Contract contract);

        void DeleteContract(int contractID);

        void DeleteContracts();

        Contract FindContract(int contractID);

        IList<Contract> FindContracts();



        //Employee
        void CreateEmployee(Employee employee);

        void DeleteEmployee(int employeeID);

        void DeleteEmployees();

        Employee FindEmployee(int employeeID);

        IList<Employee> FindEmployees();



        //Article
        void CreateArticle(Article article);

        void DeleteArticle(int articleID);

        void DeleteArticles();

        Article FindArticle(int articleID);

        IList<Article> FindArticles();

        void UpdateArticle(int articleid, int actualStock, int targetStock);



        //Order
        void CreateOrder(Order order);

        void DeleteOrder(int orderID);

        void DeleteOrders();

        Order FindOrder(int orderID);

        IList<Order> FindOrders();

        void UpdateMemberFromOrder(Order order, int memberID);

        void UpdateOrder(Order order, int articleID);
    }
}
