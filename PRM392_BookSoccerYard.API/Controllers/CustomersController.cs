using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRM392_BookSoccerYard.API.DTO.Customer;
using PRM392_BookSoccerYard.API.Models;

namespace PRM392_BookSoccerYard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly PRM392_BookSoccerYardContext _context;
        private IMapper _mapper;
        public CustomersController(PRM392_BookSoccerYardContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers()
        {
            var result = await _context.Customers.ToListAsync();
            return Ok(_mapper.Map<List<CustomerDTO>>(result));
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomer(int id)
            {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return _mapper.Map<CustomerDTO>(customer);
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, UpdatedCustomer customerDTO)
        {
            var customer = await _context.Customers.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (customer == null) { return Ok("Do not find this customer");}
            customer.UpdateDate = DateTime.Now;
            customer.FirstName = customerDTO.FirstName;
            customer.LastName = customerDTO.LastName;
            customer.Email = customerDTO.Email;
            customer.Password = customerDTO.Password;
            customer.PhoneNumber = customerDTO.PhoneNumber;
            customer.Status = customerDTO.Status;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("successfully");
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> PostCustomer(SignUp customerDTO)
        {
            var customer = _mapper.Map<Customer>(customerDTO);
            customer.CreateDate = DateTime.Now;
            customer.UpdateDate= DateTime.Now;
            customer.FirstName = "FirstName";
            customer.Img = "https://arc-anglerfish-washpost-prod-washpost.s3.amazonaws.com/public/VKPAOODQC52FBBEG6VUDMVRT6Y.jpg";
            customer.LastName = "LastName";
            customer.Status = true;
            await _context.Customers.AddAsync(customer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, _mapper.Map<CustomerDTO>(customer));
        }

        [HttpPost("/api/Customers/v2")]
        public async Task<ActionResult<CustomerDTO>> PostCustomer(CustomerDTO customerDTO)
        {
            var customer = _mapper.Map<Customer>(customerDTO);
            customer.CreateDate = DateTime.Now;
            customer.UpdateDate = DateTime.Now;
            customer.Status = true;
            await _context.Customers.AddAsync(customer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, _mapper.Map<CustomerDTO>(customer));
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
