using Microsoft.EntityFrameworkCore;
using PEP.Data;
using PEP.Models.Domain;
using PEP.Models.DTO.User;
using PEP.Repositories.Interface;

namespace PEP.Repositories.Implement
{
    public class ImpUserRepository : IUserRepository
    {
        private readonly FinalDesignContext dbContext;

        public ImpUserRepository(FinalDesignContext dbContext)
        {

            this.dbContext = dbContext;
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
    }

}
