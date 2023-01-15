using Microsoft.EntityFrameworkCore;
using my_clinic_api.Models;

namespace my_clinic_api.Interfaces
{
    public interface IHospitalService : IBaseRepository<Hospital>
    {
        Task<Hospital> FindHospitalByIdWithData(int hospitalId);
        Task<IEnumerable<Hospital>> HospitalNameIsExist(string hospitalName);
        Task<IEnumerable<Hospital>> HospitalAddressIsExist(string hospitalAddress);
        Task<bool> IsHospitalIdsIsExist(List<int> doctorHospitalIds);
        Task<Doctor> AddHospitalToDoctor(List<int> HospitalsIds, Doctor doctor);


    }
}
