using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Core.Models
{
    public class Unit
    {
        public int UnitId { get; set; }
        public string ArabicTitle{ get; set; }
        public string EnglishTitle { get; set; }


        public string ArabicDescription { get; set; }
        public string EnglishDescription { get; set; }

        public Employee? Head_Of_Unit { get; set; }

        public ICollection<Employee>? Employees { get; set; }
    }
}
