using System;
using System.IO;
using EmployeeHierarchy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmployeeHierarchytest
{

    [TestClass()]
    public class HierarchyTest
    {

        private Hierarchy _hierarchy;

        [TestInitialize]
        public void TestInitiliaze()
        {
            var data = GetData("Sample.csv");
            _hierarchy = new Hierarchy(data);
        }


        [TestMethod()]
        public void AddTest()
        {

            Assert.IsTrue(_hierarchy.LstEmployees.Contains(new Employee
            { Id = "Employee2", ManagerId = "Employee1", Salary = 800 }));
            Assert.IsTrue(_hierarchy.LstEmployees.Contains(new Employee
            { Id = "Employee4", ManagerId = "Employee2", Salary = 500 }));
        }


        [TestMethod]
        public void SubOrdinate_Not_NULL()
        {
            var subordinates = _hierarchy.GetSubordinates("Employee2");
            Assert.AreEqual(2, subordinates.Count);
        }


        [TestMethod]
        public void Employee5_has_No_SubOrdinates_Test()
        {
            var subordinates = _hierarchy.GetSubordinates("Employee5");
            Assert.AreEqual(0, subordinates.Count);
        }


        [TestMethod]
        public void LookUpTest()
        {
            Employee emp = _hierarchy.LookUp("Employee1");
            Assert.IsNotNull(emp);
        }


        [TestMethod]
        public void Lookup_Wrong_emp_id_Test()
        {
            Employee emp = _hierarchy.LookUp("Employee10");
            Assert.IsNull(emp);
        }

        string[] GetData(String path)
        {

            return File.ReadAllLines(path);
        }


        [TestMethod]
        public void GetBudgetTest()
        {
            Assert.AreEqual(1800, _hierarchy.getSalaryBudget("Employee2"));
            Assert.AreEqual(500, _hierarchy.getSalaryBudget("Employee3"));
            Assert.AreEqual(3800, _hierarchy.getSalaryBudget("Employee1"));
        }


        [TestMethod]
        public void Test_Invalid_Salary_Not_Added()
        {
            Hierarchy h2 = new Hierarchy(GetData("InvalidSalary.csv"));
            Assert.IsFalse(h2.LstEmployees.Contains(new Employee
            { Id = "Employee5" }));
            Assert.IsFalse(h2.LstEmployees.Contains(new Employee
            { Id = "Employee2" }));

            Assert.AreEqual(0, h2.LstEmployees.Count);

        }

        [TestMethod]
        public void Test_Manager_More_Than_Three()
        {
            Hierarchy h = new Hierarchy(GetData("ManagerMoreThanOne.csv"));
            Assert.IsFalse(h.LstEmployees.Contains(new Employee
            { Id = "Employee5" }));
            Assert.IsFalse(h.LstEmployees.Contains(new Employee
            { Id = "Employee1" }));
            Assert.AreEqual(0, h.LstEmployees.Count);

        }

        [TestMethod]
        public void Test_Negative_Salary_Check()
        {
            Hierarchy h = new Hierarchy(GetData("NegativeSalary.csv"));
            Assert.IsFalse(h.LstEmployees.Contains(new Employee
            { Id = "Employee5" }));
            Assert.AreEqual(0, h.LstEmployees.Count);
        }


        [TestMethod]
        public void No_Manager_Record()
        {
            Hierarchy h = new Hierarchy(GetData("NoManager.csv"));
            Assert.AreEqual(0, h.LstEmployees.Count);
        }

    }
}