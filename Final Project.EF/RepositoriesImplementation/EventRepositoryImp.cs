using FinalProject.Core.Models;
using FinalProject.EF.Configuration;
using FinalProject.Core.IRepositories;
using FinalProject.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FinalProject.EF.RepositoriesImplementation
{
    public class EventRepositoryImp : BaseRepositoryImp<Event> , IEventRepository
    {
        //public EventRepositoryImp(ApplicationDbContext context) : base(context)
        //{
        //}
        private readonly ApplicationDbContext _context;
        public EventRepositoryImp(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public string UploadEventImage(IFormFile Image, int? EventId)
        {
            var Event = _context.Events.Find(EventId);
            //if (employee == null) return null;
            _context.Entry(Event).State = EntityState.Detached;

            var validExtentions = new List<string>() { ".jpg", ".png" ,".jpeg" , ".bmp" , ".webp"};
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

            Event.Image = filename;
            _context.SaveChanges();

            return Event.Image;
        }

        public string UploadEventImage(IFormFile Image, Event? Event)
        {
            var validExtentions = new List<string>() { ".pdf", ".docx" };
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

            Event.Image= filename;
            _context.SaveChanges();

            return Event.Image;
        }
    }
}
