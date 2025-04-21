using MAINPROJECT.ModelDto;
using Microsoft.AspNetCore.Mvc;

namespace MAINPROJECT.Servicelayer
{
    public interface IDepartment
    {
        Task<IEnumerable<DepartmentDto>> GetAllAsync();
        Task<DepartmentDto> GetByIdAsync(int id);
        //Task<bool> Update(int id, DepartmentDto department);
        Task<bool> DeleteAsync(int id);
        Task<DepartmentDto> CreateStudentAsync(DepartmentDto department);
            
    }
}
