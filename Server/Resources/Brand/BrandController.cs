using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Server
{
    public class BrandController : ControllerBase
    {
        private AssetTrackerContext _context;

        public BrandController(AssetTrackerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var brands = _context.Brands
                .ToList();

            return new OkObjectResult(brands);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var brand = _context.Brands
                .FirstOrDefault(brand => brand.Id == id);

            if (brand == null)
            {
                return new NotFoundObjectResult($"Brand with id {id} not found");
            }

            return new OkObjectResult(brand);
        }

        [HttpPost]
        public IActionResult Create(BrandDto dto)
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

            var brand = new Brand();
            brand.Name = dto.Name;

            _context.Brands.Add(brand);
            _context.SaveChanges();

            return new OkObjectResult(brand);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BrandDto dto)
        {
            var brand = _context.Brands
                .FirstOrDefault(brand => brand.Id == id);

            if (brand == null)
            {
                return new NotFoundObjectResult($"Brand with id {id} not found");
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

            brand.Name = dto.Name;

            _context.SaveChanges();

            return new OkObjectResult(brand);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var brand = _context.Brands
                .FirstOrDefault(brand => brand.Id == id);

            if (brand == null)
            {
                return new NotFoundObjectResult($"Brand with id {id} not found");
            }

            _context.Brands.Remove(brand);
            _context.SaveChanges();

            return new OkResult();
        }
    }
}