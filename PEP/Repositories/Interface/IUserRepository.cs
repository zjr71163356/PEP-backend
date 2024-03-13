using Microsoft.AspNetCore.Mvc;
using PEP.Models.Domain;
using PEP.Models.DTO.User;

namespace PEP.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<bool> RegisterUserAsync(User user);

        Task<bool> IsUsernameTakenAsync(string username);

        Task<bool> IsUserAccountTakenAsync(string userAccount);

        Task<User?> LoginUserAsync(User user);

        Task<List<Course>?> GetUserCoursesListAsync(int userId);

        Task<UserCourse?> AddUserCourseToMyList(UserCourse userCourse);

        Task<bool> IsUserCourseRepeat(int userId,int courseId);

        Task<UserCourse?> RemoveCourseFromMyList(int userId,int courseId);

        Task<SubmissionRecord?> AddSubmissionRecord(SubmissionRecord submission);

        Task<List<SubmissionRecord>?> GetSubmissionRecordsByUserId(int userId);
    }
}
