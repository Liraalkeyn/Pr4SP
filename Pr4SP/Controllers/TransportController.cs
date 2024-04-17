using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pr4SP.Context;
using Pr4SP.Models;

namespace Pr4SP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportController : ControllerBase
    {
        private readonly Pr4SpContext _context;

        public TransportController(Pr4SpContext context)
        {
            _context = context;
        }

        // GET: api/Transport
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transport>>> GetTransports()
        {
            return await _context.Transports.ToListAsync();
        }

        // GET: api/Transport/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transport>> GetTransport(int id)
        {
            var transport = await _context.Transports.FindAsync(id);

            if (transport == null)
            {
                return NotFound();
            }

            return transport;
        }

        [HttpGet("cd/{code}")]
        public async Task<ActionResult<Transport>> GetTransportCode(int code)
        {
            var transport = await _context.Transports.Where(c => c.Code == code).FirstAsync();

            return transport;
        }

        [HttpGet("cl/{color}")]
        public async Task<ActionResult<IEnumerable<Transport>>> GetTransportColor(string color)
        {
            var transport = await _context.Transports.Where(cl => cl.Color == color).ToListAsync();

            return transport;
        }

        [HttpGet("cy/{company}/{firstname}")]
        public async Task<ActionResult<IEnumerable<Transport>>> GetTransportCompany(string company, string firstname)
        {
            var transport = await _context.Transports.Where(cy => cy.Company == company && cy.User.FirstName == firstname).ToListAsync();

            return transport;
        }

        // PUT: api/Transport/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransport(int id, Transport transport)
        {
            if (id != transport.TransportId)
            {
                return BadRequest();
            }

            _context.Entry(transport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransportExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Transport
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Transport>> PostTransport(Transport transport)
        {
            _context.Transports.Add(transport);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransport", new { id = transport.TransportId }, transport);
        }

        // DELETE: api/Transport/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransport(int id)
        {
            var transport = await _context.Transports.FindAsync(id);
            if (transport == null)
            {
                return NotFound();
            }

            _context.Transports.Remove(transport);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransportExists(int id)
        {
            return _context.Transports.Any(e => e.TransportId == id);
        }
    }
}
