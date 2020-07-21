using Common.Data;
using Moq;
using NUnit.Framework;
using System;

namespace Common.Services.Employee.Test
{
    /// <summary>
    ///  Employee Service Test
    ///  Some busines logic has been unit tested for create operation
    /// </summary>
    public class EmployeeServiceTest
    {

        private IUnitOfWorkFactory GetFactory()
        {
            var moq = new Mock<IUnitOfWorkFactory>();
            var moqUnitOfWork = new Mock<IUnitOfWork>();
            moqUnitOfWork.Setup(x => x.Commit()).Verifiable();
            moq.Setup(x => x.Get()).Returns(moqUnitOfWork.Object);
            return moq.Object;

        }

        private IRepository<Model.Employee> GetEmployeeRepository()
        {
            var moqemp = new Mock<IRepository<Model.Employee>>();
            moqemp.Setup(x => x.Create(It.IsAny<Model.Employee>())).Verifiable();
            return moqemp.Object;

        }

        private IRepository<Model.DocumentEmployee> GetDocumentRepository()
        {
            var moqemp = new Mock<IRepository<Model.DocumentEmployee>>();
            moqemp.Setup(x => x.Create(It.IsAny<Model.DocumentEmployee>())).Verifiable();
            return moqemp.Object;

        }
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldNotCreateEmployeeIfFirstNameIsMissing()
        {
            var employeeService = new EmployeeService(GetFactory(),
                                                      GetEmployeeRepository(),
                                                       GetDocumentRepository());

            var employee = new Model.Employee();
            try
            {
                employeeService.Create(employee);
            }
            catch(BusinessException ex)
            {
                string expectedMessage = string.Format(Messages.IS_REQUIRED, nameof(employee.FirstName));
                Assert.AreEqual(expectedMessage, ex.Message);
            }

        }


        [Test]
        public void ShouldNotCreateEmployeeIfLastNameIsMissing()
        {
            var employeeService = new EmployeeService(GetFactory(),
                                                      GetEmployeeRepository(),
                                                       GetDocumentRepository());

            var employee = new Model.Employee();
            employee.FirstName = "FName";
            try
            {
                employeeService.Create(employee);
            }
            catch (BusinessException ex)
            {
                string expectedMessage = string.Format(Messages.IS_REQUIRED, nameof(employee.LastName));
                Assert.AreEqual(expectedMessage, ex.Message);
            }

        }

        [Test]
        public void ShouldNotCreateEmployeeIfGenderIsMissing()
        {
            var employeeService = new EmployeeService(GetFactory(),
                                                      GetEmployeeRepository(),
                                                       GetDocumentRepository());

            var employee = new Model.Employee();
            employee.FirstName = "FName";
            employee.LastName = "LName";
            try
            {
                employeeService.Create(employee);
            }
            catch (BusinessException ex)
            {
                string expectedMessage = string.Format(Messages.IS_REQUIRED, nameof(employee.Gender));
                Assert.AreEqual(expectedMessage, ex.Message);
            }

        }

        [Test]
        public void ShouldNotCreateEmployeeIfGenderIsInvalid()
        {
            var employeeService = new EmployeeService(GetFactory(),
                                                      GetEmployeeRepository(),
                                                       GetDocumentRepository());

            var employee = new Model.Employee();
            employee.FirstName = "FName";
            employee.LastName = "LName";
            employee.Gender = "ABC";
            try
            {
                employeeService.Create(employee);
            }
            catch (BusinessException ex)
            {
                string expectedMessage = Messages.GENDER_RANGE;
                Assert.AreEqual(expectedMessage, ex.Message);
            }

        }


        [Test]
        public void ShouldNotCreateEmployeeIfUnderAge()
        {
            var employeeService = new EmployeeService(GetFactory(),
                                                      GetEmployeeRepository(),
                                                       GetDocumentRepository());

            var employee = new Model.Employee();
            employee.FirstName = "FName";
            employee.LastName = "LName";
            employee.Gender = "M";
            employee.DateOfBirth = DateTime.Now.AddYears(-18).AddDays(-1);
            try
            {
                employeeService.Create(employee);
            }
            catch (BusinessException ex)
            {
                string expectedMessage = Messages.UNDER_AGE;
                Assert.AreEqual(expectedMessage, ex.Message);
            }

        }

        [Test]
        public void ShouldNotCreateEmployeeIfOverAge()
        {
            var employeeService = new EmployeeService(GetFactory(),
                                                      GetEmployeeRepository(),
                                                       GetDocumentRepository());

            var employee = new Model.Employee();
            employee.FirstName = "FName";
            employee.LastName = "LName";
            employee.Gender = "M";
            employee.DateOfBirth = DateTime.Now.AddYears(-60).AddDays(-1);
            try
            {
                employeeService.Create(employee);
            }
            catch (BusinessException ex)
            {
                string expectedMessage = Messages.OVER_AGE;
                Assert.AreEqual(expectedMessage, ex.Message);
            }

        }
    }
}