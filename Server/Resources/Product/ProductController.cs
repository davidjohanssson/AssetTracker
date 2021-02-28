using System;
using System.Collections.Generic;
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
        public IActionResult Create(Product dto)
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

            _context.Products.Add(dto);
            _context.SaveChanges();

            return new OkObjectResult(dto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Product dto)
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
            var product = _context.Products.FirstOrDefault(product => product.Id == id);

            if (product == null)
            {
                return new NotFoundObjectResult($"Product with id {id} not found");
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return new OkResult();
        }

        [HttpPost("search")]
        public IActionResult Search(ProductFilter filter)
        {
            var query = _context.Products.AsQueryable();

            if (filter.Ids != null)
            {
                query = query.Where(product => filter.Ids.Contains(product.Id));
            }

            if (filter.Names != null)
            {
                query = query.Where(product => filter.Names.Contains(product.Name));
            }

            if (filter.PriceMin != null)
            {
                query = query.Where(product => product.Price >= filter.PriceMin);
            }

            if (filter.PriceMax != null)
            {
                query = query.Where(product => product.Price <= filter.PriceMax);
            }

            if (filter.BrandFilter != null)
            {
                if (filter.BrandFilter.Ids != null)
                {
                    query = query.Where(product => filter.BrandFilter.Ids.Contains(product.Brand.Id));
                }

                if (filter.BrandFilter.Names != null)
                {
                    query = query.Where(product => filter.BrandFilter.Names.Contains(product.Brand.Name));
                }
            }

            if (filter.FormFactorFilter != null)
            {
                if (filter.FormFactorFilter.Ids != null)
                {
                    query = query.Where(product => filter.FormFactorFilter.Ids.Contains(product.FormFactor.Id));
                }

                if (filter.FormFactorFilter.Names != null)
                {
                    query = query.Where(product => filter.FormFactorFilter.Names.Contains(product.FormFactor.Name));
                }
            }

            if (filter.OrderByAsc != null)
            {
                query = query.OrderBy(Helper.CapitalizeFirstLetter(filter.OrderByAsc));
            }

            if (filter.OrderByDesc != null)
            {
                query = query.OrderByDescending(Helper.CapitalizeFirstLetter(filter.OrderByDesc));
            }

            if (filter.Skip != null)
            {
                query = query.Skip(filter.Skip.Value);
            }

            var count = query.Count();

            if (filter.Take != null)
            {
                if (filter.Take > 100)
                {
                    query = query.Take(100);
                }
                else
                {
                    query = query.Take(Convert.ToInt32(filter.Take.Value));
                }
            }
            else
            {
                query = query.Take(10);
            }

            var products = query
                .Include(product => product.Brand)
                .Include(product => product.FormFactor)
                .ToList();

            var result = new Object[] { products, count };

            return new OkObjectResult(result);
        }
    }
}