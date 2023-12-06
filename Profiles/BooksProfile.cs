using AutoMapper;
using Entities;
using Models;

namespace Profiles;

public class BooksProfile : Profile
{
    public BooksProfile()
    {
        CreateMap<Book, BookDto>()
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src =>
                $"{src.Author.FirstName} {src.Author.LastName}"));

        CreateMap<CreateBookDto, Book>();
    }
}
