using AutoMapper;
using PEP.Models.Domain;
using PEP.Models.DTO.Courses;
using PEP.Models.DTO.Courses.Add;
using PEP.Models.DTO.Courses.Presentation;

namespace PEP.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CoursesStepOneDTO, Course>().ReverseMap();
            CreateMap<CoursesStepTwoDTO, Course>().ReverseMap();
            CreateMap<CoursesTagDTO, CourseTag>().ReverseMap();
            CreateMap<AddCoursesChapterDTO, CourseChapter>().ReverseMap();
            CreateMap<AddCoursesSubChapterDTO, SubChapter>().ReverseMap();
        }
    }
}
