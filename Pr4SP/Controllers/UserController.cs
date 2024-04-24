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
    public class UserController : ControllerBase
    {
        private readonly IDbContextFactory<Pr4SpContext> _contextFactory ;

        public UserController(IDbContextFactory<Pr4SpContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            Pr4SpContext context = _contextFactory.CreateDbContext();
            
            return await context.Users.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            Pr4SpContext context = _contextFactory.CreateDbContext();
            
            var user = await context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, [FromBody] User user)
        {
            Pr4SpContext context = _contextFactory.CreateDbContext();
            
            if (id != user.UserId)
            {
                return BadRequest();
            }

            context.Entry(user).State = EntityState.Modified;
            
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            Pr4SpContext context = _contextFactory.CreateDbContext();
            
            context.Users.Add(user);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            Pr4SpContext context = _contextFactory.CreateDbContext();
            
            var user = await context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            context.Users.Remove(user);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            Pr4SpContext context = _contextFactory.CreateDbContext();
            
            return context.Users.Any(e => e.UserId == id);
        }
    }
}
