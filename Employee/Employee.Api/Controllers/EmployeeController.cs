using Common.Model;
using Common.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Employee.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private const string EMPTY_FILE = "Empty file";
        private readonly IEmployeeService _service;
        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }


        [Route("{id?}")]
        public IActionResult Get(int id)
        {
            var employee = _service.FindById(id);
            return Ok(employee);
        }

        [Route("All")]
        [HttpGet]
        public IActionResult Get(int pageNumber, int pageSize)
        {
            var employees = _service.FindAll(pageNumber, pageSize);
            return Ok(employees);
        }

        [HttpPost]
        public IActionResult Post(Common.Model.Employee employee)
        {
            _service.Create(employee);
            return Ok();
        }

        [HttpPut]
        public IActionResult Put(Common.Model.Employee employee)
        {
            _service.Update(employee);
            return Ok();
        }
        
        [HttpDelete]
        public IActionResult Deactivate(int id)
        {
            _service.Deactivate(id);
           return Ok();
        }

        [HttpPost]
        [Route("UploadDoc")]
        public IActionResult Upload(FileUploadModel model)
        {
            if(string.IsNullOrEmpty(model.FileStream))
            {
                throw new BusinessException("EMPTY_FILE");

              
            }

            var byteArray = Convert.FromBase64String(model.FileStream);
            var document = new Document() { Id = model.Id, FileName = model.FileName, Type = model.Type, FileContent = byteArray };
            _service.UploadDocument(model.EmployeeId, document);
            return Ok();
        }

        [HttpGet]
        [Route("Documents/Remove")]
        public IActionResult RemoveDocument(int empId, int documentId)
        {
            _service.RemoveDocument(empId, documentId);
            return Ok();
        }

        [HttpGet]
        [Route("Documents/{empId?}")]
        public IActionResult FindDocumentList(int empId)
        {
            return Ok(_service.FindEmployeeDocuments(empId));
        }


        [HttpGet]
        [Route("Documents/Download")]
        public IActionResult DownloadDocument(int empId,int documentId)
        {
            return Ok(_service.DownloadDocument(empId, documentId));
        }




    }
}
