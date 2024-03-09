using System.ComponentModel.DataAnnotations;

namespace PEP.Models.DTO.User
{
    public class UserRegisterDTO
    {

        public string UserName { get; set; } = null!;


        public string Account { get; set; } = null!;



        public string Password { get; set; } = null!;
    }
}
