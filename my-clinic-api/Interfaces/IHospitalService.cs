using my_clinic_api.Models;

namespace my_clinic_api.Interfaces
{
    public interface IHospitalService : IBaseRepository<Hospital>
    {
        Task<IEnumerable<Hospital>> HospitalNameIsExist(string hospitalName);
        Task<IEnumerable<Hospital>> HospitalAddressIsExist(string hospitalAddress);
    }
}
