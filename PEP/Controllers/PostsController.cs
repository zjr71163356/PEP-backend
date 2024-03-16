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
        public async Task<IActionResult> GetAllPostsListByProblemId([FromQuery]bool isSolution, [FromQuery] int problemId, [FromQuery] int pageNumber, [FromQuery] int? pageSize)
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

        [HttpGet]
        [Route("GetPostById/{postId:int}")]
        public async Task<IActionResult> GetPostById([FromRoute]int postId)
        {
            var post = await impPostRepository.GetPostByIdAsync(postId);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<PostsOverviewDTO>(post));
        }

        [HttpDelete]
        [Route("DeletePostById/{postId:int}")]
        public async Task<IActionResult> DeletePostById([FromRoute]int postId)
        {
            var post = await impPostRepository.DeletePostByIdAsync(postId);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<PostsOverviewDTO>(post));
        }

        [HttpPut]
        [Route("UpdatePost/{postId:int}")]
        public async Task<IActionResult> UpdatePost([FromRoute]int postId, [FromBody] PostUpdateDTO updatePostDTO)
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
        public async Task<IActionResult> GetPostsListByUserId([FromQuery]int userId, [FromQuery]int pageNumber, [FromQuery]int? pageSize)
        {
            var allPosts = await impPostRepository.GetPostsListByUserIdAsync(userId, pageNumber, pageSize);
            return Ok(mapper.Map<List<PostsOverviewDTO>>(allPosts));
        }
    }
}
