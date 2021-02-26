using System;
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
        public IActionResult Create(Brand dto)
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

            _context.Brands.Add(dto);
            _context.SaveChanges();

            return new OkObjectResult(dto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Brand dto)
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

        [HttpPost("filter")]
        public IActionResult Filter(BrandFilter filter)
        {
            var query = _context.Brands.AsQueryable();

            if (filter.Ids != null)
            {
                query = query.Where(brand => filter.Ids.Contains(brand.Id));
            }

            if (filter.Names != null)
            {
                query = query.Where(brand => filter.Names.Contains(brand.Name));
            }

            if (filter.OrderByAsc != null)
            {
                query = query.OrderBy(filter.OrderByAsc);
            }

            if (filter.OrderByDesc != null)
            {
                query = query.OrderByDescending(filter.OrderByDesc);
            }

            if (filter.Skip != null)
            {
                query = query.Skip(filter.Skip.Value);
            }

            var count = query.Count();

            query = query.Take(10);

            var brands = query.ToList();

            var result = new Object[] { brands, count };

            return new OkObjectResult(result);
        }
    }
}