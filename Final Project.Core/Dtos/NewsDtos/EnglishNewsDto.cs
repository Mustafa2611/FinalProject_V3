using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Core.Dtos.NewsDtos
{
    public class EnglishNewsDto
    {
        public int NewsId { get; set; }
        public string EnglishTitle { get; set; }
        public string EnglishDescription { get; set; }
        public DateTime English_News_Date { get; set; }
        public string Image { get; set; }

    }
}
