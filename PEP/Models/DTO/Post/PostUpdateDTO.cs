namespace PEP.Models.DTO.Post
{
    public class PostUpdateDTO
    {
 

        public string Title { get; set; } = null!;

        public DateTime PostTime { get; set; }

        public string PostContent { get; set; } = null!;
    }
}
