using MAINPROJECT.ModelDto;
using MAINPROJECT.Servicelayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MAINPROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartment _deprepo;
        private readonly ILogger _logger;
        public DepartmentController(IDepartment deprepo, ILogger logger)
        {
            _deprepo = deprepo;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<DepartmentDto>> GetAll()
        {
            var aa = await _deprepo.GetAllAsync();
            return Ok(aa);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDto>> GetById(int id)
        {
            var iddd = await _deprepo.GetByIdAsync(id);
            _logger.LogInformation("The id is not fount log error");
                
            return Ok(iddd);
        }
        [HttpPost]
        public async Task<ActionResult> Create(DepartmentDto department)
        {
            var ccc = await _deprepo.CreateStudentAsync(department);
            return Ok(ccc);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var dd= await _deprepo.DeleteAsync(id);
            return Ok(dd);
        }
    }
}
