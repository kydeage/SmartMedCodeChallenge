using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartMedCodeChallenge.Models;

namespace SmartMedCodeChallenge.Controllers
{
    [Route("api")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly MedicineContext _context;

        public MedicinesController(MedicineContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("getmedicines")]
        //Returns a list of Medicine objects from the database context
        public async Task<ActionResult<IEnumerable<Medicine>>> GetMedicines()
        {
            return await _context.SmartMedMedicines.ToListAsync();
        }

        [HttpPost]
        [Route("createmedicine")]
        //Adds a new Medicine object to the database context
        public async Task<ActionResult<Medicine>> PostMedicine(Medicine medicine)
        {
            if (medicine.Quantity <= 0)
            {
                return BadRequest("Quantity must be greater than zero.");
            }

            _context.SmartMedMedicines.Add(medicine);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMedicines), new { id = medicine.Id }, medicine);
        }

        [HttpPost]
        [Route("deletemedicine/{id}")]
        //Removes a medicine object from the database context
        public async Task<IActionResult> DeleteMedicine(long id)
        {
            var medicine = await _context.SmartMedMedicines.FirstAsync(med => med.Id == id);
            if (medicine == null)
            {
                return NotFound();
            }

            _context.SmartMedMedicines.Remove(medicine);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
