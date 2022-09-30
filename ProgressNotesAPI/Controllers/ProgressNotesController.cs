using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgressNotesAPI.Infrastructure;

namespace ProgressNotesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressNotesController : ControllerBase
    {
        private readonly ProgressNotesDbContext _context;

        public ProgressNotesController(ProgressNotesDbContext context)
        {
            _context = context;
        }

        // GET: api/ProgressNotes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProgressNote>>> GetProgressNotes()
        {
            return await _context.ProgressNotes.ToListAsync();
        }

        // GET: api/ProgressNotes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProgressNote>> GetProgressNote(int id)
        {
            var progressNote = await _context.ProgressNotes.FindAsync(id);

            if (progressNote == null)
            {
                return NotFound();
            }

            return progressNote;
        }

        // PUT: api/ProgressNotes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProgressNote(int id, ProgressNote progressNote)
        {
            if (id != progressNote.Id)
            {
                return BadRequest();
            }

            _context.Entry(progressNote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgressNoteExists(id))
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

        // POST: api/ProgressNotes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProgressNote>> PostProgressNote(ProgressNote progressNote)
        {
            _context.ProgressNotes.Add(progressNote);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProgressNote", new { id = progressNote.Id }, progressNote);
        }

        // DELETE: api/ProgressNotes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgressNote(int id)
        {
            var progressNote = await _context.ProgressNotes.FindAsync(id);
            if (progressNote == null)
            {
                return NotFound();
            }

            _context.ProgressNotes.Remove(progressNote);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProgressNoteExists(int id)
        {
            return _context.ProgressNotes.Any(e => e.Id == id);
        }
    }
}
