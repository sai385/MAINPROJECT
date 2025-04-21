using MAINPROJECT.DomaiLayer;
using MAINPROJECT.ModelDto;
using MAINPROJECT.Servicelayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace MAINPROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _emprepo;
        public EmployeeController(IEmployee emprepo)
        {
            _emprepo = emprepo;
        }
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<EmployeeDto>> GetAll()
        {
            var gg = await _emprepo.GetAllAsync();
            return Ok(gg);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetById(int id)
        {
            var eid = await _emprepo.GetByIdAsync(id);
            return Ok(eid);

        }
        [HttpPost]
        public async Task<ActionResult> Create(EmployeeDto emp)
        {
            var pp = await _emprepo.CreateEmployeeAsync(emp);
            return Ok(pp);
        }
        [HttpPost("StoreProcedure")]
        public async Task<ActionResult> AddEmployee(EmployeeDto emp)
        {
           
            var employee = new Employee
            {
                StudentId = emp.StudentId,
                Name = emp.Name,
                Gender = emp.Gender,
                Salary = emp.Salary
            };

            var newEmployeeId = await _emprepo.AddEmployeeAsync(employee);

            return Ok(newEmployeeId);
        }

        [HttpDelete("{id}")]
        public async  Task<ActionResult> Delete(int id)
        {
            var dd= await _emprepo.DeleteAsync(id);
            return Ok(dd);
        }
    }
}
