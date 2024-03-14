using Microsoft.EntityFrameworkCore;
using PEP.Data;
using PEP.Models.Domain;
using PEP.Models.DTO.User;
using PEP.Repositories.Interface;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PEP.Repositories.Implement
{
    public class ImpUserRepository : IUserRepository
    {
        private readonly FinalDesignContext dbContext;

        public ImpUserRepository(FinalDesignContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<SubmissionRecord?> AddSubmissionRecord(SubmissionRecord submission)
        {
            await dbContext.SubmissionRecords.AddAsync(submission);
            await dbContext.SaveChangesAsync();
            return submission;
        }

        public async Task<User?> AddUser(User user)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<UserCourse?> AddUserCourseToMyList(UserCourse userCourse)
        {
            var existingUserCourse = await dbContext.UserCourses.FirstOrDefaultAsync(uc => uc.UserId == userCourse.UserId && uc.CourseId == userCourse.CourseId);
            if (existingUserCourse == null)
            {
                await dbContext.UserCourses.AddAsync(userCourse);
                await dbContext.SaveChangesAsync();
                return userCourse;
            }

            return null;
        }

        public async Task<SubmissionRecord?> GetSubmissionRecordById(int recordId)
        {
            var result = await dbContext.SubmissionRecords.FirstOrDefaultAsync(s => s.RecordId == recordId);
            if (result == null)
                return null;

            return result;
        }

        public async Task<List<SubmissionRecord>?> GetSubmissionRecords(int userId, int? problemId, int pageNumber = 1, int? pageSize = null)
        {
            var allSubmissionRecords = dbContext.SubmissionRecords.Where(s => s.UserId == userId).AsQueryable();

            if (problemId != null)
            {
                allSubmissionRecords = allSubmissionRecords.Where(s => s.ProblemId == problemId);
            }

            if (pageSize == null)
            {
                return await allSubmissionRecords.OrderByDescending(s => s.RecordId).ToListAsync();
            }
            else
            {
                int skipResult = (pageNumber - 1) * pageSize.Value;
                return await allSubmissionRecords.OrderByDescending(s => s.RecordId).Skip(skipResult).Take(pageSize.Value).ToListAsync();
            }
        }

        public async Task<User?> GetUserById(int userId)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                return null;
            }

            return user;
        }

        public async Task<List<Course>?> GetUserCoursesListAsync(int userId)
        {
            var userCourseList = await dbContext.UserCourses
                .Where(uc => uc.UserId == userId)
                .Include(uc => uc.Course)
                .ThenInclude(c => c.CourseTags)
                .ToListAsync();

            if (userCourseList.Count == 0)
            {
                return new List<Course>();
            }

            return userCourseList.Select(uc => uc.Course).ToList();
        }

        public async Task<List<User>?> GetUserList(string? fitlerQuery, int pageNumber = 1, int? pageSize = null)
        {
          var UserList  =  dbContext.Users.AsQueryable();
            if (!string.IsNullOrEmpty(fitlerQuery))
            {
                fitlerQuery = fitlerQuery.Trim();
                UserList = UserList.Where(u => u.UserName.Contains(fitlerQuery));
            }

            if (pageSize == null)
            {
                return await UserList.ToListAsync();
            }
            else
            {
                int skipResult = (pageNumber - 1) * pageSize.Value;
                return await UserList.Skip(skipResult).Take(pageSize.Value).ToListAsync();
            }
        }

        public async Task<bool> IsUserAccountTakenAsync(string userAccount)
        {
            return await dbContext.Users.AnyAsync(u => u.Account == userAccount);
        }

        public async Task<bool> IsUserCourseRepeat(int userId, int courseId)
        {
            var result = await dbContext.UserCourses.FirstOrDefaultAsync(uc => uc.UserId == userId && uc.CourseId == courseId);
            if (result == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> IsUsernameTakenAsync(string username)
        {
            return await dbContext.Users.AnyAsync(u => u.UserName == username);
        }

        public async Task<User?> LoginUserAsync(User user)
        {
            var loginResult = await dbContext.Users.FirstOrDefaultAsync(u => u.Account == user.Account && u.Password == user.Password);
            if (loginResult == null)
            {
                return null;
            }
            return loginResult;
        }

        public async Task<bool> RegisterUserAsync(User userRegister)
        {
            userRegister.Role = "User";
            await dbContext.Users.AddAsync(userRegister);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<UserCourse?> RemoveCourseFromMyList(int userId, int courseId)
        {
            var existingUserCourse = await dbContext.UserCourses.FirstOrDefaultAsync(uc => uc.UserId == userId && uc.CourseId == courseId);
            if (existingUserCourse == null)
            {
                return null;
            }
            dbContext.UserCourses.Remove(existingUserCourse);
            await dbContext.SaveChangesAsync();
            return existingUserCourse;
        }

        public async Task<User?> RemoveUser(int userId)
        {
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (existingUser == null)
            {
                return null;
            }

            dbContext.Users.Remove(existingUser);
            await dbContext.SaveChangesAsync();
            return existingUser;
        }

        public async Task<User?> UpdateUserById(int userId, User user)
        {
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (existingUser == null)
            {
                return null;
            }


            //public string UserName { get; set; } = null!;

            //public string Account { get; set; } = null!;

            //public string Password { get; set; } = null!;

            //public string? Avatar { get; set; }

            //public string Role { get; set; } = null!;


            existingUser.UserName = user.UserName;
            existingUser.Account = user.Account;
            existingUser.Password = user.Password;
            existingUser.Avatar = user.Avatar;
            existingUser.Role = user.Role;

            await dbContext.SaveChangesAsync();
            return existingUser;


        }
    }

}
