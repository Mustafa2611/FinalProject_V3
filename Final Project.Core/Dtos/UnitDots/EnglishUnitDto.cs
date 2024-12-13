using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Core.Dtos.UnitDots
{
    public class EnglishUnitDto
    {
        public int UnitId { get; set; }
        public string EnglishTitle { get; set; }
        public string EnglishDescription { get; set; }

        public int HeadOfUnitId { get; set; }
        public string HeadOfUnitName { get; set; }

    }
}
