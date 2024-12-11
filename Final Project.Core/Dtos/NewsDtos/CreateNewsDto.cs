using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Core.Dtos.NewsDtos
{
    public class CreateNewsDto
    {
        public string ArabicTitle { get; set; }
        public string EnglishTitle { get; set; }

        public string ArabicDescription { get; set; }
        public string EnglishDescription { get; set; }

        public IFormFile Image { get; set; }

        public DateTime Arabic_News_Date { get; set; }
        public DateTime English_News_Date { get; set; }

    }
}
