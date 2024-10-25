using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRM392_BookSoccerYard.API.DTO.Slot;
using PRM392_BookSoccerYard.API.Models;

namespace PRM392_BookSoccerYard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotsController : ControllerBase
    {
        private readonly PRM392_BookSoccerYardContext _context;
        private IMapper _mapper;

        public SlotsController(PRM392_BookSoccerYardContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Slots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SlotDTO>>> GetSlots()
        {
            var list = await _context.Slots.ToListAsync();
            return _mapper.Map<List<SlotDTO>>(list);
        }

        // GET: api/Slots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SlotDTO>> GetSlot(int id)
        {
            var slot = await _context.Slots.FindAsync(id);

            if (slot == null)
            {
                return NotFound();
            }

            return _mapper.Map<SlotDTO>(slot);
        }

        // PUT: api/Slots/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSlot(int id, UpdatedSlot slotDTO)
        {
           var slot = _context.Slots.Find(id);
            slot.Name = slotDTO.Name;
            slot.StartTime = slotDTO.StartTime;
            slot.EndTime = slotDTO.EndTime;
            slot.Status = slotDTO.Status;
            slot.PriceUp = slotDTO.PriceUp;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SlotExists(id))
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

        // POST: api/Slots
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SlotDTO>> PostSlot(CreatedSlot slotDTO)
        {
            var slot = _mapper.Map<Slot>(slotDTO);
            slot.Status = true;
            _context.Slots.Add(slot);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSlot", new { id = slot.Id }, _mapper.Map<SlotDTO>(slot));
        }

        // DELETE: api/Slots/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSlot(int id)
        {
            var slot = await _context.Slots.FindAsync(id);
            if (slot == null)
            {
                return NotFound();
            }

            _context.Slots.Remove(slot);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SlotExists(int id)
        {
            return _context.Slots.Any(e => e.Id == id);
        }
    }
}
