using AntarisBlog.Api.Data;
using AntarisBlog.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace AntarisBlog.Api.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly AntarisBlogContext _context;

        public HealthController(AntarisBlogContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetHealth()
        {
            return Ok(new {status="Healthy"});
        }
    }
}