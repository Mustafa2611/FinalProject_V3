﻿using FinalProject.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Core.IRepositories
{
    public interface ICourseRepository : IBaseRepository<Course>
    {


        public string UploadCourseFile(IFormFile PdfFile, int? CourseId);
        public string UploadCourseFile(IFormFile PdfFile, Course? Course);

        //Task<IEnumerable<Course>> GetAllAsync();
        //Task<Course> GetByIdAsync(int id);
        //Task<bool> AddAsync(Course course);
        //Task<bool> UpdateAsync(Course course);
        //Task<bool> DeleteAsync(int id);
    }
}
