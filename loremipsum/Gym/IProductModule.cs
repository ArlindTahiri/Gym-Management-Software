using loremipsum.Gym.Entities;

namespace loremipsum.Gym
{
    public interface IProductModule
    {
        IDictionary<Member, int> SearchMember(string searchTerm);

        Member GetMemberDetails(int memberID);

        IDictionary<Employee, int> SearchEmployee(string searchTerm);

        Employee GetEmployeeDetails(int employeeID);

        IDictionary<Contract, int> SearchContract(string searchTerm);

        Contract GetContractDetails(int contractID);
        
        IDictionary<Article, int> SearchArticle(string searchTerm);

        int GetArticlesActualStock(int articleID);

        int GetArticlesTargetStock(int articleID);

        Article GetArticleDetails(int articleID);

        IDictionary<Order, int> SearchOrder(string searchTerm);

        Order GetOrderDetails(int orderID);

    }
}
