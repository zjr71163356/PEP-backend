using PEP.Models.Domain;

namespace PEP.Models.DTO.Problems
{
    public class ProblemOverViewDTO
    {

        public int ProblemId { get; set; }

        public string Title { get; set; } = null!;

        public int? AcceptRate { get; set; }

        public string Difficulty { get; set; } = null!;

        public virtual ICollection<ProblemTagsDTO> ProblemTags { get; set; } = new List<ProblemTagsDTO>();
    }
}
