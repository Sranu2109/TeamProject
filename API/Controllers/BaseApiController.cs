using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]     //saves us from manually checking to see if there is anyvalidation errors  //attribute
    [Route("api/[controller]")]  //adding 'api/' is optional but is conventional to do it.
    public class BaseApiController : ControllerBase
    {
        
    }
}