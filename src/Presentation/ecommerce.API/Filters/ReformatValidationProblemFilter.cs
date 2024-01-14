using ecommerce.API.Utilities.Json;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ecommerce.API.Filters
{
    public class ReformatValidationProblemFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is BadRequestObjectResult badRequestObjectResult &&
                badRequestObjectResult.Value is ValidationProblemDetails validationProblemDetails)
            {
                StringBuilder stringBuilder = new StringBuilder();
                List<string> errors = new List<string>();
                foreach (var error in validationProblemDetails.Errors)
                {
                    stringBuilder.Clear();
                    stringBuilder.AppendJoin(", ", error.Value);
                    errors.Add(stringBuilder.ToString());
                }

                context.Result = new BadRequestObjectResult(JsonUtility.Fail(errors, StatusCodes.Status400BadRequest));
            }

            base.OnResultExecuting(context);
        }
    }
}
