using AutoMapper;
using my_clinic_api.Models;

namespace my_clinic_api.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, DTOS.BookDto>();
        }
    }
}
