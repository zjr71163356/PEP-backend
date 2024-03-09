namespace PEP.Models.DTO.User
{
    public class UserLoginDTO
    {
        public string Account { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? Role { get; set; } = null!;
    }
}
