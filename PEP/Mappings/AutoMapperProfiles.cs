using AutoMapper;
using PEP.Models.Domain;
using PEP.Models.DTO.Courses;
using PEP.Models.DTO.Courses.Add;
using PEP.Models.DTO.Courses.Both;
using PEP.Models.DTO.Courses.Presentation;

namespace PEP.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CoursesStepOneDTO, Course>().ReverseMap();
            CreateMap<CoursesStepTwoDTO, Course>().ReverseMap();
            CreateMap<CoursesTagDTO, CourseTag>().ReverseMap();
            CreateMap<CoursesOverviewDTO, Course>().ReverseMap();
            CreateMap<CourseDescDTO, Course>().ReverseMap();
            CreateMap<AddCoursesChapterDTO, CourseChapter>().ReverseMap();
            CreateMap<AddCoursesSubChapterDTO, SubChapter>().ReverseMap();
        }
    }
}
