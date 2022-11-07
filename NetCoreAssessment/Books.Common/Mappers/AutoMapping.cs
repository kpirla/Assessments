using AutoMapper;
using Books.DTO.Book;
using Books.Models.Entities;

namespace Books.Common.Mappers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<BookResp, Book>().ReverseMap();
            //CreateMap<Book, BookResp>().ReverseMap();
        }
    }
}
