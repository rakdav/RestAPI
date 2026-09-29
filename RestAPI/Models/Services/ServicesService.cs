using Microsoft.EntityFrameworkCore;
using RestAPI.Models.Abstractions;
using RestAPI.Models.Data;

namespace RestAPI.Models.Services
{
    public class ServicesService : AbstractionService, ICommonService<Service, int>
    {
        private readonly FirstDbContext db;
        private ServicesService(FirstDbContext _db)
        {
            this.db = _db;
        }
        public bool Create(Service model)
        {
            bool result = DoAction(delegate ()
            {
                db.Services.Add(model);
                db.SaveChangesAsync();
            });
            return result;
        }

        public bool Delete(int id)
        {
            bool result = DoAction(delegate ()
            {
                Service res = db.Services.FirstOrDefault(p => p.Code == id)!;
                db.Services.Remove(res);
                db.SaveChangesAsync();
            });
            return result;
        }

        public async Task<Service> Get(int id)
        {
            Service? service = await db.Services.FirstOrDefaultAsync(p => p.Code == id);
            return service!;
        }

        public async Task<IEnumerable<Service>> GetAll()
        {
            return await db.Services.ToListAsync();
        }

        public bool Update(int id, Service model)
        {
            bool result = DoAction(delegate ()
            {
                Service res = db.Services.FirstOrDefault(p => p.Code == id)!;
                res.Cost = model.Cost;
                res.Name = model.Name;
                res.Deadline = model.Deadline;
                res.Average = model.Average;
                db.Services.Update(res);
                db.SaveChangesAsync();
            });
            return result;
        }
    }
}
