using Microsoft.EntityFrameworkCore;
using my_clinic_api.Models;

namespace my_clinic_api.Interfaces
{
    public interface IHospitalService : IBaseRepository<Hospital>
    {
        Task<IEnumerable<Hospital>> HospitalNameIsExist(string hospitalName);
        Task<IEnumerable<Hospital>> HospitalAddressIsExist(string hospitalAddress);
        Task<bool> IsHospitalIdsIsExist(List<int> doctorHospitalIds);
        Task<bool> AddHospitalToDoctor(string doctorId, int HospitalId);


    }
}
