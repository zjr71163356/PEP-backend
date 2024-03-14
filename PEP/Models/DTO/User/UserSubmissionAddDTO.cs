namespace PEP.Models.DTO.User
{
    public class UserSubmissionAddDTO
    {

        public int ProblemId { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string? ResultState { get; set; }

        public string Compiler { get; set; } = null!;

        public decimal? Memory { get; set; }

        public decimal? Runtime { get; set; }

        public string SubmitTime { get; set; } = null!;

        public string? Code { get; set; }

        public string? CompilerOutput { get; set; }
    }
}
