using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Api
{
    public class FileUploadModel
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        public string FileName { get; set; }

        public string Type { get; set; }

        /// <summary>
        /// Base 64 encoded string
        /// </summary>
        public string FileStream { get; set; }
    }
}
