using AutoMapper;
using note.Dtos;
using note.Models;

namespace note.MappingProfiles;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<NoteForCreateDto, Note>();
        CreateMap<Note, NoteForReadDto>();
        CreateMap<NoteForUpdateDto, Note>();
    }
}