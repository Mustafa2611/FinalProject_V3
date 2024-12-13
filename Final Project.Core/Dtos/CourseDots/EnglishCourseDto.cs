using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Core.Dtos.CourseDots
{
    public class EnglishCourseDto
    {
        public int CourseId { get; set; }
        public string EnglishTitle { get; set; }
        public string LevelYear { get; set; }
        public string PdfDescription { get; set; }
        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }

    }
}
