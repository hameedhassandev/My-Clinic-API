using my_clinic_api.Models;

namespace my_clinic_api.Interfaces
{
    public interface IHospitalService : IBaseRepository<Hospital>
    {
        IEnumerable<Hospital> LargeHospitals();
    }
}
