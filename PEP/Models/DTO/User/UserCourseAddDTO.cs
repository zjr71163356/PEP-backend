namespace PEP.Models.DTO.User
{
    public class UserCourseAddDTO
    {

        public int UserId { get; set; }

        public int CourseId { get; set; }

        public bool? IsFavorite { get; set; } = true;
    }
}
