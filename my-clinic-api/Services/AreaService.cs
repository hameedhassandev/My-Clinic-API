using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using System.Linq.Expressions;

namespace my_clinic_api.Services
{
    public class AreaService : BaseRepository<Area>, IAreaService
    {
        public AreaService(ApplicationDbContext Context) : base(Context)
        {
        }

        public async Task<bool>  AreaIdIsExist(int id)
        {
            var isExist = await FindByIdAsync(id);
            if(isExist == null) return false;
            return true;
        }

        public async Task<IEnumerable<Area>> AreaNameIsExist(string areaName)
        {
            Expression<Func<Area, bool>> predicate = a => a.AreaName.Equals(areaName);
            var area = await FindAllAsync(predicate);
            if (area.Any()) return area;
            return Enumerable.Empty<Area>();
        }
    }
}
