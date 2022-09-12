namespace EmployeeHierarchy
{
    public class Employee
    {
        private string _id, _managerId = "";
        private long _salary = 0;


        public string Id
        {
            get => _id;
            set => _id = value;
        }


        public string ManagerId
        {
            get => _managerId;
            set => _managerId = value;
        }

        public long Salary
        {
            get => _salary;
            set => _salary = value;
        }

        public override bool Equals(object obj)
        {
            Employee emp1 = (Employee)obj;
            return (emp1.Id.ToUpper().Equals(Id.ToUpper()));
        }
    }
}
