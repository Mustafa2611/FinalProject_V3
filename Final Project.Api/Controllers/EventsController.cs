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
using FinalProject.Core.Dtos.NewsDtos;

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
        public async Task<ActionResult> Create(CreateEvent EventDto) {
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

        //[HttpGet("/Get_Event_By_Id/{id}")]
        //public async Task<ActionResult> Get(int id) {
        //    var Event = await _unitOfWork.Events.GetByIdAsync(e=> e.EventId == id );
        //    if (Event == null) return NotFound("News not found");

        //    return Ok(Event);
        //}

        [HttpGet("/Get_Arabic_Event_By_Id/{id}")]
        public async Task<ActionResult> GetArabic(int id)
        {
            var Event = await _unitOfWork.Events.GetByIdAsync(n => n.EventId == id);
            if (Event == null) return NotFound("Event not found");

            ArabicEventDto arabicEvent = new ArabicEventDto()
            {
                EventId = Event.EventId,
                ArabicTitle = Event.ArabicTitle,
                ArabicDescription = Event.ArabicDescription,
                Arabic_Event_Start_Date = Event.Arabic_Event_Start_Date,
                Image = Event.Image,
            };

            return Ok(arabicEvent);
        }

        [HttpGet("/Get_English_Event_By_Id/{id}")]
        public async Task<ActionResult> GetEnglish(int id)
        {
            var Event = await _unitOfWork.Events.GetByIdAsync(n => n.EventId == id);
            if (Event == null) return NotFound("Event not found");

            EnglishEventDto englishEvent = new EnglishEventDto()
            {
                EventId = Event.EventId,
                EnglishTitle = Event.EnglishTitle,
                EnglishDescription = Event.EnglishDescription,
                English_Event_Start_Date = Event.English_Event_Start_Date,
                Image = Event.Image,
            };

            return Ok(englishEvent);
        }


        //[HttpGet("/Get_All_Events")]
        //public async Task<ActionResult> GetAll()
        //{
        //    var Events = await _unitOfWork.Events.GetAllAsync(null);
        //    if (Events == null) return NotFound("There is no news created");

        //    return Ok(Events);
        //}


        [HttpGet("/Get_All_Arabic_Events")]
        public async Task<ActionResult> GetAllArabic()
        {
            var AllEvents = await _unitOfWork.Events.GetAllAsync(null);
            if (AllEvents == null) return NotFound("There is no Events created");

            IEnumerable<ArabicEventDto> arabicEvent = new List<ArabicEventDto>() { };
            foreach (var news in AllEvents)
            {
                arabicEvent.Append(new ArabicEventDto()
                {
                    EventId = news.EventId,
                    ArabicTitle = news.ArabicTitle,
                    ArabicDescription = news.ArabicDescription,
                    Arabic_Event_Start_Date = news.Arabic_Event_Start_Date,
                    Image = news.Image,
                });
            }

            return Ok(arabicEvent);
        }


        [HttpGet("/Get_last_Arabic_Events")]
        public async Task<ActionResult> GetLastArabicEvents()
        {
            var Events = await _unitOfWork.Events.GetLastFourAsync();
            if (Events == null) return NotFound("There is no news created");

            IEnumerable<ArabicEventDto> arabicEvents = new List<ArabicEventDto>() { };
            foreach (var news in Events)
            {
                arabicEvents.Append(new ArabicEventDto()
                {
                    EventId = news.EventId,
                    ArabicTitle = news.ArabicTitle,
                    ArabicDescription = news.ArabicDescription,
                    Arabic_Event_Start_Date = news.Arabic_Event_Start_Date,
                    Image = news.Image,
                });
            }


            return Ok(arabicEvents);
        }

        [HttpGet("/Get_All_English_Events")]
        public async Task<ActionResult> GetAllEnglish()
        {
            var AllEvents = await _unitOfWork.Events.GetAllAsync(null);
            if (AllEvents == null) return NotFound("There is no Events created");

            IEnumerable<EnglishEventDto> englishEvents = new List<EnglishEventDto>() { };
            foreach (var Event in AllEvents)
            {
                englishEvents.Append(new EnglishEventDto()
                {
                    EventId = Event.EventId,
                    EnglishTitle = Event.EnglishTitle,
                    EnglishDescription = Event.EnglishDescription,
                    English_Event_Start_Date = Event.English_Event_Start_Date,
                    Image = Event.Image,
                });
            }

            return Ok(englishEvents);
        }

        [HttpGet("/Get_last_English_News")]
        public async Task<ActionResult> GetLastEnglishNews()
        {
            var Events = await _unitOfWork.Events.GetLastFourAsync();
            if (Events == null) return NotFound("There is no news created");

            IEnumerable<EnglishEventDto> englishEvents = new List<EnglishEventDto>() { };
            foreach (var news in Events)
            {
                englishEvents.Append(new EnglishEventDto()
                {
                    EventId = news.EventId,
                    EnglishTitle = news.EnglishTitle,
                    EnglishDescription = news.EnglishDescription,
                    English_Event_Start_Date = news.English_Event_Start_Date,
                    Image = news.Image,
                });
            }

            return Ok(englishEvents);
        }


        [HttpPut("/Update_Event")]
        public async Task<ActionResult> Update(UpdateEvent EventDto)
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
        public async Task<ActionResult> Delete(int id)
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
