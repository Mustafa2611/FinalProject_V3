using FinalProject.Core.Dtos.NewsDtos;
using FinalProject.Core.Models;
using FinalProject.Core;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using FinalProject.Core.Dtos.EmployeeDots;
using FinalProject.Core.Models;
using Microsoft.AspNetCore.Http;
using FinalProject.Core.Dtos.CollegeDots;
using System.Collections.Generic;
using FinalProject.EF;
using FinalProject.Core;
using FinalProject.EF.Migrations;
namespace FinalProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public NewsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("/Create_News")]
        public async Task<ActionResult> Create(CreateNewsDto NewsDto)
        {
            News News = new News()
            {
                ArabicTitle = NewsDto.ArabicTitle,
                EnglishTitle = NewsDto.EnglishTitle,
                ArabicDescription = NewsDto.ArabicDescription,
                EnglishDescription = NewsDto.EnglishDescription,
                Arabic_News_Date= NewsDto.Arabic_News_Date,
                English_News_Date = NewsDto.English_News_Date,

            };

            News.Image = _unitOfWork.News.UploadNewsImage(NewsDto.Image, News);


            var NewsAdded = await _unitOfWork.News.AddAsync(News);
            if ( NewsAdded == null)
                return BadRequest("Add News operation failed");

            await _unitOfWork.CompleteAsync();
            return Ok(News);
        }

        [HttpGet("/Get_News_By_Id/{id}")]
        public async Task<ActionResult> Get(int id , string lang)
        {
            var News = await _unitOfWork.News.GetByIdAsync(n => n.NewsId == id);
            if (News == null) return NotFound("News not found");

            switch (lang)
            {
                case "Ar":
                    {
                        ArabicNewsDto arabicNews = new ArabicNewsDto()
                        {
                            NewsId = News.NewsId,
                            ArabicTitle = News.ArabicTitle,
                            ArabicDescription = News.ArabicDescription,
                            Arabic_News_Date = News.Arabic_News_Date,
                            Image = News.Image,
                        };

                        return Ok(arabicNews);

                    }
                case "En":
                    {
                        EnglishNewsDto englishNews = new EnglishNewsDto()
                        {
                            NewsId = News.NewsId,
                            EnglishTitle = News.EnglishTitle,
                            EnglishDescription = News.EnglishDescription,
                            English_News_Date = News.English_News_Date,
                            Image = News.Image,
                        };

                        return Ok(englishNews);

                    }
            }

            return BadRequest();
        }
        [HttpGet("/Get_All_News")]
        public async Task<ActionResult> GetAll(string lang)
        {
            var AllNews = await _unitOfWork.News.GetAllAsync(null);
            if (AllNews == null) return NotFound("There is no news created");

            switch (lang)
            {
                case "Ar":
                    {
                        IEnumerable<ArabicNewsDto> arabicNews = new List<ArabicNewsDto>() { };
                        foreach (var news in AllNews)
                        {
                            arabicNews.Append(new ArabicNewsDto()
                            {
                                NewsId = news.NewsId,
                                ArabicTitle = news.ArabicTitle,
                                ArabicDescription = news.ArabicDescription,
                                Arabic_News_Date = news.Arabic_News_Date,
                                Image = news.Image,
                            });
                        }

                        return Ok(arabicNews);
                    }
                case "En":
                    {
                        IEnumerable<EnglishNewsDto> englishNews = new List<EnglishNewsDto>() { };
                        foreach (var news in AllNews)
                        {
                            englishNews.Append(new EnglishNewsDto()
                            {
                                NewsId = news.NewsId,
                                EnglishTitle = news.EnglishTitle,
                                EnglishDescription = news.EnglishDescription,
                                English_News_Date = news.English_News_Date,
                                Image = news.Image,
                            });
                        }

                        return Ok(englishNews);

                    }
            }

            return BadRequest();
        }



        //[HttpGet("/Get_Arabic_News_By_Id/{id}")]
        //public async Task<ActionResult> GetArabic(int id)
        //{
        //    var News = await _unitOfWork.News.GetByIdAsync(n => n.NewsId == id);
        //    if (News == null) return NotFound("News not found");

        //    ArabicNewsDto arabicNews = new ArabicNewsDto() { 
        //        NewsId = News.NewsId,
        //        ArabicTitle = News.ArabicTitle,
        //        ArabicDescription = News.ArabicDescription ,
        //        Arabic_News_Date = News.Arabic_News_Date,
        //        Image = News.Image,
        //    };

        //    return Ok(arabicNews);
        //}

        //[HttpGet("/Get_English_News_By_Id/{id}")]
        //public async Task<ActionResult> GetEnglish(int id)
        //{
        //    var News = await _unitOfWork.News.GetByIdAsync(n => n.NewsId == id);
        //    if (News == null) return NotFound("News not found");

        //    EnglishNewsDto englishNews = new EnglishNewsDto()
        //    {
        //        NewsId = News.NewsId,
        //        EnglishTitle = News.EnglishTitle,
        //        EnglishDescription = News.EnglishDescription,
        //        English_News_Date = News.English_News_Date,
        //        Image = News.Image,
        //    };

        //    return Ok(englishNews);
        //}

        //[HttpGet("/Get_All_Arabic_News")]
        //public async Task<ActionResult> GetAllArabic()
        //{
        //    var AllNews = await _unitOfWork.News.GetAllAsync(null);
        //    if (AllNews == null) return NotFound("There is no news created");

        //    IEnumerable<ArabicNewsDto> arabicNews = new List<ArabicNewsDto>() { };
        //    foreach (var news in AllNews)
        //    {
        //        arabicNews.Append(new ArabicNewsDto()
        //        {
        //            NewsId = news.NewsId,
        //            ArabicTitle = news.ArabicTitle,
        //            ArabicDescription = news.ArabicDescription,
        //            Arabic_News_Date = news.Arabic_News_Date,
        //            Image = news.Image,
        //        });
        //    }

        //    return Ok(arabicNews);
        //}

        //[HttpGet("/Get_All_English_News")]
        //public async Task<ActionResult> GetAllEnglish()
        //{
        //    var AllNews = await _unitOfWork.News.GetAllAsync(null);
        //    if (AllNews == null) return NotFound("There is no news created");

        //    IEnumerable<EnglishNewsDto> englishNews = new List<EnglishNewsDto>() { };
        //    foreach (var news in AllNews)
        //    {
        //        englishNews.Append(new EnglishNewsDto()
        //        {
        //            NewsId = news.NewsId,
        //            EnglishTitle = news.EnglishTitle,
        //            EnglishDescription = news.EnglishDescription,
        //            English_News_Date = news.English_News_Date,
        //            Image = news.Image,
        //        });
        //    }

        //    return Ok(englishNews);
        //}


        [HttpGet("/Get_last_Arabic_News")]
        public async Task<ActionResult> GetLastArabicNews(string lang)
        {
            var News = await _unitOfWork.News.GetLastFourAsync();
            if (News == null) return NotFound("There is no news created");

            switch (lang)
            {
                case "Ar":
                    {
                        IEnumerable<ArabicNewsDto> arabicNews = new List<ArabicNewsDto>() { };
                        foreach (var news in News)
                        {
                            arabicNews.Append(new ArabicNewsDto()
                            {
                                NewsId = news.NewsId,
                                ArabicTitle = news.ArabicTitle,
                                ArabicDescription = news.ArabicDescription,
                                Arabic_News_Date = news.Arabic_News_Date,
                                Image = news.Image,
                            });
                        }
                        return Ok(arabicNews);
                    }
                case "En":
                    {
                        IEnumerable<EnglishNewsDto> englishNews = new List<EnglishNewsDto>() { };
                        foreach (var news in News)
                        {
                            englishNews.Append(new EnglishNewsDto()
                            {
                                NewsId = news.NewsId,
                                EnglishTitle = news.EnglishTitle,
                                EnglishDescription = news.EnglishDescription,
                                English_News_Date = news.English_News_Date,
                                Image = news.Image,
                            });
                        }

                        return Ok(englishNews);
                    }
            }
            return BadRequest();
        }

        //[HttpGet("/Get_last_English_News")]
        //public async Task<ActionResult> GetLastEnglishNews()
        //{
        //    var News = await _unitOfWork.News.GetLastFourAsync();
        //    if (News == null) return NotFound("There is no news created");

        //    IEnumerable<EnglishNewsDto> englishNews = new List<EnglishNewsDto>() { };
        //    foreach (var news in News)
        //    {
        //        englishNews.Append(new EnglishNewsDto()
        //        {
        //            NewsId = news.NewsId,
        //            EnglishTitle = news.EnglishTitle,
        //            EnglishDescription = news.EnglishDescription,
        //            English_News_Date = news.English_News_Date,
        //            Image = news.Image,
        //        });
        //    }

        //    return Ok(englishNews);
        //}

        [HttpGet("/Search_For_Arabic_News")]
        public async Task<ActionResult> SearchArabicNews(string SearchString)
        {
            var news = _unitOfWork.News.SearchForArabicNews(SearchString);
            if (news == null) return NotFound("There is no news in this description");
            return Ok(news);
        }

        [HttpGet("/Search_For_English_News")]
        public async Task<ActionResult> SearchEnglishNews(string SearchString)
        {
            var news = _unitOfWork.News.SearchForEnglishNews(SearchString);
            if (news == null) return NotFound("There is no news in this description");
            return Ok(news);
        }

        [HttpPut("/Update_News")]
        public async Task<ActionResult> Update(UpdateNewsDto NewsDto)
        {
            News News = await _unitOfWork.News.GetByIdAsync(n => n.NewsId == NewsDto.NewsId, new[] { "College" });
            
           
            if (News == null) return NotFound("News not found");

           

            News = new()
            {
                NewsId = NewsDto.NewsId,
                ArabicTitle = NewsDto.ArabicTitle,
                EnglishTitle = NewsDto.EnglishTitle,
                ArabicDescription = NewsDto.ArabicDescription,
                EnglishDescription = NewsDto.EnglishDescription,
                Arabic_News_Date = NewsDto.Arabic_News_Date,
                English_News_Date = NewsDto.English_News_Date,
                Image = _unitOfWork.News.UploadNewsImage(NewsDto.Image, NewsDto.NewsId),


                //College =await _unitOfWork.Colleges.GetByIdAsync(c => c.CollegeId == NewsDto.CollegeId, new[] { "Departments" })
            };
            
            var NewsUpdated = await _unitOfWork.News.UpdateAsync(News);
            if (NewsUpdated == null) return BadRequest("News Update operation failed");
            await _unitOfWork.CompleteAsync();
            return Ok(News);
        }

        [HttpDelete("/Delete_News/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var News = await _unitOfWork.News.GetByIdAsync(n => n.NewsId == id, new[] { "College" });
            if (News == null) return NotFound("News Not Found");

            var NewsDeleted = await _unitOfWork.News.DeleteAsync(id);
            if (NewsDeleted == null) return BadRequest("News delete operation failed");

            await _unitOfWork.CompleteAsync();
            return Ok(News);
        }
    }
}
