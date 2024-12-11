﻿using FinalProject.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Core.IRepositories
{
    public interface IEventRepository : IBaseRepository<Event>
    {

        public string UploadEventImage(IFormFile Image, int? EventId);
        public string UploadEventImage(IFormFile Image, Event? Event);


        //Task<IEnumerable<Event>> GetAllAsync();
        //Task<Event> GetByIdAsync(int id);
        //Task<bool> AddAsync(Event eventItem);
        //Task<bool> UpdateAsync(Event eventItem);
        //Task<bool> DeleteAsync(int id);
    }
}


