using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Server
{
    [ApiController]
    [Route("[controller]")]
    public class ModelController : ControllerBase
    {
        private AssetTrackerContext _context;

        public ModelController(AssetTrackerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var models = _context.Models
                .Include(model => model.Brand)
                .Include(model => model.FormFactor)
                .ToList();

            return new OkObjectResult(models);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var model = _context.Models
                .Include(model => model.Brand)
                .Include(model => model.FormFactor)
                .FirstOrDefault(model => model.Id == id);

            if (model == null)
            {
                return new NotFoundObjectResult($"Model with id {id} not found");
            }

            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult Create(ModelDto dto)
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

            if (double.IsNegative(dto.Price))
            {
                return new BadRequestObjectResult("Price must be positive");
            }

            if (Math.Round(dto.Price, 2) != dto.Price)
            {
                return new BadRequestObjectResult("Price must have two decimals");
            }

            var brand = _context.Brands.FirstOrDefault(brand => brand.Id == dto.BrandId);

            if (brand == null)
            {
                return new NotFoundObjectResult($"Brand with id {dto.BrandId} not found");
            }

            var formFactor = _context.FormFactors.FirstOrDefault(formFactor => formFactor.Id == dto.FormFactorId);

            if (formFactor == null)
            {
                return new NotFoundObjectResult($"FormFactor with id {dto.FormFactorId} not found");
            }

            var model = new Model();
            model.Name = dto.Name;
            model.Price = dto.Price;
            model.Brand = brand;
            model.FormFactor = formFactor;

            _context.Models.Add(model);
            _context.SaveChanges();

            return new OkObjectResult(model);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ModelDto dto)
        {
            var model = _context.Models
                .Include(model => model.Brand)
                .Include(model => model.FormFactor)
                .FirstOrDefault(model => model.Id == id);

            if (model == null)
            {
                return new NotFoundObjectResult($"Model with id {id} not found");
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

            if (double.IsNegative(dto.Price))
            {
                return new BadRequestObjectResult("Price must be positive");
            }

            if (Math.Round(dto.Price, 2) != dto.Price)
            {
                return new BadRequestObjectResult("Price must have two decimals");
            }

            var brand = _context.Brands
                .FirstOrDefault(brand => brand.Id == dto.BrandId);

            if (brand == null)
            {
                return new NotFoundObjectResult($"Brand with id {dto.BrandId} not found");
            }

            var formFactor = _context.FormFactors
                .FirstOrDefault(formFactor => formFactor.Id == dto.FormFactorId);

            if (formFactor == null)
            {
                return new NotFoundObjectResult($"FormFactor with id {dto.FormFactorId} not found");
            }

            model.Name = dto.Name;
            model.Price = dto.Price;
            model.Brand = brand;
            model.FormFactor = formFactor;

            _context.SaveChanges();

            return new OkObjectResult(model);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var model = _context.Models
                .FirstOrDefault(model => model.Id == id);

            if (model == null)
            {
                return new NotFoundObjectResult($"Model with id {id} not found");
            }

            _context.Models.Remove(model);
            _context.SaveChanges();

            return new OkResult();
        }
    }
}