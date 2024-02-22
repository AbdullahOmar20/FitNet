
using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("error/{code}")]
    //so its doesnot make error while using swagger as the method inside is 
    //not specified if it is get or what as we cant specify it bcz we dont know the type of error comming
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : BaseController
    {
        public IActionResult Error(int code){
            return new ObjectResult(new APIResponse(code));
        }
    }
}