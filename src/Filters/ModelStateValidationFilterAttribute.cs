using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Sample.AspNetCoreWebApi.Filters
{
    /// <summary>
    /// Filter for checking model state.
    /// </summary>
    public class ModelStateValidationFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Action for checking request model state. If model is not valid, then context result is set to 400 BadRequest.
        /// </summary>
        /// <param name="context">Action context.</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }

            base.OnActionExecuting(context);
        }
    }
}