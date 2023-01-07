    using loremipsum.Gym;
using loremipsum.Gym.Entities;
using loremipsum.Gym.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestLoremIpsum
{
    [TestClass]
    public class UnitTestEmployee
    {
        private IProductAdmin Admin;
        private IProductModule Query;
        private Employee e1, e2, e3, e4;


        [TestInitialize()]
        public void SetUp()
        {
            IGymPersistence persistence = new GymPersistenceEF();
            GymFactory factory = new GymFactory(persistence);
            Admin = factory.GetProductAdmin();
            Query = factory.GetProductModule();
            GenerateTestData();
        }

        public void GenerateTestData()
        {
            e1 = new Employee("Anton", "Zunhammer", "Lindenstraße 3", 83374, "Traunwalchen", "Deutschland",
                    "Anton.Zunhammer@gmail.com", "DE77500105176812849778", new DateTime(1999, 1, 1));
            e2 = new Employee("Anton", "Zunhammer", "Lindenstraße 3", 83374, "Traunwalchen", "Deutschland",
                    "Anton.Zunhammer@gmail.com", "DE77500105176812849778", new DateTime(1999, 1, 1));
            e3 = new Employee("Nina", "Niedl", "Eichenweg 3", 83301, "Traunreut", "Deutschland",
                    "Nina.Niedl@gmail.com", "DE65500105176354525673", new DateTime(1987, 2, 23));
            e4 = new Employee("Peter","Gebauer", "Große Budengasse 8",50667,"Köln","Deutschland",
                    "Petergebauer@gmail.com", "DE91500105172839528154", new DateTime(1978, 5, 13));
        }

        [TestMethod]
        public void CreateEmployee()
        {
            //Add the employee e1
            Admin.AddEmployee(e1);


            //Test if the employee is in the database
            Assert.IsTrue(Query.GetEmployeeDetails(e1.EmployeeID).CompareTo(e1) == 0);


            //Test if you can upload the same employee multiple times:
            Assert.ThrowsException<Microsoft.EntityFrameworkCore.DbUpdateException>(() => Admin.AddEmployee(e1));


            //Test if you can upload different employee object but same properties.
            Assert.ThrowsException<Microsoft.EntityFrameworkCore.DbUpdateException>(() => Admin.AddEmployee(e2));


            //Add also the other employees
            Admin.AddEmployee(e3);
            Admin.AddEmployee(e4);
        }

        [TestMethod]
        public void UpdateEmployee()
        {
            //Test if you can update Employee Properties
            Admin.UpdateEmployee(e3.EmployeeID, "Tina", "Peters", "Eichenweg 1", 83701, "Stephanskirchen", "Deutschland",
                    "Tina.Peters@gmail.com", "DE96500105172721576161", new DateTime(1986, 3, 30));
            Employee newEmployee = Query.GetEmployeeDetails(e3.EmployeeID);
            Assert.IsTrue(newEmployee.Iban.Equals("DE96500105172721576161") && newEmployee.Forename.Equals("Tina") && newEmployee.Surname.Equals("Peters") &&
                newEmployee.Street.Equals("Eichenweg 1") && newEmployee.PostcalCode== 83701 && newEmployee.City.Equals("Stephanskirchen") && newEmployee.Country.Equals("Deutschland") &&
                newEmployee.EMail.Equals("Tina.Peters@gmail.com") && newEmployee.Birthday.Equals(new DateTime(1986, 3, 30)));
        }

        [TestMethod]
        public void DeleteEmployee()
        {
            //Test if you can delete one of the employees
            Admin.DeleteEmployee(e1.EmployeeID);
            Assert.IsNull(Query.GetEmployeeDetails(e1.EmployeeID));

            //Test if you can delete the same employee multiple times
            Assert.ThrowsException<Microsoft.EntityFrameworkCore.DbUpdateException>(() => Admin.DeleteEmployee(e1.EmployeeID));

            //Test if you can delete all of the rest employees
            Admin.DeleteEmployees();
            Assert.IsNull(Query.GetEmployeeDetails(e3.EmployeeID));
            Assert.IsNull(Query.GetContractDetails(e4.EmployeeID));
        }

    }
}
