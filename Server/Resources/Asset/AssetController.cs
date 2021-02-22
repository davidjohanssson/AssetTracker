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

            query = query.Take(20);

            var assets = query
                .Include(asset => asset.Product.Brand)
                .Include(asset => asset.Product.FormFactor)
                .Include(asset => asset.Office)
                .ToList();

            var result = new { assets, count };

            return new OkObjectResult(result);
        }
    }
}