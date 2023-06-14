using Demo.DAL.Entity;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace Demo.PL.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "name is required")]
        [MaxLength(50)]
        [MinLength(5)]
        public string Name { get; set; }
        [Range(22, 30, ErrorMessage = "Age must be between 22 and 30")]
        public int? Age { get; set; }
        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}$"
        , ErrorMessage = "Address must be like 123-street-city-country")]

        public string Adress { get; set; }
        [DataType(DataType.Currency)]
        [Range(4000, 7000)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
      
        //[ForeignKey("Department")]
        public int? DepartmentId { get; set; } // not allow null
        // Navigational prop [One]
        public Department Department { get; set; }

        public IFormFile  Image { get; set; }
        public string ImageName { get; set; }

    }
}
