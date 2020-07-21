using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Employee.Api.Filters
{
    public class ErrorMessage
    {
        public string Message { get; set; }
        public string Description { get; set; }
    }
    /// <summary>
    ///  Global Error handler
    /// </summary>
    public class ErrorFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = int.MaxValue - 10;


        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is Common.Services.BusinessException)
            {
                context.Result = new ObjectResult(new ErrorMessage() { Message = context.Exception.Message })
                {
                    StatusCode = 400,
                };
                context.ExceptionHandled = true;
            }
            else if (context.Exception != null)
            {
                context.Result = new ObjectResult(new ErrorMessage() { Message = context.Exception.Message })
                {
                    StatusCode = 500,
                };
                context.ExceptionHandled = true;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}
