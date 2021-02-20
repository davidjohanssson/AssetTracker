using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Server
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private AssetTrackerContext _context;

        public ProductController(AssetTrackerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _context.Products
                .Include(product => product.Brand)
                .Include(product => product.FormFactor)
                .ToList();

            return new OkObjectResult(products);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = _context.Products
                .Include(product => product.Brand)
                .Include(product => product.FormFactor)
                .FirstOrDefault(product => product.Id == id);

            if (product == null)
            {
                return new NotFoundObjectResult($"Product with id {id} not found");
            }

            return new OkObjectResult(product);
        }

        [HttpPost]
        public IActionResult Create(ProductDto dto)
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

            var product = new Product();
            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Brand = brand;
            product.FormFactor = formFactor;

            _context.Products.Add(product);
            _context.SaveChanges();

            return new OkObjectResult(product);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ProductDto dto)
        {
            var product = _context.Products
                .Include(product => product.Brand)
                .Include(product => product.FormFactor)
                .FirstOrDefault(product => product.Id == id);

            if (product == null)
            {
                return new NotFoundObjectResult($"Product with id {id} not found");
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

            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Brand = brand;
            product.FormFactor = formFactor;

            _context.SaveChanges();

            return new OkObjectResult(product);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products
                .FirstOrDefault(product => product.Id == id);

            if (product == null)
            {
                return new NotFoundObjectResult($"Product with id {id} not found");
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return new OkResult();
        }
    }
}