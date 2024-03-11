namespace PEP.Models.DTO.User
{
    public class UserLoginResultDTO
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = null!;

        public string Account { get; set; } = null!;


        public string? Avatar { get; set; }

        public string Role { get; set; } = null!;
    }
}
