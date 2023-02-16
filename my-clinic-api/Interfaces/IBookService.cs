using my_clinic_api.DTOS;
using my_clinic_api.DTOS.CreateDto;
using my_clinic_api.Models;

namespace my_clinic_api.Interfaces
{
    public interface IBookService : IBaseRepository<Book> 
    {
        Task<string> AddBook(CreateBookDto bookDto);
        Task<bool> IsBookAvailable(CreateBookDto bookDto);
        Task<bool> ConfirmBook(int confirmId);

        Task<IEnumerable<Book>> GetBookingsOfDoctor(string doctorId);
        Task<IEnumerable<Book>> GetBookingsOfPatient(string patientId);


    }
}
