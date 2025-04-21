using MAINPROJECT.AppDbContect;
using MAINPROJECT.DomaiLayer;
using MAINPROJECT.ModelDto;
using MAINPROJECT.Servicelayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace MAINPROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudent _sturepo;
        private readonly IConfiguration _configuration;
        public StudentController(IStudent sturepo, IConfiguration configuration)
        {
            _sturepo = sturepo;
            _configuration = configuration;
        }
        [EnableQuery]
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<StudentDto>> GetAllAsync()
        {
            var ss = await _sturepo.GetAllAsync();
            return Ok(ss);
        }
       
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> Getbyid(int id)
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var ss = await _sturepo.GetByIdAsync(id);

            if (ss == null)
                throw new Exception("The Id with null is not found");
            return Ok(ss);
        }
        [HttpPost]
        public async Task<ActionResult> Create(StudentDto student)
        {
            var ss = await _sturepo.CreateStudentAsync(student);
            return Ok(ss);

        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] User model)
        {
            if (model.Username == "admin" && model.Password == "password")
            {
            
                var token = _sturepo.GenerateToken(model.Username);

                return Ok(new { Token = token });
            }

            return Unauthorized();
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] StudentDto student)
        {
            var result = await _sturepo.Update(id, student);

            if (result == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }

            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Deleate(int id)
        {
            var dd= await _sturepo.DeleteAsync(id);
            return Ok(dd);
        }
    }
}
