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
using FinalProject.Core.Dtos.EventDtos;
using FinalProject.EF.Migrations;

namespace FinalProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("/Create_Event")]
        public async Task<IActionResult> Create(CreateEvent EventDto) {
            Event Event = new Event()
            {
                ArabicTitle = EventDto.ArabicTitle,
                EnglishTitle = EventDto.EnglishTitle,
                ArabicDescription = EventDto.ArabicDescription,
                EnglishDescription = EventDto.EnglishDescription,
                Arabic_Event_Start_Date = EventDto.Arabic_Event_Start_Date,
                English_Event_Start_Date = EventDto.English_Event_Start_Date,

                
                //College =await _unitOfWork.Colleges.GetByIdAsync(c=> c.CollegeId == EventDto.CollegeId , new[] {"Departments"})
            };

            Event.Image = _unitOfWork.Events.UploadEventImage(EventDto.Image, Event);

            var EventAdded = await _unitOfWork.Events.AddAsync(Event);
            if (EventAdded == null)
                return BadRequest("Bad Request");

           
            await _unitOfWork.CompleteAsync();
            return Ok(Event);
        }

        [HttpGet("/Get_Event_By_Id/{id}")]
        public async Task<IActionResult> Get(int id) {
            var Event = await _unitOfWork.Events.GetByIdAsync(e=> e.EventId == id );
            if (Event == null) return NotFound("News not found");

            return Ok(Event);
        }

        [HttpGet("/Get_All_Events")]
        public async Task<IActionResult> GetAll()
        {
            var Events = await _unitOfWork.Events.GetAllAsync(null);
            if (Events == null) return NotFound("There is no news created");

            return Ok(Events);
        }

        [HttpGet("/Get_last_Events")]
        public async Task<IActionResult> GetLastEvents()
        {
            var Events = await _unitOfWork.Events.GetLastFourAsync();
            if (Events == null) return NotFound("There is no Event created");

            return Ok(Events);
        }


        [HttpPut("/Update_Event")]
        public async Task<IActionResult> Update(UpdateEvent EventDto)
        {
            Event Event = await _unitOfWork.Events.GetByIdAsync(e => e.EventId == EventDto.EventId);

            if ( Event == null) return NotFound("Event Not Found");

            Event = new()
            {
                EventId = EventDto.EventId,
                ArabicTitle = EventDto.ArabicTitle,
                EnglishTitle = EventDto.EnglishTitle,
                ArabicDescription = EventDto.ArabicDescription,
                EnglishDescription = EventDto.EnglishDescription,
                Arabic_Event_Start_Date = EventDto.Arabic_Event_Start_Date,
                English_Event_Start_Date = EventDto.English_Event_Start_Date,
                Image = _unitOfWork.Events.UploadEventImage(EventDto.Image, EventDto.EventId),

                //College =await _unitOfWork.Colleges.GetByIdAsync(c=> c.CollegeId == EventDto.CollegeId , new[] {"Departments"})
            };


            var EventUpdated  = await _unitOfWork.Events.UpdateAsync(Event);
            if (EventUpdated == null) return BadRequest("Event Update operation failed");

            await _unitOfWork.CompleteAsync();
            return Ok(Event);
        }

        [HttpDelete("/Delete_Event/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Event = await _unitOfWork.Events.GetByIdAsync(e => e.EventId == id);
            if (Event == null)
                return NotFound("Event Not Found");

            var EventDeleted = await _unitOfWork.Events.DeleteAsync(id);
            if (EventDeleted == null) return BadRequest("Event delete operation failed");

            await _unitOfWork.CompleteAsync();
            return Ok(Event);
        }
    }
}
