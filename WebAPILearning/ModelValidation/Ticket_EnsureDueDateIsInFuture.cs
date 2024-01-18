using System;
using System.ComponentModel.DataAnnotations;
using WebAPILearning.Models;

namespace WebAPILearning.ModelValidation
{
    public class Ticket_EnsureDueDateIsInFuture : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var ticket = validationContext.ObjectInstance as Ticket;
            if (ticket != null && ticket.TicketId == null)
            {
                if (ticket.DueDate.Value < DateTime.Now)
                {
                    return new ValidationResult("Due date has to be in the future");
                }
            }
            return ValidationResult.Success;
        }
    }
}