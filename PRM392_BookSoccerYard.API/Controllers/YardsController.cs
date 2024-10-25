﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRM392_BookSoccerYard.API.DTO.Yard;
using PRM392_BookSoccerYard.API.Models;

namespace PRM392_BookSoccerYard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YardsController : ControllerBase
    {
        private readonly PRM392_BookSoccerYardContext _context;
        private IMapper _mapper;
        public YardsController(PRM392_BookSoccerYardContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Yards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<YardDTO>>> GetYards()
        {
            var list = await _context.Yards.ToListAsync();
            return _mapper.Map<List<YardDTO>>(list);
        }

        // GET: api/Yards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<YardDTO>> GetYard(int id)
        {
            var yard = await _context.Yards.FindAsync(id);

            if (yard == null)
            {
                return NotFound();
            }

            return _mapper.Map<YardDTO>(yard);
        }

        // PUT: api/Yards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutYard(int id, UpdatedYard yardDTO)
        {
            var yard = await _context.Yards.FindAsync(id);
            if(yard == null) { return Ok("Not Found"); }
            yard.Status = yardDTO.Status;
            yard.Price = yardDTO.Price;
            yard.Description = yardDTO.Description;
            yard.Img = yardDTO.Img;
            yard.Name = yardDTO.Name;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!YardExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Successfully");
        }

        // POST: api/Yards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<YardDTO>> PostYard(CreatedYard yardDTO)
        {
            var yard = _mapper.Map<Yard>(yardDTO);
            yard.Status = true;
            _context.Yards.Add(yard);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (YardExists(yard.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetYard", new { id = yard.Id },_mapper.Map<YardDTO>(yard));
        }

        // DELETE: api/Yards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteYard(int id)
        {
            var yard = await _context.Yards.FindAsync(id);
            if (yard == null)
            {
                return NotFound();
            }

            _context.Yards.Remove(yard);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool YardExists(int id)
        {
            return _context.Yards.Any(e => e.Id == id);
        }
    }
}