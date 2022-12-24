using my_clinic_api.Models;

namespace my_clinic_api.Interfaces
{
    public interface IPatientService : IBaseRepository<Patient>
    {
        Task<IEnumerable<Patient>> GetAllPatientsWithData();
        Task<Patient> FindPatientByIdAsync(string patientId);
        Task<Patient> FindPatientByIdWithData(string patientId);
    }
}
