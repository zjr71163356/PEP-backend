using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PEP.Models.Domain;
using PEP.Models.DTO.Post;
using PEP.Repositories.Interface;

namespace PEP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository impPostRepository;
        private readonly IMapper mapper;


        public PostsController(IPostRepository impPostRepository, IMapper mapper)
        {
            this.impPostRepository = impPostRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllPostsListByProblemId")]
        public async Task<IActionResult> GetAllPostsListByProblemId([FromQuery] bool isSolution, [FromQuery] int problemId, [FromQuery] int pageNumber, [FromQuery] int? pageSize)
        {
            var allPosts = await impPostRepository.GetPostsListByProblemIdAsync(isSolution, problemId, pageNumber, pageSize);
            return Ok(mapper.Map<List<PostsOverviewDTO>>(allPosts));
        }

        [HttpPost]
        [Route("AddPost")]
        public async Task<IActionResult> AddPost([FromBody] PostAddDTO addPostDTO)
        {
            var post = mapper.Map<Post>(addPostDTO);
            var result = await impPostRepository.AddPostAsync(post);
            return Ok(mapper.Map<PostsOverviewDTO>(result));
        }

        [HttpPost]
        [Route("AddComment")]
        public async Task<IActionResult> AddComment([FromBody] PostCommentAddDTO addCommentDTO)
        {
            var comment = mapper.Map<Comment>(addCommentDTO);
            var result = await impPostRepository.AddCommentAsync(comment);
            return Ok(mapper.Map<PostCommentPreDTO>(result));
        }

        [HttpPost]
        [Route("AddReply")]
        public async Task<IActionResult> AddReply([FromBody] PostReplyAddDTO addReplyDTO)
        {
            var reply = mapper.Map<Reply>(addReplyDTO);
            var result = await impPostRepository.AddReplyAsync(reply);
            return Ok(mapper.Map<PostReplyPreDTO>(result));
        }


        [HttpGet]
        [Route("GetPostById/{postId:int}")]
        public async Task<IActionResult> GetPostById([FromRoute] int postId)
        {
            var post = await impPostRepository.GetPostByIdAsync(postId);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<PostsOverviewDTO>(post));
        }
        [HttpGet]
        [Route("GetCommentsByPostId")]
        public async Task<IActionResult> GetCommentsByPostId([FromQuery] int postId, [FromQuery]int pageNumber, [FromQuery] int? pageSize)
        {
            var comments = await impPostRepository.GetCommentsByPostIdAsync(postId,pageNumber,pageSize);
            if (comments == null)
            {
                return NotFound();
            }
            var  result = mapper.Map<List<PostCommentPreDTO>>(comments);
            return Ok(result);


        }

        [HttpDelete]
        [Route("DeletePostById/{postId:int}")]
        public async Task<IActionResult> DeletePostById([FromRoute] int postId)
        {
            var post = await impPostRepository.DeletePostByIdAsync(postId);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<PostsOverviewDTO>(post));
        }
        [HttpDelete]
        [Route("DeleteCommentById/{commentId:int}")]
        public async Task<IActionResult> DeleteCommentById([FromRoute] int commentId)
        {
            var comment = await impPostRepository.DeleteCommentByIdAsync(commentId);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<PostCommentPreDTO>(comment));
        }

        [HttpDelete]
        [Route("DeleteReplyById/{replyId:int}")]
        public async Task<IActionResult> DeleteReplyById([FromRoute] int replyId)
        {
            var reply = await impPostRepository.DeleteReplyByIdAsync(replyId);
            if (reply == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<PostReplyPreDTO>(reply));
        }

        [HttpPut]
        [Route("UpdatePost/{postId:int}")]
        public async Task<IActionResult> UpdatePost([FromRoute] int postId, [FromBody] PostUpdateDTO updatePostDTO)
        {
            var post = mapper.Map<Post>(updatePostDTO);
            var result = await impPostRepository.UpdatePostAsync(postId, post);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<PostsOverviewDTO>(result));
        }

        [HttpGet]
        [Route("GetPostsListByUserId")]
        public async Task<IActionResult> GetPostsListByUserId([FromQuery] int userId, [FromQuery] int pageNumber, [FromQuery] int? pageSize)
        {
            var allPosts = await impPostRepository.GetPostsListByUserIdAsync(userId, pageNumber, pageSize);
            return Ok(mapper.Map<List<PostsOverviewDTO>>(allPosts));
        }
    }
}
