using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FinalProject.Core.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public string ArabicJob_Title { get; set; }
        public string EnglishJob_Title { get; set; }

        public string Resume { get; set; }
        public string Image {  get; set; }
        //[ForeignKey("Department")]
        //public int DepartmentId { get; set; }
        [JsonIgnore]
        public Department? Department { get; set; }

        //public int UnitId { get; set; }
        [JsonIgnore]
        public Unit? Unit { get; set; }
    }
}
