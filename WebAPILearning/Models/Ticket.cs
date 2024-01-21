using System;
using System.ComponentModel.DataAnnotations;
using WebAPILearning.ModelValidation;

namespace WebAPILearning.Models
{
    public class Ticket
    {
        public int? TicketId { get; set; }

        [Required]
        public string ProjectId { get; set; }

        public string Description { get; set; }

        [Required]
        public string Title { get; set; }

        public string Owner { get; set; }

        [Ticket_EnsureDueDateForTicketOwner]
        [Ticket_EnsureDueDateIsInFuture]
        public DateTime? DueDate { get; set; }

        public DateTime? EnteredDate { get; set; }
    }
}