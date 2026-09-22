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
            throw new NotImplementedException();
        }

        public Task<Service> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Service>> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(int id, Service model)
        {
            throw new NotImplementedException();
        }
    }
}
