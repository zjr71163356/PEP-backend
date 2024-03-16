namespace PEP.Models.DTO.Post
{
    public class PostAddDTO
    {
        public string Title { get; set; } = null!;

        public int UserId { get; set; }



        public DateTime PostTime { get; set; }

        public string PostContent { get; set; } = null!;

        public bool PostType { get; set; }
 

        public int ProblemId { get; set; }
    }
}
