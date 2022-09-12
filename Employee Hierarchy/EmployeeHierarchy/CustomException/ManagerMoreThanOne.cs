using System;
using System.Runtime.Serialization;

namespace EmployeeHierarchy.CustomException
{
    class ManagerMoreThanOne : Exception
    {
        public ManagerMoreThanOne(string message) : base(message)
        {
        }
    }
}
