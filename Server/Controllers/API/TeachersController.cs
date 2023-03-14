using ClassTracking.Server.Services.TeacherService;
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
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeachersController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        //GET: api/Teachers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers()
        {
            return Ok(await _teacherService.GetTeachers());
        }

        //GET: api/Teachers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacher(int id)
        {
            return await _teacherService.GetTeacher(id);
        }

        //GET: api/Teachers/GetAssignTecher/5
        [HttpGet("{GetAssignTecher}/{id}")]
        public async Task<ActionResult<int>> GetAssignTecher(int id)
        {
            return await _teacherService.GetAssignTecher(id);
        }

        //GET: api/Teachers/GetTotalTeacherInClass/Count/5
        [HttpGet("{GetTotalTeacherInClass}/{Count}/{id}")]
        public async Task<ActionResult<int>> GetTotalTeacherInClass(int id)
        {
            return await _teacherService.GetTotalTeacherInClass(id);
        }

        //PUT: api/Teachers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult> PutTeacher(int id, Teacher teacher)
        {
            if (id != teacher.TeacherId)
            {
                return BadRequest();
            }

            try
            {
                await _teacherService.PutTeacher(id, teacher);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
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

        //POST: api/Teachers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher(Teacher teacher)
        {
            await _teacherService.PostTeacher(teacher);
            return CreatedAtAction("GetTeacher", new { id = teacher.TeacherId }, teacher);
        }

        //DELETE: api/Teachers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Teacher>> DeleteTeacher(int id)
        {
            var teacher = await _teacherService.DeleteTeacher(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return teacher;
        }

        private bool TeacherExists(int id)
        {
            return _teacherService.TeacherExists(id);
        }
    }
}
