using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Core.Dtos.EventDtos
{
    public class EnglishEventDto
    {
        public int EventId { get; set; }
        public string EnglishTitle { get; set; }
        public string EnglishDescription { get; set; }
        public DateTime English_Event_Start_Date { get; set; }
        public string Image { get; set; }

    }
}
