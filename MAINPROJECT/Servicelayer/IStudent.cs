using MAINPROJECT.ModelDto;
using Microsoft.AspNetCore.Mvc;

namespace MAINPROJECT.Servicelayer
{
    public interface IStudent
    {
        Task<IEnumerable<StudentDto>> GetAllAsync();
        Task<StudentDto> GetByIdAsync(int id);
        Task<bool> Update(int id,StudentDto student);
        Task<bool> DeleteAsync(int id);
        Task<StudentDto> CreateStudentAsync(StudentDto studentDto);
        string GenerateToken(string username);
    }
}
