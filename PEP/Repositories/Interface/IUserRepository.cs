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

        Task<User?> AddUser(User user);
        Task<bool> IsUserCourseRepeat(int userId, int courseId);

        Task<UserCourse?> RemoveCourseFromMyList(int userId, int courseId);

        Task<User?> RemoveUser(int userId);

        Task<SubmissionRecord?> AddSubmissionRecord(SubmissionRecord submission);

        Task<List<SubmissionRecord>?> GetSubmissionRecords(int userId, int? problemId, int pageNumber = 1, int? pageSize = null);
 
        Task<List<User>?> GetUserList(string? fitlerQuery, int pageNumber = 1, int? pageSize = null);

        Task<SubmissionRecord?> GetSubmissionRecordById(int recordId);

        Task<User?> UpdateUserById(int userId, User user);

        Task<User?> GetUserById(int userId);

    }
}
