using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Data
{
    public class EmployeeUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly string _connectionString;
        public EmployeeUnitOfWorkFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IUnitOfWork Get()
        {
            return new EmployeeContext(_connectionString);
        }
    }
}
