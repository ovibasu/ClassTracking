using ClassTracking.Server.Services.StudentService;
using ClassTracking.Shared.Models;
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
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        //GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return Ok(await _studentService.GetStudents());
        }

        //GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            return await _studentService.GetStudent(id);
        }

        //GET: api/Students/GetEnrollmentCountForStudent/5
        [HttpGet("{GetEnrollmentCountForStudent}/{id}")]
        public async Task<ActionResult<int>> GetEnrollmentCountForStudent(int id)
        {
            return await _studentService.GetEnrollmentCountForStudent(id);
        }

        //GET: api/Students/GetTotalStudentInClass/Count/5
        [HttpGet("{GetTotalStudentInClass}/{Count}/{id}")]
        public async Task<ActionResult<int>> GetTotalStudentInClass(int id)
        {
            return await _studentService.GetTotalStudentInClass(id);
        }

        //PUT: api/Students/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult> PutStudent(int id, Student student)
        {
            if (id != student.StudentId)
            {
                return BadRequest();
            }

            try
            {
                await _studentService.PutStudent(id, student);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        //POST: api/Students
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            await _studentService.PostStudent(student);
            return CreatedAtAction("GetStudent", new { id = student.StudentId }, student);
        }

        //DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var student = await _studentService.DeleteStudent(id);
            if (student == null)
            {
                return NotFound();
            }
            return student;
        }

        private bool StudentExists(int id)
        {
            return _studentService.StudentExists(id);
        }
    }
}
