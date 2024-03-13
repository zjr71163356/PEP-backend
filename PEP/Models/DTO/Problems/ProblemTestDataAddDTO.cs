namespace PEP.Models.DTO.Problems
{
    public class ProblemTestDataAddDTO
    {

        public int ProblemId { get; set; }
        public string InputData { get; set; } = null!;

        public string OutputData { get; set; } = null!;
    }
}
