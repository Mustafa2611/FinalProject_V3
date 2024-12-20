﻿using FinalProject.Core.Dtos.CourseDtos;
using FinalProject.Core.Models;
using FinalProject.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using FinalProject.Core.Models;
using FinalProject.Core.Dtos.CollegeDots;
using Microsoft.AspNetCore.Http;
using FinalProject.EF.Migrations;
using System.Collections.Generic;
using FinalProject.EF;
using FinalProject.Core;
using FinalProject.Core.Dtos.CourseDots;
namespace FinalProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // POST: api/Course/Create_Course
        [HttpPost("/Create_Course")]
        public async Task<ActionResult> Create(CreateCourseDto courseDto)
        {
            Course course = new Course()
            {
                ArabicTitle = courseDto.ArabicTitle,
                EnglishTitle= courseDto.EnglishTitle,
                LevelYear = courseDto.LevelYear,
                //Description = courseDto.Description,
                DepartmentId = courseDto.DepartmentId,
                //Department =await _unitOfWork.Departments.GetByIdAsync(d => d.DepartmentId == courseDto.DepartmentId)
            };
            course.PdfDescription = _unitOfWork.Courses.UploadCourseFile(courseDto.PdfDescription, course);
            var AddCourse = await _unitOfWork.Courses.AddAsync(course);
            if ( AddCourse == null) return BadRequest("Add Course operation failed ");

            await _unitOfWork.CompleteAsync();
            return Ok(course);
        }

        // GET: api/Course/Get_Course_By_Id/{id}
        [HttpGet("/Get_Course_By_Id/{id}")]
        public async Task<ActionResult> Get(int id , string lang)
        {
            Course course =await _unitOfWork.Courses.GetByIdAsync(c => c.CourseId == id, new[] { "Department" });
            if (course == null)
                return NotFound("Course not found");

            switch (lang)
            {
                case "Ar":
                    {
                        var arabicCourse = new ArabicCourseDto()
                        {
                            CourseId = course.CourseId,
                            ArabicTitle= course.ArabicTitle,
                            LevelYear = course.LevelYear,
                            PdfDescription= course.PdfDescription,
                            DepartmentId = course.Department.DepartmentId,
                            DepartmentName = course.Department.ArabicTitle,
                        };
                        return Ok(arabicCourse);
                    }
                case "En":
                    {
                        var englishCourse = new EnglishCourseDto()
                        {
                            CourseId = course.CourseId,
                            EnglishTitle = course.EnglishTitle,
                            LevelYear = course.LevelYear,
                            PdfDescription = course.PdfDescription,
                            DepartmentId = course.Department.DepartmentId,
                            DepartmentName = course.Department.EnglishTitle,
                        };
                        return Ok(englishCourse);
                    }
            }

            return BadRequest();
        }

        // GET: api/Course/Get_All_Courses
        [HttpGet("/Get_All_Courses")]
        public async Task<ActionResult> GetAll(string lang)
        {
            IEnumerable<Course> courses =await _unitOfWork.Courses.GetAllAsync(null,new[] { "Department" });
            if (courses == null) return NotFound("There is no course created yet.");

            switch (lang)
            {
                case "Ar":
                    {
                        IEnumerable<ArabicCourseDto> arabicCourses = new List<ArabicCourseDto>();
                        foreach (var course in courses)
                            arabicCourses.Append(new ArabicCourseDto()
                            {
                                CourseId = course.CourseId,
                                ArabicTitle = course.ArabicTitle,
                                LevelYear = course.LevelYear,
                                PdfDescription = course.PdfDescription,
                                DepartmentId = course.Department.DepartmentId,
                                DepartmentName = course.Department.ArabicTitle,

                            });
                        return Ok(arabicCourses);
                    }
                case "En":
                    {
                        IEnumerable<EnglishCourseDto> englishCourses = new List<EnglishCourseDto>();
                        foreach (var course in courses)
                            englishCourses.Append(new EnglishCourseDto()
                            {
                                CourseId = course.CourseId,
                                EnglishTitle = course.EnglishTitle,
                                LevelYear = course.LevelYear,
                                PdfDescription = course.PdfDescription,
                                DepartmentId = course.Department.DepartmentId,
                                DepartmentName = course.Department.EnglishTitle,

                            });
                        return Ok(englishCourses);
                    }
            }

            return BadRequest();
        }

        // PUT: api/Course/Update_Course
        [HttpPut("/Update_Course")]
        public async Task<ActionResult> Update(UpdateCourseDto courseDto)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(c => c.CourseId == courseDto.CourseId, new[] { "Department" })
;            if ( course == null) return NotFound("Course not found");

            var department = await _unitOfWork.Departments.GetByIdAsync(d => d.DepartmentId == courseDto.DepartmentId);
            if (department == null) return NotFound("Department not found");

             course = new Course()
            {
                CourseId = courseDto.CourseId,
                 ArabicTitle = courseDto.ArabicTitle,
                 EnglishTitle = courseDto.EnglishTitle,
                 LevelYear = courseDto.LevelYear,

                 PdfDescription = _unitOfWork.Courses.UploadCourseFile(courseDto.PdfDescription, courseDto.CourseId),
                 DepartmentId = courseDto.DepartmentId,
                //Department = await _unitOfWork.Departments.GetByIdAsync(d => d.DepartmentId == courseDto.DepartmentId)

            };


            var UpdateCourse = await _unitOfWork.Courses.UpdateAsync(course);
            if (UpdateCourse == null) return BadRequest("Update course operation failed");
            await _unitOfWork.CompleteAsync();
            return Ok(course);
        }

        // DELETE: api/Course/Delete_Course/{id}
        [HttpDelete("/Delete_Course/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Course course =await _unitOfWork.Courses.GetByIdAsync(c => c.CourseId == id, new[] { "Department" });
            if (course == null) return NotFound("Course Not Found");

            var DeleteCourse = await _unitOfWork.Courses.DeleteAsync(id);
            if (DeleteCourse == null) return BadRequest("Delete course operation failed");
            await _unitOfWork.CompleteAsync();
            return Ok(course);
        }
    }
}
