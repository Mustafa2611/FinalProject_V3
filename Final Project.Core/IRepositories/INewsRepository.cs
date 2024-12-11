using FinalProject.Core.Models;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace FinalProject.Core.IRepositories
{
    public interface INewsRepository : IBaseRepository<News>
    {
        //Task<IEnumerable<News>> GetAllAsync();
        //Task<News> GetByIdAsync(int id);
        //Task<bool> AddAsync(News newsItem);
        //Task<bool> UpdateAsync(News newsItem);
        //Task<bool> DeleteAsync(int id);


        public string UploadNewsImage(IFormFile Image, int? NewsId);
        public string UploadNewsImage(IFormFile Image, News? News);
        public Task<IEnumerable<News>> GetLastFourAsync();


    }
}

