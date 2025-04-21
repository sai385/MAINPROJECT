using MAINPROJECT.DomaiLayer;
using MAINPROJECT.ModelDto;
using Microsoft.AspNetCore.Mvc;

namespace MAINPROJECT.Servicelayer
{
    public interface IEmployee
    {
        Task<IEnumerable<EmployeeDto>> GetAllAsync();
        Task<EmployeeDto> GetByIdAsync(int id);
        Task<bool> Update(int id,EmployeeDto employee);
        Task<bool> DeleteAsync(int id);
        Task<EmployeeDto> CreateEmployeeAsync(EmployeeDto employeeDto);
        Task<int> AddEmployeeAsync(Employee emp);
    }
}
