using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Core.Dtos.DepartmentDtos
{
    public class CreateDepartmentDto
    {
        //public int DepartmentId { get; set; }
        public string ArabicTitle { get; set; }
        public string EnglishTitle { get; set; }

        public string ArabicDescription { get; set; }
        public string EnglishDescription { get; set; }
        public int CollegeId { get; set; }
        public int HeadOfDepartmentId { get; set; }
        //public int EmployeeCount { get; set; }
        //public int CourseCount { get; set; }
    }
}



