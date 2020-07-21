using NUnit.Framework;
using System;

namespace Common.Data.Employee.Test
{
    public class Tests
    {
        private const string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\projects\EmployeeManager\Employee\Common.Data.Employee.Test\Data\EmployeeManagerTest.mdf;Integrated Security=True";
        
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CanCreateEmployee()
        {
            var empReposotory = new GenericRepository<Model.Employee>();
            var employee = new Common.Model.Employee();
            using (var unitOfWork = new EmployeeContext(_connectionString))
            {
                empReposotory.Context = unitOfWork;
                employee.Active = true;
                employee.FirstName = "Sumeet";
                employee.LastName = "Deshmukh";
                employee.MiddleName = "Jayant";
                employee.DateOfBirth = new DateTime(1974, 7, 13);
                employee.JoiningDate = DateTime.Now.Date.AddDays(-5);
                employee.SeparationDate = null;
                employee.Email = string.Empty;
                employee.Gender = "M";
                employee.CurrentAddress = new Model.CurrentAddress()
                {
                    AddressLine1 = "Address1",
                    AddressLine2 = "Address2",
                    City = "Pune",
                    State = "Maharashtra",
                    PinCode = "411021"
                };
                employee.PermanentAddress = new Model.PermanentAddress()
                {
                    AddressLine1 = "Address3",
                    AddressLine2 = "Address4",
                    City = "Pune",
                    State = "Maharashtra",
                    PinCode = "411021"
                };
                employee.DepartmentId = 1;
                empReposotory.Create(employee);
                unitOfWork.Commit();
            }

            Assert.IsTrue(employee.Id > 0);
        }

        [TearDown]
        public void TearDown()
        {


        }
    }
}