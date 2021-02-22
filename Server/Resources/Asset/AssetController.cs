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