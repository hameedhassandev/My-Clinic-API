using my_clinic_api.Interfaces;
using my_clinic_api.Models;

namespace my_clinic_api.Services
{
    public class BookService : BaseRepository<Book>, IBookService
    {
        public BookService(ApplicationDbContext Context) : base(Context)
        {
        }
    }
}
