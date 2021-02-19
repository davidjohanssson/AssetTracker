using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Server
{
    [ApiController]
    [Route("[controller]")]
    public class OfficeController : ControllerBase
    {
        private AssetTrackerContext _context;

        public OfficeController(
            AssetTrackerContext context
        )
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var offices = _context.Offices
                .Include(office => office.Currency)
                .ToList();

            return new OkObjectResult(offices);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var office = _context.Offices
                .Include(office => office.Currency)
                .FirstOrDefault(office => office.Id == id);

            if (office == null)
            {
                return new NotFoundObjectResult($"Office with id {id} not found");
            }

            return new OkObjectResult(office);
        }
    }
}