using loremipsum.Gym.Entities;

namespace loremipsum.Gym
{
    public interface IProductModule
    {
        //Member
        Member GetMemberDetails(int memberID);


        //Employee
        Employee GetEmployeeDetails(int employeeID);


        //Contract
        Contract GetContractDetails(int contractID);
        

        //Article
        Article GetArticleDetails(int articleID);


        //Order
        Order GetOrderDetails(int orderID);


        //LogIn
        LogIn GetLogInDetails(string logInName);
    }
}
