using Microsoft.EntityFrameworkCore;
using PEP.Data;
using PEP.Models.Domain;
using PEP.Repositories.Interface;

namespace PEP.Repositories.Implement
{
    public class ImpPostRepository : IPostRepository
    {
        private readonly FinalDesignContext dbContext;

        public ImpPostRepository(FinalDesignContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Comment?> AddCommentAsync(Comment comment)
        {
            var existingPost = await dbContext.Posts.FirstOrDefaultAsync(p => p.PostId == comment.PostId);


            if (existingPost == null)
            {
                return null;
            }

            existingPost.PostTime = DateTime.Now;
            comment.Timestamp = DateTime.Now;
            await dbContext.Comments.AddAsync(comment);
            await dbContext.SaveChangesAsync();
            return comment;
        }

        public async Task<Post?> AddPostAsync(Post post)
        {
            post.PostTime = DateTime.Now;
            await dbContext.Posts.AddAsync(post);
            await dbContext.SaveChangesAsync();
            return post;
        }

        public async Task<Reply?> AddReplyAsync(Reply reply)
        {
            reply.Timestamp = DateTime.Now;
            await dbContext.Replies.AddAsync(reply);
            await dbContext.SaveChangesAsync();
            return reply;
        }

        public async Task<UserLike?> AddUserLikeAsync(UserLike userLike)
        {
            var existingLike = await dbContext.UserLikes.FirstOrDefaultAsync(ul => ul.UserId == userLike.UserId && ul.PostId == userLike.PostId);
            if (existingLike != null)
            {
                return null;
            }
            var existingPost = await dbContext.Posts.FirstOrDefaultAsync(p => p.PostId == userLike.PostId);
            if (existingPost == null)
            {
                return null;
            }
            existingPost.Likes++;
            await dbContext.UserLikes.AddAsync(userLike);
            await dbContext.SaveChangesAsync();
            return userLike;
        }

        public async Task<Comment?> DeleteCommentByIdAsync(int commentId)
        {
            var existingComment = await dbContext.Comments.Include(c => c.Replies).FirstOrDefaultAsync(c => c.CommentId == commentId);
            if (existingComment == null)
            {
                return null;
            }
            dbContext.Comments.Remove(existingComment);
            await dbContext.SaveChangesAsync();
            return existingComment;
        }


        public async Task<Post?> DeletePostByIdAsync(int postId)
        {
            var existingPost = await dbContext.Posts.Include(p => p.Comments).ThenInclude(c => c.Replies).FirstOrDefaultAsync(p => p.PostId == postId);
            if (existingPost == null)
            {
                return null;
            }

            dbContext.Posts.Remove(existingPost);
            await dbContext.SaveChangesAsync();
            return existingPost;

        }

        public async Task<Reply?> DeleteReplyByIdAsync(int replyId)
        {
            var existingReply = await dbContext.Replies.FirstOrDefaultAsync(r => r.ReplyId == replyId);
            if (existingReply == null)
            {
                return null;
            }
            dbContext.Replies.Remove(existingReply);
            await dbContext.SaveChangesAsync();
            return existingReply;
        }

        public async Task<UserLike?> DeleteUserLikeAsync(int userId, int postId)
        {
            var existingUserLike = await dbContext.UserLikes.FirstOrDefaultAsync(ul => ul.UserId == userId && ul.PostId == postId);
            if (existingUserLike == null)
            {
                return null;
            }
            var existingPost = await dbContext.Posts.FirstOrDefaultAsync(p => p.PostId == postId);
            if (existingPost == null)
            {
                return null;
            }
            existingPost.Likes--;
            dbContext.UserLikes.Remove(existingUserLike);
            await dbContext.SaveChangesAsync();
            return existingUserLike;
        }

        public async Task<List<Comment>?> GetCommentsByPostIdAsync(int postId, int pageNumber, int? pageSize)
        {
            var comments = dbContext.Comments.Include(c => c.FromUser).Include(c => c.Replies).ThenInclude(r => r.FromUser).Where(c => c.PostId == postId).OrderBy(c => c.Timestamp);

            if (pageSize != null)
            {
                int skipResult = (pageNumber - 1) * pageSize.Value;
                return await comments.Skip(skipResult).Take(pageSize.Value).ToListAsync();
            }
            else
            {
                return await comments.ToListAsync();
            }


        }

        public async Task<Post?> GetPostByIdAsync(int postId)
        {
            var result = await dbContext.Posts.FirstOrDefaultAsync(p => p.PostId == postId);
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public async Task<List<Post>?> GetPostsListByProblemIdAsync(bool isSolution, int problemId, int pageNumber, int? pageSize)
        {
            var postListQuery = dbContext.Posts.Include(p => p.User).OrderByDescending(p => p.PostTime).AsQueryable();
            postListQuery = postListQuery.Where(p => p.ProblemId == problemId && p.PostType == isSolution);

            if(isSolution)
            {
                postListQuery = postListQuery.OrderByDescending(p=>p.Likes);
            }
            if (pageSize == null)
            {
                return await postListQuery.ToListAsync();
            }
            else
            {
                int skipResult = (pageNumber - 1) * pageSize.Value;
                return await postListQuery.Skip(skipResult).Take(pageSize.Value).ToListAsync();
            }
        }

        public async Task<List<Post>?> GetPostsListByUserIdAsync(int userId, int pageNumber, int? pageSize, bool isSolution = true)
        {
            var existingPost = dbContext.Posts.Where(p => p.UserId == userId && p.PostType == isSolution).AsQueryable();
            if (pageSize == null)
            {
                return await existingPost.ToListAsync();
            }
            else
            {
                int skipResult = (pageNumber - 1) * pageSize.Value;
                return await existingPost.Skip(skipResult).Take(pageSize.Value).ToListAsync();
            }

        }



        public async Task<Post?> UpdatePostAsync(int postId, Post post)
        {
            var result = await dbContext.Posts.FirstOrDefaultAsync(p => p.PostId == postId);
            if (result == null)
            {
                return null;
            }
            result.PostContent = post.PostContent;
            result.PostTime = DateTime.Now;
            result.Title = post.Title;
            await dbContext.SaveChangesAsync();

            return result;

        }
    }
}
