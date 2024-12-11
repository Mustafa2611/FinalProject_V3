using FinalProject.Core.Models;
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

namespace FinalProject.EF.RepositoriesImplementation
{
    public class CourseRepositoryImp : BaseRepositoryImp<Course>, ICourseRepository
    {
        private readonly ApplicationDbContext _context;
        public CourseRepositoryImp(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public string UploadCourseFile(IFormFile PdfFile, int? CourseId)
        {
            var Course = _context.Courses.Find(CourseId);
            //if (employee == null) return null;
            _context.Entry(Course).State = EntityState.Detached;

            var validExtentions = new List<string>() { ".pdf" };
            var extention = Path.GetExtension(PdfFile.FileName);
            if (!validExtentions.Contains(extention)) return $"Extention is not valid {string.Join(',', validExtentions)}";

            long size = PdfFile.Length;
            if (size > (5 * 1024 * 1024))
                return $"Maximum size can be 5mb";

            //var filename = Guid.NewGuid().ToString() + extention;
            var filename = Guid.NewGuid() + Path.GetFileName(PdfFile.FileName);

            var path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            FileStream stream = new FileStream(Path.Combine(path, filename), FileMode.Create);

            PdfFile.CopyTo(stream);

            var relativePath = $"/uploads/{filename}";
            Course.PdfDescription = relativePath;

            _context.Courses.Attach(Course);
            _context.Entry(Course).Property(e => e.PdfDescription).IsModified = true;
            //employee.Resume = filename;
            _context.SaveChanges();
            return relativePath;
        }

        public string UploadCourseFile(IFormFile PdfFile, Course? Course)
        {

            var validExtentions = new List<string>() { ".pdf"};
            var extention = Path.GetExtension(PdfFile.FileName);
            if (!validExtentions.Contains(extention)) return $"Extention is not valid {string.Join(',', validExtentions)}";

            long size = PdfFile.Length;
            if (size > (5 * 1024 * 1024))
                return $"Maximum size can be 5mb";

            //var filename = Guid.NewGuid().ToString() + extention;
            var filename = Guid.NewGuid() + Path.GetFileName(PdfFile.FileName);

            var path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            FileStream stream = new FileStream(Path.Combine(path, filename), FileMode.Create);

            PdfFile.CopyTo(stream);

            var relativePath = $"/uploads/{filename}";
            Course.PdfDescription = relativePath;
            //Course.Resume = filename;

            _context.Courses.Attach(Course);
            _context.Entry(Course).Property(e => e.PdfDescription).IsModified = true;

            _context.SaveChanges();
            return relativePath;
        }
    }
}