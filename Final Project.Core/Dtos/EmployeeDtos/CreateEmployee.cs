using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Core.Dtos.EmployeeDots
{
    public class EmployeeCreateDto
    {
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ArabicJob_Title { get; set; }
        public string EnglishJob_Title { get; set; }
        public IFormFile Resume { get; set; }
        public IFormFile Image { get; set; }
        //public int DepartmentId { get; set; }
        //public int? UnitId { get; set; }
    }

   
}
