using AutoMapper;

namespace apief
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Note, NoteDto>().ReverseMap();
        }
    }
}