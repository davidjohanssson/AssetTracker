using System;
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
        public IActionResult Create(FormFactor dto)
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

            _context.FormFactors.Add(dto);
            _context.SaveChanges();

            return new OkObjectResult(dto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, FormFactor dto)
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

        [HttpPost("search")]
        public IActionResult Search(FormFactorFilter filter)
        {
            var query = _context.FormFactors.AsQueryable();

            if (filter.Ids != null)
            {
                query = query.Where(formFactor => filter.Ids.Contains(formFactor.Id));
            }

            if (filter.Names != null)
            {
                query = query.Where(formFactor => filter.Names.Contains(formFactor.Name));
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

            var formFactors = query.ToList();

            var result = new Object[] { formFactors, count };

            return new OkObjectResult(result);
        }
    }
}