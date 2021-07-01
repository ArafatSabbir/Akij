using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AkijGroup.Models;

namespace AkijGroup.Controllers
{
    [Route("api/ColdDrinks")]
    [ApiController]
    public class ColdDrinksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ColdDrinksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ColdDrinks
        // get all product list
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ColdDrinks>>> GetColdDrinks()
        {
            return await _context.ColdDrinks.ToListAsync();
        }

        // GET: api/ColdDrinks/TotalPrice
        [HttpGet("TotalPrice")]
        public async Task<decimal> TotalPrice()
        {
            var total = await _context.ColdDrinks.Select(c => (c.Quantity * c.UnitPrice)).SumAsync();
            return total;
        }

        // GET: api/ColdDrinks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ColdDrinks>> GetColdDrinks(int id)
        {
            var coldDrinks = await _context.ColdDrinks.FindAsync(id);

            if (coldDrinks == null)
            {
                return NotFound();
            }

            return coldDrinks;
        }


        // GET: api/ColdDrinks/GetColdDrinksByName/mojo
        [HttpGet("GetColdDrinksByName/{coldDrinksName}")]
        public async Task<ActionResult<ColdDrinks>> GetColdDrinksByName(string coldDrinksName)
        {
            var coldDrinks = await _context.ColdDrinks.FirstOrDefaultAsync(c => c.ColdDrinksName == coldDrinksName);

            if (coldDrinks == null)
            {
                return NotFound();
            }

            return coldDrinks;
        }


        //PUT: api/ColdDrinks/PutColdDrinks
        [HttpPut("PutColdDrinks")]
        public async Task<IActionResult> PutColdDrinks(ColdDrinks coldDrinks)
        {

            _context.Entry(coldDrinks).State = EntityState.Modified;

            
            await _context.SaveChangesAsync();
            return NoContent();
        }


        // Create: api/ColdDrinks/CreateColdDrinks
        [HttpPost("CreateColdDrinks")]
        public async Task<ActionResult<ColdDrinks>> CreateColdDrinks(ColdDrinks coldDrinks)
        {
            _context.ColdDrinks.Add(coldDrinks);
            await _context.SaveChangesAsync();

            return StatusCode(200);
        }



        // DELETE: api/ColdDrinks/DeleteColdDrinks/3
        [HttpDelete("DeleteColdDrinks/{id}")]
        public async Task<ActionResult<ColdDrinks>> DeleteColdDrinks(int id)
        {
            var coldDrinks = await _context.ColdDrinks.FindAsync(id);
            if (coldDrinks == null)
            {
                return NotFound();
            }

            _context.ColdDrinks.Remove(coldDrinks);
            await _context.SaveChangesAsync();

            return coldDrinks;
        }



        // DELETE: api/ColdDrinks/DeleteColdDrinksByName/Speed
        [HttpDelete("DeleteColdDrinksByName/{coldDrinksName}")]
        public async Task<ActionResult<ColdDrinks>> DeleteColdDrinksByName(string coldDrinksName)
        {
            var coldDrinks = await _context.ColdDrinks.FirstOrDefaultAsync(c => c.ColdDrinksName == coldDrinksName);
            if (coldDrinks == null)
            {
                return NotFound();
            }

            _context.ColdDrinks.Remove(coldDrinks);
            await _context.SaveChangesAsync();

            return coldDrinks;
        }


        // DELETE: api/ColdDrinks/DeleteColdDrinksOfLessQuantity/500
        [HttpDelete("DeleteColdDrinksOfLessQuantity/{quantity}")]
        public async Task<int> DeleteColdDrinksOfLessQuantity(int quantity)
        {
            var records = await _context.ColdDrinks.Where(c => c.Quantity < quantity).ToListAsync();
            if (!records.Any())
            {
                throw new Exception(".NET ObjectNotFoundException");
            }
            foreach (var record in records)
            {
                _context.ColdDrinks.Remove(record);
            }
            return await _context.SaveChangesAsync();
        }

        private bool ColdDrinksExists(int id)
        {
            return _context.ColdDrinks.Any(e => e.ColdDrinksId == id);
        }
    }
}
