using my_clinic_api.Models;

namespace my_clinic_api.Interfaces
{
    public interface IPatientService : IBaseRepository<Patient>
    {
        Task<Patient> FindPatientByIdAsync(string patientId);
        Task<Patient> FindPatientByIdWithIncludeAsync(string patientId);
    }
}
