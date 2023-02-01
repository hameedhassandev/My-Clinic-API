using my_clinic_api.DTOS;
using my_clinic_api.DTOS.CreateDto;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using System.Linq.Expressions;

namespace my_clinic_api.Services
{
    public class BookService : BaseRepository<Book>, IBookService
    {
        private readonly ITimesOfWorkService _timesOfWorkService;
        private readonly IDoctorService _doctorService;
        public BookService(ApplicationDbContext Context, ITimesOfWorkService timesOfWorkService, IDoctorService doctorService) : base(Context)
        {
            _timesOfWorkService = timesOfWorkService;
            _doctorService = doctorService;
        }

        public async Task<string> AddBook(CreateBookDto bookDto)
        {
            //check that the time not passed
            if (bookDto.Time < DateTime.Now) return "This time has passed!";
            //Check if doctor has no working days
            var allTimes = await _timesOfWorkService.GetTimesOfDoctor(bookDto.DoctorId);
            if (allTimes == null || !allTimes.Any()) return "This doctor is not available!";
            // Get working Times Of specific DayOfWeak that user chooses 
            var timeOfDay = allTimes.SingleOrDefault(d => d.day.ToString() == bookDto.Time.DayOfWeek.ToString());
            if (timeOfDay == null) return "The doctor is not available at this day!";
            //Get List of doctor times withen waiting time;
            var watingTime = await _doctorService.GetWaitingTimeOfDoctor(bookDto.DoctorId);
            var listOftimes = _timesOfWorkService.GetDoctorAllTimesSpans(timeOfDay.StartWork, timeOfDay.EndWork, watingTime);
            if (!listOftimes.Any(t => t == bookDto.Time.TimeOfDay)) return "This time is invalid for this doctor!";
            //Ensure that the time is in range of doctor's working time
            var checkTimeIsAvailable = await _timesOfWorkService.TimeIsAvailable(bookDto);
            if (!checkTimeIsAvailable) return "The doctor is not available at this time!";
            //Ensure that no bookings at same time 
            var checkBookIsAvailable = await IsBookAvailable(bookDto);
            if (!checkBookIsAvailable) return "This time is already booked!";

            var book = new Book
            {
                DoctorId = bookDto.DoctorId,
                PatientId = bookDto.PatientId,
                Time = bookDto.Time,
                CreatedAt = DateTime.Now,
                ExpiryDate = new DateTime(bookDto.Time.Year, bookDto.Time.Month,
                    bookDto.Time.Day, timeOfDay.EndWork.Hours,
                    timeOfDay.EndWork.Minutes,
                    timeOfDay.EndWork.Seconds),
            };
            var result = await AddAsync(book);
            CommitChanges();
            return "Success";
        }
        public async Task<IEnumerable<Book>> GetBookingsOfDoctor(string doctorId)
        {
            Expression<Func<Book, bool>> predicate = h => h.DoctorId == doctorId;
            var bookings = await FindAllAsync(predicate);
            if (bookings.Any()) return bookings;
            return Enumerable.Empty<Book>();
        }
        public async Task<bool> IsBookAvailable(CreateBookDto bookDto )
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
