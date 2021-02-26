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

        [HttpPost("filter")]
        public IActionResult Filter(AssetFilter filter)
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

            if (filter.ProductIds != null)
            {
                query = query.Where(asset => filter.ProductIds.Contains(asset.Product.Id));
            }

            if (filter.ProductNames != null)
            {
                query = query.Where(asset => filter.ProductNames.Contains(asset.Product.Name));
            }

            if (filter.ProductPriceMin != null)
            {
                query = query.Where(asset => asset.Product.Price >= filter.ProductPriceMin);
            }

            if (filter.ProductPriceMax != null)
            {
                query = query.Where(asset => asset.Product.Price <= filter.ProductPriceMax);
            }

            if (filter.BrandIds != null)
            {
                query = query.Where(asset => filter.BrandIds.Contains(asset.Product.Brand.Id));
            }

            if (filter.BrandNames != null)
            {
                query = query.Where(asset => filter.BrandNames.Contains(asset.Product.Brand.Name));
            }

            if (filter.FormFactorIds != null)
            {
                query = query.Where(asset => filter.FormFactorIds.Contains(asset.Product.FormFactor.Id));
            }

            if (filter.FormFactorNames != null)
            {
                query = query.Where(asset => filter.FormFactorNames.Contains(asset.Product.FormFactor.Name));
            }

            if (filter.OfficeIds != null)
            {
                query = query.Where(asset => filter.OfficeIds.Contains(asset.Office.Id));
            }

            if (filter.OfficeCities != null)
            {
                query = query.Where(asset => filter.OfficeCities.Contains(asset.Office.City));
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