using Common.Model;
using System.Collections.Generic;

namespace Common.Services
{
    /// <summary>
    ///  Business Service Interface to Manage Employee
    /// </summary>
    public interface IEmployeeService:IGenericService<Employee>
    {
        void UploadDocument(int employeeId, Document document);
        void RemoveDocument(int employeeId, int documentId);

        List<DocumentEmployee> FindEmployeeDocuments(int employeeId);

        Document DownloadDocument(int employeeId, int documentId);
    }
}
