using Common.Data;
using Common.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Services
{
    /// <summary>
    ///  Employee Service class
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWorkFactory _factory;
        private readonly IRepository<Employee> _employeeRepo;
        private readonly IRepository<DocumentEmployee> _documentEmpRepo;
        private const string DEPARTMENT = "Department";
        private const string CURRENT_ADDRESS = "CurrentAddress";
        private const string PERMANENT_ADDRESS = "PermanentAddress";

        public EmployeeService(IUnitOfWorkFactory factory,
            IRepository<Employee> employeeRepo,
            IRepository<DocumentEmployee> documentEmpRepo)
        {
            _factory = factory;
            _employeeRepo = employeeRepo;
            _documentEmpRepo = documentEmpRepo;

        }

        private static int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            return age;
        }
        private void ValidateEmployee(Employee employee)
        {
            if(string.IsNullOrEmpty(employee.FirstName))
                throw new BusinessException(string.Format(Messages.IS_REQUIRED, nameof(employee.FirstName)));

            if (string.IsNullOrEmpty(employee.LastName))
                throw new BusinessException(string.Format(Messages.IS_REQUIRED, nameof(employee.LastName)));

            if (string.IsNullOrEmpty(employee.Gender))
                throw new BusinessException(string.Format(Messages.IS_REQUIRED, nameof(employee.Gender)));

            if(employee.Gender !="M" &&  employee.Gender != "F")
            {
                throw new BusinessException(Messages.GENDER_RANGE);
            }

            // Age should be 18 years and should be less than 60 years
            var age =  CalculateAge(employee.DateOfBirth);
            if (age < 18)
                throw new BusinessException(Messages.UNDER_AGE);

            
            if (age >= 60)
                throw new BusinessException(Messages.OVER_AGE);



        }
        public void Create(Employee obj)
        {
            obj.Active = true;
            ValidateEmployee(obj);
            using(var unitOfWork = _factory.Get())
            {
                _employeeRepo.Context = unitOfWork;
                _employeeRepo.Create(obj);
                unitOfWork.Commit();
            }
        }
        public void Deactivate(int id)
        {
            using (var unitOfWork = _factory.Get())
            {
                _employeeRepo.Context = unitOfWork;
                var employee = _employeeRepo.GetById(id);
                if (employee == null)
                {
                    throw new BusinessException(Messages.EMPLOYEE_NOT_FOUND);
                }
                employee.Active = false;
                unitOfWork.Commit();
            }
        }
        public PagedResult<Employee> FindAll(int pageNumber,int pageSize)
        {
            using(var unitOfWork = _factory.Get())
            {
                _employeeRepo.Context = unitOfWork;
                return _employeeRepo.GetWithPaging(x => x.Active == true, pageNumber, pageSize, DEPARTMENT, CURRENT_ADDRESS,PERMANENT_ADDRESS);
            }
        }

        public Employee FindById(int id)
        {
            if(id == 0)
            {
                var emp = new Employee();
                emp.DateOfBirth = DateTime.Now.AddYears(-18).Date;
                emp.JoiningDate = DateTime.Now.Date;
                emp.CurrentAddress = new CurrentAddress();
                emp.PermanentAddress = new PermanentAddress();
                return emp;

            }
            using (var unitOfWork = _factory.Get())
            {
                _employeeRepo.Context = unitOfWork;
                var employee = _employeeRepo.GetById(id);
                if(employee == null || employee.Active == false)
                {
                    throw new BusinessException(Messages.EMPLOYEE_NOT_FOUND);
                }
                var context = unitOfWork.GetContext<DbContext>();
                context.Entry(employee).Reference(x => x.CurrentAddress).Load();
                context.Entry(employee).Reference(x => x.PermanentAddress).Load();
                context.Entry(employee).Reference(x => x.Department).Load();
              
                return employee;
            }
        }

        public void RemoveDocument(int eployeeId, int documentId)
        {
            using (var unitOfWork = _factory.Get())
            {
                _documentEmpRepo.Context = unitOfWork;
                var result = _documentEmpRepo.Get(x => x.EmployeeId == eployeeId && x.DocumentId == documentId && x.Employee.Active == true);
                if(result.Count == 1)
                {
                    _documentEmpRepo.Delete(result[0]);
                }

                unitOfWork.Commit();
            }

        }

        public void Update(Employee obj)
        {
            ValidateEmployee(obj);
            using (var unitOfWork = _factory.Get())
            {
                _employeeRepo.Context = unitOfWork;
                var employee = _employeeRepo.GetById(obj.Id);
                if(employee == null)
                {
                    throw new BusinessException(Messages.EMPLOYEE_NOT_FOUND);
                }
                employee.FirstName = obj.FirstName;
                employee.LastName = obj.LastName;
                employee.Email = obj.Email;
                employee.Gender = obj.Gender;
                employee.JoiningDate = obj.JoiningDate;
                employee.LastName = obj.MiddleName;
               // employee.Active = obj.Active;
                employee.CurrentAddress = obj.CurrentAddress;
                employee.PermanentAddress = obj.PermanentAddress;
                employee.SeparationDate = obj.SeparationDate;
                if(employee.DepartmentId != obj.DepartmentId)
                {
                    employee.DepartmentId = obj.DepartmentId;
                }
                
                //employee.Department = obj.Department;
                unitOfWork.Commit();
            }
        }

        public void UploadDocument(int employeeId, Document document)
        {
            using (var unitOfWork = _factory.Get())
            {
                var documentEmployee = new DocumentEmployee();
                documentEmployee.Document = document;
                documentEmployee.EmployeeId = employeeId;
                _documentEmpRepo.Context = unitOfWork;
                _documentEmpRepo.Create(documentEmployee);
                unitOfWork.Commit();
            }
        }

        public List<DocumentEmployee> FindEmployeeDocuments(int employeeId)
        {
            using (var unitOfWork = _factory.Get())
            {
               return unitOfWork.GetContext<DbContext>().Set<DocumentEmployee>()
                    .Include(x => x.Document)
                    .Where(x => x.EmployeeId == employeeId && x.Employee.Active == true)
                    .Select(y => new DocumentEmployee() { Id = y.Id,EmployeeId = y.EmployeeId, DocumentId = y.DocumentId, Document = new Document() { Id = y.Document.Id, FileName = y.Document.FileName, Type = y.Document.Type, FileContent = null } }).ToList();


            }
        }

        public Document DownloadDocument(int employeeId, int documentId)
        {
            using (var unitOfWork = _factory.Get())
            {
                return unitOfWork.GetContext<DbContext>().Set<DocumentEmployee>()
                  .Include(x => x.Document)
                  .Where(x => x.EmployeeId == employeeId && x.Employee.Active == true && x.DocumentId == documentId)
                  .Select(document => document.Document).FirstOrDefault();
            }
                
        }
    }
}
