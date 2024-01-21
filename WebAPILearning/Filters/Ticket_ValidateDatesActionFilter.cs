using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using WebAPILearning.Models;

public class Ticket_ValidateDatesActionFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        var ticket = context.ActionArguments["ticket"] as Ticket;
        if (ticket != null && !string.IsNullOrWhiteSpace(ticket.Owner))

        {
            bool isValid = true;
            if (ticket.EnteredDate.HasValue == false)
            {
                context.ModelState.AddModelError("EnteredDate", "EnteredDate is required.");
                isValid = false;
            }
            else if (ticket.EnteredDate.HasValue && ticket.DueDate.HasValue && ticket.EnteredDate.Value > ticket.DueDate.Value)
            {
                context.ModelState.AddModelError("DueDate", "DueDate has to be later than the EnteredDate");
                isValid = false;
            }
            if (!isValid)
            {
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
        }
    }
}