using Microsoft.VisualStudio.TestTools.UnitTesting;
using Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Tests
{
    [TestClass()]
    public class EmployeeTests
    {
        Employee employees = new Employee();
        StringBuilder csvString = new StringBuilder();

        //Tests for Successfully reading the csv file
        [TestMethod()]
        public void readCsvTest()
        {
            string expected = "Success";
            string actual = "";
                
            csvString.AppendLine("Employee4,Employee2,500");
            csvString.AppendLine("Employee3,Employee1,500");
            csvString.AppendLine("Employee1,,1000");
            csvString.AppendLine("Employee5,Employee1,500");
            csvString.AppendLine("Employee2,Employee1,800");

            actual = employees.validateCsv(csvString.ToString());
            Assert.AreEqual(expected, actual);
            //Assert.Fail();
        }

        [TestMethod()]
        public void validSalaryTest()
        {
            csvString.AppendLine("Employee4,Employee2,500");
            csvString.AppendLine("Employee3,Employee1,500");
            csvString.AppendLine("Employee1,,1000");
            csvString.AppendLine("Employee5,Employee1,500");
            csvString.AppendLine("Employee2,Employee1,800");

            employees = new Employee(csvString.ToString());
        }

        //Tests to validate salary to be integers
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void invalidSalaryTest()
        {
            csvString.AppendLine("Employee4,Employee2,500");
            csvString.AppendLine("Employee3,Employee1,500");
            csvString.AppendLine("Employee1,,1000");
            csvString.AppendLine("Employee5,Employee1,yui");
            csvString.AppendLine("Employee2,Employee1,800");

            employees = new Employee(csvString.ToString());
        }

        // Tests to validate one employee does not report to more than one manager.
        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void oneManagerPerEmployeeTest()
        {
            csvString.AppendLine("Employee4,Employee2,500");
            csvString.AppendLine("Employee3,Employee1,Employee2,500");
            csvString.AppendLine("Employee1,,1000");
            csvString.AppendLine("Employee5,Employee1,500");
            csvString.AppendLine("Employee2,Employee1,800");

            employees = new Employee(csvString.ToString());
            //Assert.Fail();
        }

        //There is only one CEO, i.e. only one employee with no manager.
        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void oneCEOTest()
        {
            csvString.AppendLine("Employee4,Employee2,500");
            csvString.AppendLine("Employee3,Employee1,500");
            csvString.AppendLine("Employee1,,1000");
            csvString.AppendLine("Employee5,,500");
            csvString.AppendLine("Employee2,Employee1,800");

            employees = new Employee(csvString.ToString());
            //Assert.Fail();
        }

        //Test to Validate all managers to be employees
        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void managerIsEmployeeTest()
        {
            csvString.AppendLine("Employee4,Employee6,500");
            csvString.AppendLine("Employee3,Employee1,500");
            csvString.AppendLine("Employee1,,1000");
            csvString.AppendLine("Employee5,Employee1,500");
            csvString.AppendLine("Employee2,Employee1,800");

            employees = new Employee(csvString.ToString());
            //Assert.Fail();
        }

        //Test for budget for a manager who is the CEO
        [TestMethod()]
        public void ceoBudgetTest()
        {
            long expected = 3300;
            long actual = 0;
            csvString.AppendLine("Employee4,Employee2,500");
            csvString.AppendLine("Employee3,Employee1,500");
            csvString.AppendLine("Employee1,,1000");
            csvString.AppendLine("Employee5,Employee1,500");
            csvString.AppendLine("Employee2,Employee1,800");

            employees = new Employee(csvString.ToString());

            actual = employees.managerBudget("Employee1");
            Assert.AreEqual(expected, actual);
            //Assert.Fail();
        }

        //Test for a budget of a specific manager
        [TestMethod()]
        public void managerBudgetTest()
        {
            long expected = 1300;
            long actual = 0;
            csvString.AppendLine("Employee4,Employee2,500");
            csvString.AppendLine("Employee3,Employee1,500");
            csvString.AppendLine("Employee1,,1000");
            csvString.AppendLine("Employee5,Employee1,500");
            csvString.AppendLine("Employee2,Employee1,800");

            employees = new Employee(csvString.ToString());

            actual = employees.managerBudget("Employee2");
            Assert.AreEqual(expected, actual);
            //Assert.Fail();
        }

        //Test for an employee who is not a manager
        [TestMethod()]
        public void juniourBudgetTest()
        {
            long expected = 500;
            long actual = 0;
            csvString.AppendLine("Employee4,Employee2,500");
            csvString.AppendLine("Employee3,Employee1,500");
            csvString.AppendLine("Employee1,,1000");
            csvString.AppendLine("Employee5,Employee1,500");
            csvString.AppendLine("Employee2,Employee1,800");

            employees = new Employee(csvString.ToString());

            actual = employees.managerBudget("Employee5");
            Assert.AreEqual(expected, actual);
            //Assert.Fail();
        }
    }
}