using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using MessageExchangeModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObservationsAPI.Infrastructure;

namespace ObservationsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObservationsController : ControllerBase
    {
        private readonly ObservationsDbContext _context;
        private IPublishEndpoint publishEndpoint;

        public ObservationsController(ObservationsDbContext context, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            this.publishEndpoint = publishEndpoint;
        }

        // GET: api/Observations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Observation>>> GetObservations()
        {
            return await _context.Observations.ToListAsync();
        }

        // GET: api/Observations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Observation>> GetObservation(int id)
        {
            var observation = await _context.Observations.FindAsync(id);

            if (observation == null)
            {
                return NotFound();
            }

            return observation;
        }

        // PUT: api/Observations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutObservation(int id, Observation observation)
        {
            if (id != observation.Id)
            {
                return BadRequest();
            }

            _context.Entry(observation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ObservationExists(id))
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

        // POST: api/Observations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Observation>> PostObservation(Observation observation)
        {
            try
            {
                _context.Observations.Add(observation);
                await _context.SaveChangesAsync();

                await publishEndpoint.Publish<Message>(new Message() { Description = observation.Description, ObservationId = observation.Id });
                return CreatedAtAction("GetObservation", new { id = observation.Id }, observation);
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
                return BadRequest();
            }
         
        }

        // DELETE: api/Observations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteObservation(int id)
        {
            var observation = await _context.Observations.FindAsync(id);
            if (observation == null)
            {
                return NotFound();
            }

            _context.Observations.Remove(observation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ObservationExists(int id)
        {
            return _context.Observations.Any(e => e.Id == id);
        }
    }
}
