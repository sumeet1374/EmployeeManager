using System;
using System.Collections.Generic;

namespace Common.Model
{
    /// <summary>
    ///  Entity class for employee
    /// </summary>
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime JoiningDate { get; set; }

        public DateTime? SeparationDate { get; set; }

        public  CurrentAddress CurrentAddress { get; set; }

        public  PermanentAddress PermanentAddress { get; set; }
        public bool Active { get; set; }

        public string Email { get; set; }

        public int DepartmentId { get; set; }
        public  Department Department { get; set; }

        public  List<DocumentEmployee> AssociatedDocuments { get; set; }

    }
}
