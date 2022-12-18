using my_clinic_api.DTOS;
using my_clinic_api.Models;

namespace my_clinic_api.Interfaces
{
    public interface IBookService : IBaseRepository<Book> 
    {
        Task<bool> IsBookAvailable(BookDto bookDto);
        Task<IEnumerable<Book>> GetBookingsOfDoctor(string doctorId);

    }
}
