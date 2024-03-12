using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PEP.Models.Domain;
using PEP.Models.DTO.Problems;
using PEP.Repositories.Interface;

namespace PEP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemsController : ControllerBase
    {
        private readonly IProblemRepository impProblemRepository;
        private readonly IMapper mapper;

        public ProblemsController(IProblemRepository impProblemRepository, IMapper mapper)
        {
            this.impProblemRepository = impProblemRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProblemsList([FromQuery] string? fitlerQuery = null, [FromQuery] int pageNumber = 1, [FromQuery] int? pageSize = null)
        {
            var allProblems = await impProblemRepository.GetAllProblemsListAsync(fitlerQuery, pageNumber, pageSize);

            return Ok(mapper.Map<List<ProblemOverViewDTO>>(allProblems));
        }
        [HttpGet]
        [Route("GetTestDatasByProblemId/{problemId:int}")]
        public async Task<IActionResult> GetTestDatasByProblemId([FromRoute] int problemId)
        {
            var allProblemTestData = await impProblemRepository.GetProblemTestDataByProblemIdAsync(problemId);

            if (allProblemTestData == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<ProblemTestDataDTO>>(allProblemTestData));
        }

        [HttpGet]
        [Route("GetProblemById")]
        public async Task<IActionResult> GetProblemById([FromQuery] int problemId)
        {
            var problem = await impProblemRepository.GetProblemByIdAsync(problemId);
            if (problem == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ProblemDescDTO>(problem));
        }

        [HttpPost]
        [Route("AddProblem")]
        public async Task<IActionResult> AddProblem([FromBody] ProblemAddDTO problemAddDTO)
        {
            var problem = mapper.Map<AlgorithmProblem>(problemAddDTO);
            var result = await impProblemRepository.AddProblemAsync(problem);

            return Ok(mapper.Map<ProblemDescDTO>(result));
        }

        [HttpDelete]
        [Route("DeleteProblem")]
        public async Task<IActionResult> DeleteProblem([FromQuery] int problemId)
        {

            var result = await impProblemRepository.DeleteProblemByIdAsync(problemId);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<ProblemAddDTO>(result));
        }


    }
}
