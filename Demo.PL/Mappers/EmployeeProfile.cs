using AutoMapper;
using Demo.DAL.Entity;
using Demo.PL.Models;

namespace Demo.PL.Mappers
{
    public class EmployeeProfile :Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
        
    }
}
