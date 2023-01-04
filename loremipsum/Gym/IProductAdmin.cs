﻿using loremipsum.Gym.Entities;

namespace loremipsum.Gym
{
    public interface IProductAdmin
    {
        //Member
        Member AddMember(int contractID, string forename, string surname, string street, int postcalCode, string city, string country, string eMail, string iban, DateTime birthday);

        void DeleteMember(int memberID);

        IList<Member> ListMembers();

        void DeleteMembers();

        void UpdateMember(int memberID, string forename, string surname, string street, int postcalCode, string city, string country, string eMail, string iban, DateTime birthday);

        void UpdateContractFromMember(int memberID, int contractID);

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

        void UpdateEmployee(int employeeID, string forename, string surname, string street, int postcalCode, string city, string country, string eMail, string iban, DateTime birthday, string status);


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

        void UpdateMemberFromOrder(int orderID, int memberID);

        void UpdateArticleToOrder(int orderID, int articleID, int amount);

        IList<Order> ListAllOrdersFromMember(int memberID);


        //LogIn
        void AddLogIn(LogIn logIn);

        void DeleteLogIn(string logInName);

        IList<LogIn> ListLogIns();

        void DeleteLogIns();

        void UpdateLogIn(string logInName, string newLogInName, string newlogInPassword, int rank);
    }
}
