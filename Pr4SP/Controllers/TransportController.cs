using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
        /// <summary>
        /// Поиск по айди
        /// </summary>
        /// <param name="id">Поиск по id</param>
        /// <returns></returns>
        // GET ID
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

        // GET CODE
        /// <summary>
        /// Поиск по коду
        /// </summary>
        /// <param name="code">Поиск по коду</param>
        /// <returns></returns>
        [HttpGet("cd/{code}")]
        public async Task<ActionResult<Transport>> GetTransportCode(int code)
        {
            var transport = await _context.Transports.Where(c => c.Code == code).FirstAsync();

            return transport;
        }

        // GET COLOR
        /// <summary>
        /// Поиск по цвету
        /// </summary>
        /// <param name="color">Поиск по цвету</param>
        /// <returns></returns>
        [HttpGet("cl/{color}")]
        public async Task<ActionResult<IEnumerable<Transport>>> GetTransportColor(string color)
        {
            var transport = await _context.Transports.Where(cl => cl.Color == color).ToListAsync();

            return transport;
        }
        

        /// <summary>
        /// Поиск по компании
        /// </summary>
        /// <param name="company">Поиск по компании</param>
        /// <returns></returns>
        [HttpGet("cy/{company}")]
        public async Task<ActionResult<IEnumerable<Transport>>> GetTransportCompany(string company)
        {
            var transport = await _context.Transports.Where(cy => cy.Company == company).ToListAsync();

            return transport;
        } 

        
        
        // PUT
        [HttpPut]
        public async Task<IActionResult> PutTransport([FromBody] Transport transport)
        {
            _context.Entry(transport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransportExists(transport.TransportId))
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

        // POST
        [HttpPost]
        public async Task<ActionResult<Transport>> PostTransport([FromBody]Transport transport)
        {
            await _context.Transports.AddAsync(transport);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostTransport), transport);
        }

        // DELETE
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
