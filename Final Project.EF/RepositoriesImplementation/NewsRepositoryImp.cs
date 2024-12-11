using FinalProject.EF.Configuration;
using FinalProject.Core.IRepositories;
using FinalProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace FinalProject.EF.RepositoriesImplementation
{
    public class NewsRepositoryImp : BaseRepositoryImp<News> , INewsRepository
    {
        //public NewsRepositoryImp(ApplicationDbContext context) : base(context) {

        //}
        private readonly ApplicationDbContext _context;
        public NewsRepositoryImp(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }

        public async Task<IEnumerable<News>> GetLastFourAsync()
        {
            var entitys = _context.Set<News>().OrderByDescending(n=> n.English_News_Date).Take(4).ToListAsync();
            return await entitys;
        }

        public string UploadNewsImage(IFormFile Image, int? NewsId)
        {
            var News = _context.News.Find(NewsId);
            //if (employee == null) return null;
            _context.Entry(News).State = EntityState.Detached;

            var validExtentions = new List<string>() { ".jpg", ".png", ".jpeg", ".bmp", ".webp" };
            var extention = Path.GetExtension(Image.FileName);
            if (!validExtentions.Contains(extention)) return $"Extention is not valid {string.Join(',', validExtentions)}";

            long size = Image.Length;
            if (size > (5 * 1024 * 1024))
                return $"Maximum size can be 5mb";

            //var filename = Guid.NewGuid().ToString() + extention;
            var filename = Guid.NewGuid() + Path.GetFileName(Image.FileName);

            var path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            FileStream stream = new FileStream(Path.Combine(path, filename), FileMode.Create);

            Image.CopyTo(stream);

            News.Image = filename;
            _context.SaveChanges();

            return News.Image;
        }

        public string UploadNewsImage(IFormFile Image, News? News)
        {
            var validExtentions = new List<string>() { ".jpg", ".png", ".jpeg", ".bmp", ".webp" };
            var extention = Path.GetExtension(Image.FileName);
            if (!validExtentions.Contains(extention)) return $"Extention is not valid {string.Join(',', validExtentions)}";

            long size = Image.Length;
            if (size > (5 * 1024 * 1024))
                return $"Maximum size can be 5mb";

            //var filename = Guid.NewGuid().ToString() + extention;
            var filename = Guid.NewGuid() + Path.GetFileName(Image.FileName);

            var path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            FileStream stream = new FileStream(Path.Combine(path, filename), FileMode.Create);

            Image.CopyTo(stream);

            News.Image = filename;
            _context.SaveChanges();

            return News.Image;
        }
    }
}
