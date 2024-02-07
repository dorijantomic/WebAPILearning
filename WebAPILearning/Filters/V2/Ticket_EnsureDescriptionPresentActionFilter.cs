using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Drawing;

namespace WebAPI.Filters.V2
{
    public class Ticket_EnsureDescriptionPresentActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var ticket = context.ActionArguments["ticket"] as Ticket;
            if (ticket != null && !ticket.ValidateDescription())
            {
                context.ModelState.AddModelError("Description", "Description is required.");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}