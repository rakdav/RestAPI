using Microsoft.AspNetCore.Mvc;
using RestAPI.Models.Data;
using RestAPI.Models.Services;
using RestAPI.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Caching.Memory;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly ILogger<ServiceController> logger;
        private readonly FirstDbContext db;
        private readonly ServicesService service;
        private readonly IMemoryCache memoryCache;
        private const string OutOfStockServicesKey = "OOSP";

        public ServiceController(FirstDbContext _db,ILogger<ServiceController> _logger,IMemoryCache _memoryCache)
        {
            this.db = _db;
            this.service = new ServicesService(this.db);
            logger = _logger;
            memoryCache=_memoryCache;
        }
        [HttpGet]
        [Produces(typeof(Service[]))]
        public async Task<ActionResult<IEnumerable<Service>>> GetPage(int? page)
        {
            if (!memoryCache.TryGetValue(OutOfStockServicesKey, out IEnumerable<Service>? cashedvalue))
            {
                cashedvalue =await service.GetPage(page);
                MemoryCacheEntryOptions cacheOptions = new()
                {
                    SlidingExpiration = TimeSpan.FromSeconds(5),
                    Size=cashedvalue.ToArray()?.Length
                };
                memoryCache.Set(OutOfStockServicesKey, cashedvalue);
            }
            MemoryCacheStatistics? stats=memoryCache.GetCurrentStatistics();
            logger.LogInformation($"Memory cache. Total hits: {stats?.TotalHits}. Estimated size: {stats?.CurrentEstimatedSize}.");
            return Ok(cashedvalue??Enumerable.Empty<Service>());
        }

        [HttpGet("{id:int}")]
        [ResponseCache(Duration =5,
            Location =ResponseCacheLocation.Any,
            VaryByHeader ="User-Agent")]
        public async Task<ActionResult<Service>> GetById(int id)
        {
            var res = service.Get(id).Result;
            return res == null ? NotFound(new { message = "Service not found" }) : Ok(res);
        }
        [HttpPost]
        public async Task<ActionResult<Service>> Create([FromBody] Service serv)
        {
            if(service.Create(serv))
                return CreatedAtAction(nameof(GetById), new { id = serv.Code }, serv);
                return BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (service.Delete(id))
                return NoContent();
            return NotFound();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Service>> Update(int id, [FromBody] Service serv)
        {
            if (serv.Code != id) return BadRequest();
            if (service.Update(id, serv))
                return Ok(serv);
            return NotFound();
        }
    }
}
