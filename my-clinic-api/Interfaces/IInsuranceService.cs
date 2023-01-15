using my_clinic_api.Models;

namespace my_clinic_api.Interfaces
{
    public interface IInsuranceService : IBaseRepository<Insurance>
    {
        Task<Insurance> FindInsuranceByIdWithData(int insuranceId);
        Task<IEnumerable<Insurance>> InsuranceNameIsExist(string insuranceName);
        Task<bool> IsInsuranceIdsIsExist(List<int> doctorInsuranceIds);
        Task<Doctor> AddInsuranceToDoctor(List<int> InsurancesIds, Doctor doctor);
    }
}
