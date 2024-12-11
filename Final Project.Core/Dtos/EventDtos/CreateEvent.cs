using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Core.Dtos.EventDtos
{
    public class CreateEvent
    {
        //public string Name { get; set; }
        //public string Description { get; set; }
        //public DateTime Event_Start_Date { get; set; }
        //public IFormFile Image { get; set; }


        public string ArabicTitle { get; set; }
        public string EnglishTitle { get; set; }

        public string ArabicDescription { get; set; }
        public string EnglishDescription { get; set; }

        public IFormFile Image { get; set; }

        public DateTime Arabic_Event_Start_Date { get; set; }
        public DateTime English_Event_Start_Date { get; set; }
    }
}
