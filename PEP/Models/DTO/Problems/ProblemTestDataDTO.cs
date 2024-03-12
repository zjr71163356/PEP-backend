namespace PEP.Models.DTO.Problems
{
    public class ProblemTestDataDTO
    {
        public int TestDataId { get; set; }

        public int ProblemId { get; set; }

        public int? SequenceNumber { get; set; }

        public string InputData { get; set; } = null!;

        public string OutputData { get; set; } = null!;

    }
}
