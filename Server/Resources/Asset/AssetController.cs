using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Server
{
    [ApiController]
    [Route("[controller]")]
    public class AssetController : ControllerBase
    {
        private AssetTrackerContext _context;

        public AssetController(AssetTrackerContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var asset = _context.Assets
                .Include(asset => asset.Product.Brand)
                .Include(asset => asset.Product.FormFactor)
                .Include(asset => asset.Office)
                .FirstOrDefault(asset => asset.Id == id);

            if (asset == null)
            {
                return new NotFoundObjectResult($"Asset with id {id} not found");
            }

            return new OkObjectResult(asset);
        }

        [HttpPost]
        public IActionResult Create(Asset dto)
        {
            if (dto.PurchaseDate <= new DateTime(2000, 1, 1))
            {
                return new BadRequestObjectResult("PurchaseDate must not be before 2000-01-01");
            }

            if (dto.PurchaseDate >= DateTime.Now)
            {
                return new BadRequestObjectResult("PurchaseDate must not be after current date");
            }

            var product = _context.Products.FirstOrDefault(product => product.Id == dto.ProductId);

            if (product == null)
            {
                return new NotFoundObjectResult($"Product with id {dto.ProductId} not found");
            }

            var office = _context.Offices.FirstOrDefault(office => office.Id == dto.OfficeId);

            if (office == null)
            {
                return new NotFoundObjectResult($"Office with id {dto.OfficeId} not found");
            }

            _context.Assets.Add(dto);
            _context.SaveChanges();

            return new OkObjectResult(dto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Asset dto)
        {
            var asset = _context.Assets
                .Include(asset => asset.Product.Brand)
                .Include(asset => asset.Product.FormFactor)
                .Include(asset => asset.Office)
                .FirstOrDefault(asset => asset.Id == id);

            if (asset == null)
            {
                return new NotFoundObjectResult($"Asset with id {id} not found");
            }

            if (dto.PurchaseDate <= new DateTime(2000, 1, 1))
            {
                return new BadRequestObjectResult("PurchaseDate must not be before 2000-01-01");
            }

            if (dto.PurchaseDate >= DateTime.Now)
            {
                return new BadRequestObjectResult("PurchaseDate must not be after current date");
            }

            var product = _context.Products.FirstOrDefault(product => product.Id == dto.ProductId);

            if (product == null)
            {
                return new NotFoundObjectResult($"Product with id {dto.ProductId} not found");
            }

            var office = _context.Offices.FirstOrDefault(office => office.Id == dto.OfficeId);

            if (office == null)
            {
                return new NotFoundObjectResult($"Office with id {dto.OfficeId} not found");
            }

            asset.PurchaseDate = dto.PurchaseDate;
            asset.Product = product;
            asset.Office = office;

            _context.SaveChanges();

            return new OkObjectResult(asset);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var asset = _context.Assets.FirstOrDefault(asset => asset.Id == id);

            if (asset == null)
            {
                return new NotFoundObjectResult($"Asset with id {id} not found");
            }

            _context.Assets.Remove(asset);
            _context.SaveChanges();

            return new OkResult();
        }

        [HttpPost("search")]
        public IActionResult Search(AssetFilter filter)
        {
            var query = _context.Assets.AsQueryable();

            if (filter.Ids != null)
            {
                query = query.Where(asset => filter.Ids.Contains(asset.Id));
            }

            if (filter.PurchaseDateMin != null)
            {
                query = query.Where(asset => asset.PurchaseDate >= filter.PurchaseDateMin);
            }

            if (filter.PurchaseDateMax != null)
            {
                query = query.Where(asset => asset.PurchaseDate <= filter.PurchaseDateMax);
            }

            if (filter.ProductFilter != null)
            {
                if (filter.ProductFilter.Ids != null)
                {
                    query = query.Where(asset => filter.ProductFilter.Ids.Contains(asset.Product.Id));
                }

                if (filter.ProductFilter.Names != null)
                {
                    query = query.Where(asset => filter.ProductFilter.Names.Contains(asset.Product.Name));
                }

                if (filter.ProductFilter.PriceMin != null)
                {
                    query = query.Where(asset => asset.Product.Price >= filter.ProductFilter.PriceMin);
                }

                if (filter.ProductFilter.PriceMax != null)
                {
                    query = query.Where(asset => asset.Product.Price <= filter.ProductFilter.PriceMax);
                }

                if (filter.ProductFilter.BrandFilter != null)
                {
                    if (filter.ProductFilter.BrandFilter.Ids != null)
                    {
                        query = query.Where(asset => filter.ProductFilter.BrandFilter.Ids.Contains(asset.Product.Brand.Id));
                    }

                    if (filter.ProductFilter.BrandFilter.Names != null)
                    {
                        query = query.Where(asset => filter.ProductFilter.BrandFilter.Names.Contains(asset.Product.Brand.Name));
                    }
                }

                if (filter.ProductFilter.FormFactorFilter != null)
                {
                    if (filter.ProductFilter.FormFactorFilter.Ids != null)
                    {
                        query = query.Where(asset => filter.ProductFilter.FormFactorFilter.Ids.Contains(asset.Product.FormFactor.Id));
                    }

                    if (filter.ProductFilter.FormFactorFilter.Names != null)
                    {
                        query = query.Where(asset => filter.ProductFilter.FormFactorFilter.Names.Contains(asset.Product.FormFactor.Name));
                    }
                }
            }

            if (filter.OfficeFilter != null)
            {
                if (filter.OfficeFilter.Ids != null)
                {
                    query = query.Where(asset => filter.OfficeFilter.Ids.Contains(asset.Office.Id));
                }

                if (filter.OfficeFilter.Cities != null)
                {
                    query = query.Where(asset => filter.OfficeFilter.Cities.Contains(asset.Office.City));
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

            var assets = query
                .Include(asset => asset.Product.Brand)
                .Include(asset => asset.Product.FormFactor)
                .Include(asset => asset.Office)
                .ToList();

            var result = new Object[] { assets, count };

            return new OkObjectResult(result);
        }
    }
}