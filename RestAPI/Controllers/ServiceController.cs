using Microsoft.AspNetCore.Mvc;
using RestAPI.Models.Data;
using RestAPI.Models.Services;
using RestAPI.Models;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly FirstDbContext db;
        private readonly ServicesService service;

        public ServiceController(FirstDbContext _db)
        {
            this.db = _db;
            this.service = new ServicesService(this.db);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetAll()
        {
            return Ok(await service.GetAll());
        }

        [HttpGet("{id}")]
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
