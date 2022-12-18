using my_clinic_api.DTOS;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using System.Linq.Expressions;

namespace my_clinic_api.Services
{
    public class BookService : BaseRepository<Book>, IBookService
    {
        public BookService(ApplicationDbContext Context) : base(Context)
        {
        }
        public async Task<IEnumerable<Book>> GetBookingsOfDoctor(string doctorId)
        {
            Expression<Func<Book, bool>> predicate = h => h.DoctorId == doctorId;
            var bookings = await FindAllAsync(predicate);
            if (bookings.Any()) return bookings;
            return Enumerable.Empty<Book>();
        }
        public async Task<bool> IsBookAvailable(BookDto bookDto )
        {
            Expression<Func<Book, bool>> predicate =
                h => h.DoctorId==bookDto.DoctorId;
            var bookings = await FindAllAsync(predicate);
            if (bookings.Where(d=>d.Time.DayOfWeek == bookDto.Time.DayOfWeek).Any(b => b.Time.TimeOfDay == bookDto.Time.TimeOfDay))
            {
                return false;
            }
            return true;
        }
    }
}
