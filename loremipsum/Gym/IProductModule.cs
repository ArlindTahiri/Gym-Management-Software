using loremipsum.Gym.Entities;

namespace loremipsum.Gym
{
    public interface IProductModule
    {
        //Member
        IDictionary<Member, int> SearchMember(string searchTerm);

        Member GetMemberDetails(int memberID);


        //Employee
        IDictionary<Employee, int> SearchEmployee(string searchTerm);

        Employee GetEmployeeDetails(int employeeID);


        //Contract
        IDictionary<Contract, int> SearchContract(string searchTerm);

        Contract GetContractDetails(int contractID);
        

        //Article
        IDictionary<Article, int> SearchArticle(string searchTerm);

        int GetArticlesActualStock(int articleID);

        int GetArticlesTargetStock(int articleID);

        Article GetArticleDetails(int articleID);


        //Order
        IDictionary<Order, int> SearchOrder(string searchTerm);

        Order GetOrderDetails(int orderID);

        //LogIn
        LogIn GetLogInDetails(string logInName);
    }
}
