using ClassTracking.Server.Services.ClassService;
using ClassTracking.Shared.Models;
using ClassTracking.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassTracking.Server.Controllers.API
{
    [Authorize(Roles = "Admin")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassesController(IClassService classService)
        {
            _classService = classService;
        }

        //GET: api/Classes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class>>> GetClasses()
        {
            return Ok(await _classService.GetClasses());
        }

        //GET: api/Classes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Class>> GetClass(int id)
        {
            return await _classService.GetClass(id);
        }

        //GET: api/Classes/GetClassInfo/5
        [HttpGet("{GetClassInfo}/{id}")]
        public async Task<ActionResult<ViewClass>> GetClassInfo(int id)
        {
            return await _classService.GetClassInfo(id);
        }

        //PUT: api/Classes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult> PutClass(int id, Class cls)
        {
            if (id != cls.ClassId)
            {
                return BadRequest();
            }

            try
            {
                await _classService.PutClass(id, cls);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(id))
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

        //POST: api/Classes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Class>> PostClass(Class cls)
        {
            await _classService.PostClass(cls);
            return CreatedAtAction("GetClass", new { id = cls.ClassId }, cls);
        }

        //DELETE: api/Classes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Class>> DeleteClass(int id)
        {
            var cls = await _classService.DeleteClass(id);
            if (cls == null)
            {
                return NotFound();
            }
            return cls;
        }

        private bool ClassExists(int id)
        {
            return _classService.ClassExists(id);
        }
    }
}
