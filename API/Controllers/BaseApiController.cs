
using API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ServiceFilter(typeof(RegistrarAtividadeDoUsuario))]
    [ApiController]
    [Route("api/[controller]")] //acessar 
    public class BaseApiController : ControllerBase
    {
        
    }
}