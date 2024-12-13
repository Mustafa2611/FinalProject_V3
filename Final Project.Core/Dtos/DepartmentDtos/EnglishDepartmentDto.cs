using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Core.Dtos.DepartmentDtos
{
    public class EnglishDepartmentDto
    {
        public int DepartmentId { get; set; }
        public string EnglishTitle { get; set; }
        public string EnglishDescription { get; set; }
        public int HeadOfDepartmentId { get; set; }
        public string HeadOfDepartmentName { get; set; }

    }
}
