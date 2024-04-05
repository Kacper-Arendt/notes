using AutoMapper;
using noteApp.Dtos;
using noteApp.Models;

namespace noteApp.MappingProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<NoteForCreateDto, Note>();
        CreateMap<Note, NoteForReadDto>();
        CreateMap<NoteForUpdateDto, Note>();

        CreateMap<UserAuthForReadDto, UserAuthentication>();

        CreateMap<TaskForCreate, TaskItem>();
        CreateMap<TaskItem, TaskForRead>();
        CreateMap<TaskForUpdate, TaskItem>();

        CreateMap<TaskListForCreate, TaskList>();
        CreateMap<TaskList, TaskListForRead>();
        CreateMap<TaskListForUpdate, TaskList>();
    }
}