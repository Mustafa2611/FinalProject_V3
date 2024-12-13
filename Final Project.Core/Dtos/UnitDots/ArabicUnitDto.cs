using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Core.Dtos.UnitDots
{
    public class ArabicUnitDto
    {
        public int UnitId { get; set; }
        public string ArabicTitle { get; set; }
        public string ArabicDescription { get; set; }
        public int HeadOfUnitId { get; set; }
        public string HeadOfUnitName { get; set; }
    }
}
