﻿using AutoMapper;
using PEP.Models.Domain;
using PEP.Models.DTO.Courses;
using PEP.Models.DTO.Courses.Add;
using PEP.Models.DTO.Courses.Both;
using PEP.Models.DTO.Courses.Presentation;
using PEP.Models.DTO.Post;
using PEP.Models.DTO.Problems;
using PEP.Models.DTO.User;

namespace PEP.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //about Course
            CreateMap<CoursesOverviewDTO, Course>().ReverseMap();
            CreateMap<CoursesStepOneDTO, Course>().ReverseMap();
            CreateMap<CoursesStepTwoDTO, Course>().ReverseMap();
            CreateMap<CourseDescDTO, Course>().ReverseMap();

            //about CourseTag
            CreateMap<CoursesTagDTO, CourseTag>().ReverseMap();


            //about CourseChapter
            CreateMap<PreCoursesChapterDTO, CourseChapter>().ReverseMap();
            CreateMap<AddCoursesChapterDTO, CourseChapter>().ReverseMap();
            CreateMap<AddCoursesChapterByCourseIdDTO, CourseChapter>().ReverseMap();

            //about CourseSubChapter
            CreateMap<AddCoursesSubChapterDTO, SubChapter>().ReverseMap();
            CreateMap<AddCoursesSubChapterByChapterIdDTO, SubChapter>().ReverseMap();
            CreateMap<PreCoursesSubChapterDTO, SubChapter>().ReverseMap();
            CreateMap<PreCoursesSubChapterWithMDDTO, SubChapter>().ReverseMap();
            CreateMap<SubChapterMDContentDTO, SubChapter>().ReverseMap();


            //about User
            CreateMap<UserRegisterDTO, User>().ReverseMap();
            CreateMap<UserLoginDTO, User>().ReverseMap();
            CreateMap<UserLoginResultDTO, User>().ReverseMap();
            CreateMap<UserAvatarDTO, User>().ReverseMap();

            //about UserCourse
            CreateMap<UserCourseAddDTO, UserCourse>().ReverseMap();


            //about ProblemTag
            CreateMap<ProblemTagsDTO, ProblemTag>().ReverseMap();

            //about Problem
            CreateMap<ProblemOverViewDTO, AlgorithmProblem>().ReverseMap();
            CreateMap<ProblemAddDTO, AlgorithmProblem>().ReverseMap();
            CreateMap<ProblemDescDTO, AlgorithmProblem>().ReverseMap();

            //about ProblemTestData
            CreateMap<ProblemTestDataDTO, TestDatum>().ReverseMap();
            CreateMap<ProblemTestDataAddDTO, TestDatum>().ReverseMap();

            // about SubmissionRecord
            CreateMap<UserSubmissionAddDTO, SubmissionRecord>().ReverseMap();
            CreateMap<UserSubmissionPreDTO, SubmissionRecord>().ReverseMap();

            //about User
            CreateMap<UserAddDTO, User>().ReverseMap();
            CreateMap<UserPreDTO, User>().ReverseMap();
            CreateMap<UserProfileEditDTO, User>().ReverseMap();
            CreateMap<UserAvatarDTO, User>().ReverseMap();
            CreateMap<UserPWEditDTO, User>().ReverseMap();

            //about Post
            CreateMap<PostsOverviewDTO, Post>().ReverseMap();
            CreateMap<PostAddDTO, Post>().ReverseMap();
            CreateMap<PostUpdateDTO, Post>().ReverseMap();

            //about Comment
            CreateMap<PostCommentAddDTO, Comment>().ReverseMap();
            CreateMap<PostCommentPreDTO, Comment>().ReverseMap();

            //about Reply
            CreateMap<PostReplyAddDTO, Reply>().ReverseMap();
            CreateMap<PostReplyPreDTO, Reply>().ReverseMap();

            //




        }
    }
}
