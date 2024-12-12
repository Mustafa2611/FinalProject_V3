using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Core.Dtos.EventDtos
{
    public class ArabicEventDto
    {
        public int EventId { get; set; }
        public string ArabicTitle { get; set; }
        public string ArabicDescription { get; set; }
        public string Image { get; set; }
        public DateTime Arabic_Event_Start_Date { get; set; }

    }
}
