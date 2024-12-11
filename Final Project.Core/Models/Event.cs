using FinalProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Core.Models
{
    public class Event
    {//dev
        public int EventId { get; set; }
        public string ArabicTitle { get; set; }
        public string EnglishTitle { get; set; }

        public string ArabicDescription { get; set; }
        public string EnglishDescription { get; set; }

        public string Image { get; set; }

        public DateTime Arabic_Event_Start_Date { get; set; }
        public DateTime English_Event_Start_Date { get; set; }


    }
}
