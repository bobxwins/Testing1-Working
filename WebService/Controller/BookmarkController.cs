using System.Collections.Generic;
using AutoMapper;
using DataServiceLib;
using DataServiceLib.DBObjects;
using Microsoft.AspNetCore.Mvc;
using ProjectPortfolio2_Group11.Model;
using DataServiceLib.IDataService;
using DataServiceLib.DataService;



namespace ProjectPortfolio2_Group11.Controller
{
    [ApiController]
    [Route("api/bookmark")]
    public class BookmarkController : ControllerBase
    {
        private readonly IBookmarkingDataService _dataService;
        private readonly IMapper _mapper;
        public BookmarkController(IBookmarkingDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
            
            
        }
         

            [HttpGet("{userid}/{nconst}")]

            // uses userid and nconst as the URL path
       
        public IActionResult GetBookMark(int userid,string nconst)
        {
            var bookmark = _dataService.GetBookMark(userid,nconst);
            if (bookmark == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<BookmarkPersonDto>(bookmark));
        }


        [HttpPost("{userid}")]
        public IActionResult CreateBookmark(BookmarkPersonForCreationDto bookDto)
        {
            var bookmark = _mapper.Map<BookmarkPerson>(bookDto);
            _dataService.CreateBookmark(bookmark);
            return Created("", bookmark);
        }


        [HttpPut("{userid}/{nconst}")]
        public IActionResult UpdateBookmark(int UserId,string Nconst)
        {
            var bookmark = _mapper.Map<BookmarkPerson>(UserId);
            if (_dataService.UpdateBookmark(bookmark))
            {
                return NotFound();
            }
            return NoContent();
        }


        [HttpDelete("{userid}/{nconst}")]
        public IActionResult DeleteBookmark(int userid,string nconst)
        {
            var response = " bookmark not found";
            if (!_dataService.DeleteBookmark(userid,nconst))
            {
                return NotFound(response);
            }
            response = " bookmark deleted";
            return CreatedAtRoute(null, userid + response);
        }
    }
}