using ClassTracking.Server.Services.AssignTeacherService;
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
    public class AssignTeachersController : ControllerBase
    {
        private readonly IAssignTeacherService _assignTeacherService;

        public AssignTeachersController(IAssignTeacherService assignTeacherService)
        {
            _assignTeacherService = assignTeacherService;
        }

        //GET: api/AssignTeachers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssignTeacher>>> GetAssignTeachers()
        {
            return Ok(await _assignTeacherService.GetAssignTeachers());
        }

        //GET: api/AssignTeachers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AssignTeacher>> GetAssignTeacher(int id)
        {
            return await _assignTeacherService.GetAssignTeacher(id);
        }

        //GET: api/AssignTeachers/GetTeacherInfo/5
        [HttpGet("{GetTeacherInfo}/{id}")]
        public async Task<ActionResult<ViewTeacher>> GetTeacherInfo(int id)
        {
            return await _assignTeacherService.GetTeacherInfo(id);
        }

        //PUT: api/AssignTeachers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAssignTeacher(int id, AssignTeacher assignTeacher)
        {
            if (id != assignTeacher.AssignTeacherId)
            {
                return BadRequest();
            }

            try
            {
                await _assignTeacherService.PutAssignTeacher(id, assignTeacher);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssignTeacherExists(id))
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

        //POST: api/AssignTeachers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AssignTeacher>> PostAssignTeacher(AssignTeacher assignTeacher)
        {
            await _assignTeacherService.PostAssignTeacher(assignTeacher);
            return CreatedAtAction("GetAssignTeacher", new { id = assignTeacher.AssignTeacherId }, assignTeacher);
        }

        //DELETE: api/AssignTeachers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AssignTeacher>> DeleteAssignTeacher(int id)
        {
            var assignTeacher = await _assignTeacherService.DeleteAssignTeacher(id);
            if (assignTeacher == null)
            {
                return NotFound();
            }
            return assignTeacher;
        }

        private bool AssignTeacherExists(int id)
        {
            return _assignTeacherService.AssignTeacherExists(id);
        }
    }
}
