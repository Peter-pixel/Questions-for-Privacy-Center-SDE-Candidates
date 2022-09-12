using System;
using System.Collections.Generic;
using EmployeeHierarchy.CustomException;

namespace EmployeeHierarchy
{
    public class Hierarchy
    {
        readonly Dictionary<string, List<string>> _lstSubOrdinates = new Dictionary<string, List<string>>();
        private List<Employee> _lstEmployees = new List<Employee>();

        public List<Employee> LstEmployees => _lstEmployees;

        public Hierarchy(String[] data)
        {
            ProcessData(data);

            foreach (var emp in _lstEmployees)
            {
                Add(emp.ManagerId, emp.Id);
            }
        }


        public void ProcessData(string[] data)
        {

            int totalceo = 0;//keep count of ceos


            foreach (var li in data)
            {
                try
                {
                    var parts = li.Split(',');
                    var temp = new Employee();
                    temp.Id = parts[0];
                    if (parts[1].Equals(""))
                    {
                        temp.ManagerId = "";
                        totalceo++;
                        //Managers are more than one throws Exception
                        if (totalceo > 1)
                        {
                            throw new ManagerMoreThanOne("Managers are more than one... Exiting");
                        }
                    }
                    else
                    {
                        temp.ManagerId = parts[1];
                    }


                    long salary;
                    var isvalid = Int64.TryParse(parts[2], out salary);
                    //is salary a valid number?
                    if (isvalid)
                    {
                        //valid salary should be greater than 0
                        if (salary > 0)
                        {
                            temp.Salary = salary;
                        }
                        else
                        {
                            throw new SalaryInvalid("Salary is a Negative");
                        }

                    }
                    else
                    {
                        throw new SalaryInvalid("Salary is not valid");
                    }

                    _lstEmployees.Add(temp);
                }
                catch (ManagerMoreThanOne ex)
                {

                    _lstEmployees.Clear();
                    Console.WriteLine(ex.Message);
                    return;
                }
                catch (SalaryInvalid ex)
                {
                    _lstEmployees.Clear();
                    Console.WriteLine(ex.Message);
                    return;
                }
            }

            //Verify That there is a manager
            if (totalceo != 1)
            {
                Console.WriteLine("There is no Manager identified check again the dataset");
                _lstEmployees.Clear();
            }
        }

        public List<String> GetSubordinates(String empId)
        {
            return _lstSubOrdinates[empId];
        }

        public long getSalaryBudget(String root)
        {
            long salary = 0;
            HashSet<String> visited = new HashSet<String>();
            Stack<String> stack = new Stack<String>();
            stack.Push(root);
            while (stack.Count != 0)
            {
                String empId = stack.Pop();
                if (!visited.Contains(empId))
                {
                    visited.Add(empId);
                    foreach (String v in GetSubordinates(empId))
                    {
                        stack.Push(v);
                    }
                }
            }

            if (visited.Count == 0) return salary;
            foreach (var id in visited)
            {
                salary += LookUp(id).Salary;
            }

            return salary;
        }

        public void Add(string employeeId)
        {
            //if Employee ID exists do nothing
            if (_lstSubOrdinates.ContainsKey(employeeId))
            {
                return;
            }

            _lstSubOrdinates.Add(employeeId, new List<string>());
        }

        public void Add(string boss, string employeeId)
        {
            Add(boss);
            Add(employeeId);
            _lstSubOrdinates[boss].Add(employeeId);
        }

        public Employee LookUp(string id)
        {
            foreach (Employee emp in _lstEmployees)
            {
                if (emp.Id.Equals(id))
                {
                    return emp;
                }
            }

            return null;
        }



    }
}
