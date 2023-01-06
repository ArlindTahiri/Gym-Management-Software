﻿using loremipsum.Gym.Entities;

namespace loremipsum.Gym
{
    public interface IGymPersistence
    {
        //Member
        void UpdateContractFromMember(Member member, Contract contract);

        Member CreateMember(Contract contract, string forename, string surname, string street, int postcalCode, string city, string country, string eMail, string iban, DateTime birthday);

        void DeleteMember(int memberID);

        void DeleteMembers();

        Member FindMember(int memberID);

        IList<Member> FindMembers();

        void UpdateMember(Member member, string forename, string surname, string street, int postcalCode, string city, string country, string eMail, string iban, DateTime birthday);



        //Contract
        void CreateContract(Contract contract);

        void DeleteContract(int contractID);

        void DeleteContracts();

        Contract FindContract(int contractID);

        IList<Contract> FindContracts();

        void UpdateContract(Contract contract, string contractType, int price);



        //Employee
        void CreateEmployee(Employee employee);

        void DeleteEmployee(int employeeID);

        void DeleteEmployees();

        Employee FindEmployee(int employeeID);

        IList<Employee> FindEmployees();

        void UpdateEmployee(Employee employee, string forename, string surname, string street, int postcalCode, string city, string country, string eMail, string iban, DateTime birthday);



        //Article
        void CreateArticle(Article article);

        void DeleteArticle(int articleID);

        void DeleteArticles();

        Article FindArticle(int articleID);

        IList<Article> FindArticles();

        void UpdateArticle(Article article, string articleName, int price, int actualStock, int targetStock);



        //Order
        Order CreateOrder(Member member, Article article, int amount);

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

        void UpdateLogIn(LogIn logIn, string newLogInName, string logInPassword, int rank);

    }
}
