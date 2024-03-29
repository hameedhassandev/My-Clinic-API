﻿using my_clinic_api.DTOS;
using my_clinic_api.Models;

namespace my_clinic_api.Interfaces
{
    public interface IDoctorService : IBaseRepository<Doctor>
    {
        Task<Doctor> FindDoctorByIdAsync(string doctorId);
        Task<Doctor> FindDoctorByIdWithDataAsync(string doctorId);
        Task<int> GetWaitingTimeOfDoctor(string Id);
        Task<IEnumerable<Doctor>> GetAllConfirmedDoctors();
        Task<IEnumerable<Doctor>> GetAllNotConfirmedDoctors();
        Task<IEnumerable<Doctor>> GetAllDoctorsWithFiltercriteria(FilterDto dto);
        
    }

}
