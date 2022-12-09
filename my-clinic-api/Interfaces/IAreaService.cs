using my_clinic_api.Models;

namespace my_clinic_api.Interfaces
{
    public interface IAreaService : IBaseRepository<Area>
    {
        Task<IEnumerable<Area>> AreaNameIsExist(string areaName);

        public Task<bool> AreaIdIsExist(int id);
     
    }
}
