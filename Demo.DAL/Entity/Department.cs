using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entity
{
    public class Department
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="code is required!")]
        public string Code { get; set; }
        [Required (ErrorMessage = "code is required!")]
        [MaxLength(50,ErrorMessage ="max length is 50 Char")]
        public string Name { get; set; }
        public DateTime DateofCreation { get; set; }

       
      
        // Navigational prop [Many]
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
