using AutoMapper;
using MAINPROJECT.AppDbContect;
using MAINPROJECT.DomaiLayer;
using MAINPROJECT.ModelDto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace MAINPROJECT.Servicelayer
{
    public class EmployeeRepo : IEmployee
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly string _connectionString;
        public EmployeeRepo(ApplicationDbContext context, IMapper mapper,IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _connectionString = configuration.GetConnectionString("DefaultConnection");

        }
        public async Task<EmployeeDto> CreateEmployeeAsync(EmployeeDto employeeDto)
        {
            var studentExists = await _context.Students.AnyAsync(s => s.Id == employeeDto.StudentId);

            if (!studentExists)
            {
                throw new Exception("StudentId does not exist in the Students table.");
            }

            var employeeEntity = _mapper.Map<Employee>(employeeDto);

        
            await _context.Employees.AddAsync(employeeEntity);

            await _context.SaveChangesAsync();

            return _mapper.Map<EmployeeDto>(employeeEntity);
        }



        public async Task<bool> DeleteAsync(int id)
        {
            var scc = await _context.Employees.FindAsync(id);
            if (scc == null)
                return false;
            var dd=   _context.Employees.Remove(scc);
            await _context.SaveChangesAsync();
            return true;
        }

        public  async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var ss=  await _context.Employees.ToListAsync();
            var ssmap= _mapper.Map<IEnumerable<EmployeeDto>>(ss);
            return ssmap;

        }

        public  async Task<EmployeeDto> GetByIdAsync(int id)
        {
            var scc = await _context.Employees.FindAsync(id);
            if (scc == null)
                return null;
            var sccmap = _mapper.Map<EmployeeDto>(scc);
            return sccmap;

        }

        public async Task<bool> Update(int id, EmployeeDto employee)
        {
            var stud = await _context.Employees.FindAsync(id);

            if (stud == null)
            {
                return false;
            }

            _mapper.Map<Employee>(employee);

            _context.Employees.Update(stud);
            await _context.SaveChangesAsync();

            return true;

        }
        public async Task<int> AddEmployeeAsync(Employee emp)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("AddEmployee5", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", emp.StudentId);
            cmd.Parameters.AddWithValue("@Name", emp.Name);
            cmd.Parameters.AddWithValue("@Gender", emp.Gender);
            cmd.Parameters.AddWithValue("@Salary", emp.Salary);
            
            SqlParameter outParam = new SqlParameter("@Id", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(outParam);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return (int)outParam.Value;


        }
    }
}
