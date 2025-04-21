using AutoMapper;
using MAINPROJECT.AppDbContect;
using MAINPROJECT.Datamemoryin_seperate;
using MAINPROJECT.DomaiLayer;
using MAINPROJECT.ModelDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MAINPROJECT.Servicelayer
{
    public class DepartmentRepo : IDepartment
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public DepartmentRepo(ApplicationDbContext context, IMapper mapper)//hhhhhhhh
        {
            _context = context;

            _mapper = mapper;

        }
        public  async Task<DepartmentDto> CreateStudentAsync(DepartmentDto department)
        {
            var dpp = _mapper.Map<Department>(department);


             await  _context.Departments.AddAsync(dpp);

            await _context.SaveChangesAsync();


            return _mapper.Map<DepartmentDto>(dpp);
        }

        public  async Task<bool> DeleteAsync(int id)
        {
            var iii = await _context.Departments.FindAsync(id);
            if (iii == null)
                return false;
            var ddd =  _context.Departments.Remove(iii);
            await _context.SaveChangesAsync();
            return true;

        }

        public  async Task<IEnumerable<DepartmentDto>> GetAllAsync()
        {
            var gg =  await _context.Departments.ToListAsync();
            var mm= _mapper.Map<IEnumerable<DepartmentDto>>(gg);
            return mm;

        }

        public  async Task<DepartmentDto> GetByIdAsync(int id)
        {
            var iii = await _context.Departments.FindAsync(id);
            if (iii == null)
                return null;
            var dd = _mapper.Map<DepartmentDto>(iii);
            return dd;


        }

        //public Task<bool> Update(int id, DepartmentDto department)
        //{

        //}
    }
}
