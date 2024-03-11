using AutoMapper;
using PEP.Models.Domain;
using PEP.Models.DTO.Courses;
using PEP.Models.DTO.Courses.Add;
using PEP.Models.DTO.Courses.Both;
using PEP.Models.DTO.Courses.Presentation;
using PEP.Models.DTO.User;

namespace PEP.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CoursesOverviewDTO, Course>().ReverseMap();
            CreateMap<CoursesStepOneDTO, Course>().ReverseMap();
            CreateMap<CoursesStepTwoDTO, Course>().ReverseMap();
            CreateMap<CourseDescDTO, Course>().ReverseMap();

            CreateMap<CoursesTagDTO, CourseTag>().ReverseMap();


            CreateMap<PreCoursesChapterDTO, CourseChapter>().ReverseMap();
            CreateMap<AddCoursesChapterDTO, CourseChapter>().ReverseMap();
            CreateMap<AddCoursesChapterByCourseIdDTO, CourseChapter>().ReverseMap();


            CreateMap<AddCoursesSubChapterDTO, SubChapter>().ReverseMap();
            CreateMap<AddCoursesSubChapterByChapterIdDTO, SubChapter>().ReverseMap();
            CreateMap<PreCoursesSubChapterDTO, SubChapter>().ReverseMap();
            CreateMap<PreCoursesSubChapterWithMDDTO, SubChapter>().ReverseMap();
            CreateMap<SubChapterMDContentDTO, SubChapter>().ReverseMap();

            CreateMap<UserRegisterDTO, User>().ReverseMap();
            CreateMap<UserLoginDTO, User>().ReverseMap();
        }
    }
}
