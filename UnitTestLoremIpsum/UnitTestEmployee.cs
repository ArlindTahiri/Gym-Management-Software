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


        [TestInitialize]
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
            e1 = new Employee("Anton_test", "Zunhammer", "Lindenstraße 3", 83374, "Traunwalchen", "Deutschland",
                    "Anton.Zunhammer@gmail.com", "DE77500105176812849778", new DateTime(1999, 1, 1));
            e2 = new Employee("Anton_test", "Zunhammer", "Lindenstraße 3", 83374, "Traunwalchen", "Deutschland",
                    "Anton.Zunhammer@gmail.com", "DE77500105176812849778", new DateTime(1999, 1, 1));
        }

        [TestMethod]
        public void TestEmployee()
        {
            //Add the emoloyee e1
            Admin.AddEmployee(e1);

            //Test if the employee is in the database
            Assert.IsTrue(Query.GetEmployeeDetails(e1.EmployeeID).CompareTo(e1) == 0);


            //Add an employee e2 which is identical with e1
            Admin.AddEmployee(e2);
            Assert.IsTrue(Query.GetEmployeeDetails(e2.EmployeeID) == null);


            //Update the employee e1
            Admin.UpdateEmployee(e1.EmployeeID, "Tina_test", "Peters", "Eichenweg 1", 83701, "Stephanskirchen", "Deutschland",
                    "Tina.Peters@gmail.com", "DE96500105172721576161", new DateTime(1986, 3, 30));

            //Test if the updated employee is in the database
            Employee newEmployee = Query.GetEmployeeDetails(e1.EmployeeID);
            Assert.IsTrue(newEmployee.Iban.Equals("DE96500105172721576161") && newEmployee.Forename.Equals("Tina_test") && newEmployee.Surname.Equals("Peters") &&
                newEmployee.Street.Equals("Eichenweg 1") && newEmployee.PostcalCode == 83701 && newEmployee.City.Equals("Stephanskirchen") && newEmployee.Country.Equals("Deutschland") &&
                newEmployee.EMail.Equals("Tina.Peters@gmail.com") && newEmployee.Birthday.Equals(new DateTime(1986, 3, 30)));

            //check if there are employees in the IList
            IList<Employee> employees = Admin.ListEmployees();
            Assert.IsTrue(employees.Count > 0);


            //delete employee e1
            Admin.DeleteEmployee(e1.EmployeeID);

            //test if employee e1 is still in database
            Assert.IsNull(Query.GetEmployeeDetails(e1.EmployeeID));
        }
    }
}