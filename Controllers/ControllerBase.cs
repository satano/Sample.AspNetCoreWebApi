using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sample.AspNetCoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "User")]
    public class ControllerBase : Controller
    {

    }
}