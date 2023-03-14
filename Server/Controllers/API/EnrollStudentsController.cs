using ClassTracking.Server.Services.EnrollStudentService;
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
    public class EnrollStudentsController : ControllerBase
    {
        private readonly IEnrollStudentService _enrollStudentService;

        public EnrollStudentsController(IEnrollStudentService enrollStudentService)
        {
            _enrollStudentService = enrollStudentService;
        }

        //GET: api/EnrollStudents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnrollStudent>>> GetEnrollStudents()
        {
            return Ok(await _enrollStudentService.GetEnrollStudents());
        }

        //GET: api/EnrollStudents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EnrollStudent>> GetEnrollStudent(int id)
        {
            return await _enrollStudentService.GetEnrollStudent(id);
        }

        //GET: api/EnrollStudents/GetStudentInfo/5
        [HttpGet("{GetStudentInfo}/{id}")]
        public async Task<ActionResult<IEnumerable<ViewStudent>>> GetStudentInfo(int id)
        {
            return Ok(await _enrollStudentService.GetStudentInfo(id));
        }

        //PUT: api/EnrollStudents/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult> PutEnrollStudent(int id, EnrollStudent enrollStudent)
        {
            if (id != enrollStudent.EnrollStudentId)
            {
                return BadRequest();
            }

            try
            {
                await _enrollStudentService.PutEnrollStudent(id, enrollStudent);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnrollStudentExists(id))
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

        //POST: api/EnrollStudents
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<EnrollStudent>> PostEnrollStudent(EnrollStudent enrollStudent)
        {
            await _enrollStudentService.PostEnrollStudent(enrollStudent);
            return CreatedAtAction("GetEnrollStudent", new { id = enrollStudent.EnrollStudentId }, enrollStudent);
        }

        //DELETE: api/EnrollStudents/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EnrollStudent>> DeleteEnrollStudent(int id)
        {
            var enrollStudent = await _enrollStudentService.DeleteEnrollStudent(id);
            if (enrollStudent == null)
            {
                return NotFound();
            }
            return enrollStudent;
        }

        private bool EnrollStudentExists(int id)
        {
            return _enrollStudentService.EnrollStudentExists(id);
        }
    }
}
