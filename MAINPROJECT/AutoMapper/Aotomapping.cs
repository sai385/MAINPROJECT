using AutoMapper;
using MAINPROJECT.DomaiLayer;
using MAINPROJECT.ModelDto;

namespace MAINPROJECT.AutoMapper
{
    public class Aotomapping:Profile
    {
        public Aotomapping()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Student,StudentDto>().ReverseMap();

            CreateMap<Department, DepartmentDto>().ReverseMap()
                .ForMember(dest => dest.DepName, opt => opt.MapFrom(src => src.DepatmentName));
        }
    }
}
