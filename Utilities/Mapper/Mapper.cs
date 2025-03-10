using AutoMapper;

namespace apief
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Note, NoteDto>().ReverseMap();
            CreateMap<Note, NoteResponseDto>().ReverseMap();
            CreateMap<NoteDto, NoteResponseDto>().ReverseMap();
            CreateMap<NoteUpdateDto, NoteResponseDto>().ReverseMap();
        }
    }
}