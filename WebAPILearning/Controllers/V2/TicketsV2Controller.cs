using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Filters.V2;
using WebAPI.QueryFilter;

namespace WebAPILearning.Controllers.V2
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/tickets")]
    public class TicketsV2Controller : ControllerBase
    {
        private readonly BugsContext db;

        public TicketsV2Controller(BugsContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] TicketQueryFilter ticketQueryFilter)
        {
            IQueryable<Ticket> tickets = db.Tickets;
            if (ticketQueryFilter != null)
            {
                if (ticketQueryFilter.Id.HasValue)
                    tickets = tickets.Where(x => x.TicketId == ticketQueryFilter.Id);
            }
            if (!string.IsNullOrWhiteSpace(ticketQueryFilter.Title))
            {
                tickets = tickets.Where(x => x.Title.Contains(ticketQueryFilter.Title, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrWhiteSpace(ticketQueryFilter.Description))
            {
                tickets = tickets.Where(x => x.Description.Contains(ticketQueryFilter.Description, StringComparison.OrdinalIgnoreCase));
            }
            return Ok(await tickets.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ticket = await db.Tickets.FindAsync(id);
            if (ticket == null) return NotFound();
            return Ok(ticket);
        }

        [Ticket_EnsureDescriptionPresentActionFilter]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Ticket ticket)
        {
            db.Tickets.Add(ticket);
            await db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = ticket.TicketId }, ticket);
        }

        [Ticket_EnsureDescriptionPresentActionFilter]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Ticket ticket)
        {
            if (id != ticket.TicketId) return BadRequest();
            db.Entry(ticket).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch
            {
                if (await db.Tickets.FindAsync(id) == null)
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await db.Tickets.FindAsync(id);
            if (ticket == null) return NotFound();
            db.Tickets.Remove(ticket);
            await db.SaveChangesAsync();
            return Ok(ticket);
        }
    }
}