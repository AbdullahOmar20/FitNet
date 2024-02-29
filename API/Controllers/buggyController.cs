
using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class buggyController : BaseController
    {
        private readonly StoreContext _context;

        public buggyController(StoreContext context)
        {
            _context = context;
        }
        [HttpGet("testauth")]
        [Authorize]
        public ActionResult<string> GetSecretText()
        {
            return "secret stuff";
        }

        [HttpGet("notfound")]
        public IActionResult GetNotFoundRequest(){
            var temp = _context.Products.Find(40);
            if(temp == null)
                return NotFound(new APIResponse(404));
            return Ok();
        }
        [HttpGet("servererror")]
        public IActionResult GetServerError(){
            var temp = _context.Products.Find(40);
            var result = temp.ToString();
            return Ok();
        }
        [HttpGet("badrequest")]
        public IActionResult GetBadRequest(){
            
            return BadRequest(new APIResponse(400));
        }
        [HttpGet("badrequest/{id}")]
        public IActionResult GetBadRequest(int id){
            return Ok();
        }
        
    }
}