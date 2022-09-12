using System;
using System.Collections;
using System.Runtime.Serialization;

namespace EmployeeHierarchy.CustomException
{
  
    class SalaryInvalid : Exception
    {
        public SalaryInvalid(string message) : base(message)
        {
        }
    }
}
