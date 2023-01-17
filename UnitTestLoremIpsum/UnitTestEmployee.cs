    using loremipsum.Gym;
using loremipsum.Gym.Entities;
using loremipsum.Gym.Persistence;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
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
        private Employee e1, e2;


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
            Admin.DeleteEmployees();
            e1 = new Employee("Anton", "Zunhammer", "Lindenstraße 3", 83374, "Traunwalchen", "Deutschland",
                    "Anton.Zunhammer@gmail.com", "DE77500105176812849778", new DateTime(1999, 1, 1));
            e2 = new Employee("Anton", "Zunhammer", "Lindenstraße 3", 83374, "Traunwalchen", "Deutschland",
                    "Anton.Zunhammer@gmail.com", "DE77500105176812849778", new DateTime(1999, 1, 1));
        }

        [TestMethod]
        public void CreateEmployee()
        {
            //Add the emoloyee e1
            Admin.AddEmployee(e1);

            //Test if the employee is in the database
            Assert.IsTrue(Query.GetEmployeeDetails(e1.EmployeeID).CompareTo(e1) == 0);
        }

        [TestMethod]
        public void UpdateEmployee()
        {
            Admin.AddEmployee(e1);
            //Test if you can update Employee Properties
            Admin.UpdateEmployee(e1.EmployeeID, "Tina", "Peters", "Eichenweg 1", 83701, "Stephanskirchen", "Deutschland",
                    "Tina.Peters@gmail.com", "DE96500105172721576161", new DateTime(1986, 3, 30));
            Employee newEmployee = Query.GetEmployeeDetails(e1.EmployeeID);
            Assert.IsTrue(newEmployee.Iban.Equals("DE96500105172721576161") && newEmployee.Forename.Equals("Tina") && newEmployee.Surname.Equals("Peters") &&
                newEmployee.Street.Equals("Eichenweg 1") && newEmployee.PostcalCode== 83701 && newEmployee.City.Equals("Stephanskirchen") && newEmployee.Country.Equals("Deutschland") &&
                newEmployee.EMail.Equals("Tina.Peters@gmail.com") && newEmployee.Birthday.Equals(new DateTime(1986, 3, 30)));
        }

        [TestMethod]
        public void ListEmployees()
        {
            //add an employee and than check if there are employees
            Admin.AddEmployee(e1);
            IList<Employee> employees = Admin.ListEmployees();
            Assert.IsTrue(employees.Count > 0);

            //now delete them and check again
            Admin.DeleteEmployees();
            employees = Admin.ListEmployees();
            Assert.IsTrue(employees.Count == 0);
        }

        [TestMethod]
        public void DeleteEmployee()
        {
            //add an employee and than check if you can delete it
            Admin.AddEmployee(e1);
            Assert.IsNotNull(Query.GetEmployeeDetails(e1.EmployeeID));
            Admin.DeleteEmployee(e1.EmployeeID);
            Assert.IsNull(Query.GetEmployeeDetails(e1.EmployeeID));
        }

        [TestMethod]
        public void DeleteEmployees()
        {
            //add multiple employees
            Admin.AddEmployee(e1);
            Admin.AddEmployee(e2);
            IList<Employee> employees = Admin.ListEmployees();
            Assert.IsTrue(employees.Count > 0);

            //check if you can delete all employees
            Admin.DeleteEmployees();
            employees = Admin.ListEmployees();
            Assert.IsTrue(employees.Count == 0);
        }
    }
}
