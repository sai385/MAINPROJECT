using AutoMapper;
using MAINPROJECT.AppDbContect;
using MAINPROJECT.Datamemoryin_seperate;
using MAINPROJECT.DomaiLayer;
using MAINPROJECT.ModelDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MAINPROJECT.Servicelayer
{
    public class StudentRepo : IStudent
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public StudentRepo(ApplicationDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;



        }
        
       
    public  async Task<StudentDto> CreateStudentAsync(StudentDto studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);

           
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

          
            return _mapper.Map<StudentDto>(student);

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return false;

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return true;

        }

        public string GenerateToken(string username)
        {
           
            
             var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JwtSettings:ExpiryMinutes"])),
                    Issuer = _configuration["JwtSettings:Issuer"],
                    Audience = _configuration["JwtSettings:Audience"],
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
        
    

        public async Task<IEnumerable<StudentDto>> GetAllAsync()
        {
            var stu =  await _context.Students.ToListAsync();
            var studto=_mapper.Map<IEnumerable<StudentDto>>(stu);
            return studto;
          
        }

        public async Task<StudentDto> GetByIdAsync(int id)
        {
            var stu = await _context.Students.FindAsync(id);
            if(stu == null)
            {
                return null;
            }
            var studto=_mapper.Map<StudentDto>(stu);
            return studto;
        }

        public async Task<bool> Update(int id, StudentDto student)
        {
            var stud =   await _context.Students.FindAsync(id);

            if (stud == null)
            {
                return false;
            }
            
            _mapper.Map<StudentDto>(student);

            _context.Students.Update(stud);
            await _context.SaveChangesAsync();

            return true;

        }
    }
}
