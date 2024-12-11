using FinalProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Core.Models
{
    public class News
    {
        public int NewsId { get; set; }
        //public string Name { get; set; }
        //public string Description { get; set; }
        //public string img { get; set; }
        //public DateTime News_Date { get; set; }

        public string ArabicTitle { get; set; }
        public string EnglishTitle { get; set; }

        public string ArabicDescription { get; set; }
        public string EnglishDescription { get; set; }

        public string Image { get; set; }

        public DateTime Arabic_News_Date { get; set; }
        public DateTime English_News_Date { get; set; }


    }
}
