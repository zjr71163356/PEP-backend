using PEP.Models.Domain;

namespace PEP.Repositories.Interface
{
    public interface IPostRepository
    {

        Task<List<Post>?> GetPostsListByProblemIdAsync(bool isSolution, int problemId, int pageNumber, int? pageSize);
        Task<Post?> GetPostByIdAsync(int postId);

        Task<Post?> AddPostAsync(Post post);
        Task<Post?> UpdatePostAsync(int postId, Post post);
        Task<Post?> DeletePostByIdAsync(int postId);

        Task<List<Post>?> GetPostsListByUserIdAsync(int userId, int pageNumber, int? pageSize, bool isSolution = true);

        Task<Comment?> AddCommentAsync(Comment comment);
        Task<Comment?> DeleteCommentByIdAsync(int commentId);

        Task<List<Comment>?> GetCommentsByPostIdAsync(int postId, int pageNumber, int? pageSize);

        Task<Reply?> AddReplyAsync(Reply reply);
        Task<Reply?> DeleteReplyByIdAsync(int replyId);

        //Task<List<Reply>?> GetReplysByPostIdAsync(int commentId, int pageNumber, int? pageSize);



    }
}
