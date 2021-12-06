using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartMedCodeChallenge.Data;
using SmartMedCodeChallenge.Models;

namespace SmartMedCodeChallenge.Controllers.v1
{
    [Route("api/v1/medicines")]
    [ApiController]
    public class MedicinesController : ControllerBase, IContext<Medicine>
    {
        private readonly MedicineContext _context;

        public MedicinesController(MedicineContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("getall")]
        //Returns a list of Medicine objects from the database context
        public async Task<ActionResult<List<Medicine>>> GetAll()
        {
            return await _context.SmartMedMedicines.ToListAsync();
        }

        [HttpGet]
        [Route("get/{id}")]
        //Returns a specified Medicine object from the database context using the supplied Id
        public async Task<ActionResult<Medicine>> Get(long id)
        {
            var medicine = await _context.SmartMedMedicines.FirstAsync(med => med.Id == id);
            if (medicine == null)
            {
                return NotFound();
            }

            return medicine;
        }

        [HttpPost]
        [Route("add")]
        //Adds a new Medicine object to the database context
        public async Task<ActionResult<Medicine>> Post(Medicine medicine)
        {
            if (medicine.Quantity <= 0)
            {
                return BadRequest("Quantity must be greater than zero.");
            }

            _context.SmartMedMedicines.Add(medicine);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAll), new { id = medicine.Id }, medicine);
        }

        [HttpPost]
        [Route("delete/{id}")]
        //Removes a medicine object from the database context
        public async Task<IActionResult> Delete(long id)
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
