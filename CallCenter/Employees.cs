using System;
using System.Collections.Generic;

namespace CallCenter
{
    class Employees
    {
        private readonly object _locker = new object();
        private readonly Stack<IEmployee> _operators = new Stack<IEmployee>();
        private readonly Stack<IEmployee> _managers = new Stack<IEmployee>();
        private readonly Stack<IEmployee> _directors = new Stack<IEmployee>();

        public int TotalCount { get; private set; }

        public Employees(int operatorsCount, int managersCount, int directorsCount)
        {
            _operators = new Stack<IEmployee>(operatorsCount);
            CreateEmployees(EmployeeType.Operator, operatorsCount);
            CreateEmployees(EmployeeType.Manager, managersCount);
            CreateEmployees(EmployeeType.Director, directorsCount);
            TotalCount = operatorsCount + managersCount + directorsCount;
        }

        public void SetAvailable(IEmployee employee)
        {
            lock (_locker)
            {
                Push(employee);
            }
        }

        public bool TryGetAvailable(out IEmployee employee)
        {
            lock (_locker)
            {
                if (_operators.TryPop(out employee))
                {
                    return true;
                }
                if (_managers.TryPop(out employee))
                {
                    return true;
                }
                if (_directors.TryPop(out employee))
                {
                    return true;
                }
            }
            return false;
        }

        private void CreateEmployees(EmployeeType type, int count)
        {
            int number = 0;
            while (number < count)
            {
                var employee = new Employee(type, ++number);
                Push(employee);
            }
        }

        private void Push(IEmployee employee)
        {
            switch (employee.Type)
            {
                case EmployeeType.Operator:
                    _operators.Push(employee);
                    break;
                case EmployeeType.Director:
                    _directors.Push(employee);
                    break;
                case EmployeeType.Manager:
                    _managers.Push(employee);
                    break;
                default:
                    throw new ArgumentException($"{employee.Type} is not supported");
            }
        }
    }
}
