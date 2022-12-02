using my_clinic_api.Models;

namespace my_clinic_api.Interfaces
{
    public interface IInsuranceService : IBaseRepository<Insurance>
    {
        Task<IEnumerable<Insurance>> InsuranceNameIsExist(string insuranceName);
    }
}
