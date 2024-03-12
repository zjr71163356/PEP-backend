using PEP.Models.Domain;

namespace PEP.Models.DTO.Problems
{
    public class ProblemAddDTO
    {
        public string Title { get; set; } = null!;
        public string Difficulty { get; set; } = null!;
        public string ProblemContent { get; set; } = null!;

        public virtual ICollection<ProblemTagsDTO> ProblemTags { get; set; } = new List<ProblemTagsDTO>();
    }
}
