using AutoMapper;

namespace PlugApi.Helpers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        //// CreateAuthorRequest => Author
        //CreateMap<CreateAuthorRequest, Author>();

        //// UpdateAuthorRequest => Author
        //CreateMap<UpdateAuthorRequest, Author>()
        //    .ForAllMembers(x => x.Condition(
        //        (src, dest, prop) =>
        //        {
        //            if (prop == null) return false;
        //            if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

        //            return true;
        //        }
        //      ));

        //// CreateBookRequest => Book
        //CreateMap<CreateBookRequest, Book>();

        //// UpdateBookRequest => Book
        //CreateMap<UpdateBookRequest, Book>();

        //// Book => GetBookResponse
        //CreateMap<Book, GetBooksResponse>();
        //CreateMap<Author, GetAuthorsResponse>();
    }
}
