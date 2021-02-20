using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Server
{
    [ApiController]
    [Route("[controller]")]
    public class FormFactorController : ControllerBase
    {
        private AssetTrackerContext _context;

        public FormFactorController(AssetTrackerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var formFactors = _context.FormFactors
                .ToList();

            return new OkObjectResult(formFactors);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var formFactor = _context.FormFactors
                .FirstOrDefault(formFactor => formFactor.Id == id);

            if (formFactor == null)
            {
                return new NotFoundObjectResult($"FormFactor with id {id} not found");
            }

            return new OkObjectResult(formFactor);
        }

        [HttpPost]
        public IActionResult Create(FormFactorDto dto)
        {
            if (dto.Name == null)
            {
                return new BadRequestObjectResult("Name must not be null");
            }

            if (dto.Name.Length < 1)
            {
                return new BadRequestObjectResult("Name must be atleast 1 character");
            }

            if (dto.Name.Length > 256)
            {
                return new BadRequestObjectResult("Name must not be more than 256 characters");
            }

            var formFactor = new FormFactor();
            formFactor.Name = dto.Name;

            _context.FormFactors.Add(formFactor);
            _context.SaveChanges();

            return new OkObjectResult(formFactor);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, FormFactorDto dto)
        {
            var formFactor = _context.FormFactors
                .FirstOrDefault(formFactor => formFactor.Id == id);

            if (formFactor == null)
            {
                return new NotFoundObjectResult($"FormFactor with id {id} not found");
            }

            if (dto.Name == null)
            {
                return new BadRequestObjectResult("Name must not be null");
            }

            if (dto.Name.Length < 1)
            {
                return new BadRequestObjectResult("Name must be atleast 1 character");
            }

            if (dto.Name.Length > 256)
            {
                return new BadRequestObjectResult("Name must not be more than 256 characters");
            }

            formFactor.Name = dto.Name;

            _context.SaveChanges();

            return new OkObjectResult(formFactor);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var formFactor = _context.FormFactors
                .FirstOrDefault(formFactor => formFactor.Id == id);

            if (formFactor == null)
            {
                return new NotFoundObjectResult($"FormFactor with id {id} not found");
            }

            _context.FormFactors.Remove(formFactor);
            _context.SaveChanges();

            return new OkResult();
        }
    }
}