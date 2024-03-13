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
        [Route("GetTestData/{testId:int}")]
        public async Task<IActionResult> GetTestData([FromRoute] int testId)
        {
            var allProblemTestData = await impProblemRepository.GetTestDataByIdAsync(testId);

            if (allProblemTestData == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map< ProblemTestDataDTO >(allProblemTestData));
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


        [HttpPost]
        [Route("AddTestData")]
        public async Task<IActionResult> AddTestData([FromBody] ProblemTestDataAddDTO problemTestDataAddDTO)
        {
            var testData = mapper.Map<TestDatum>(problemTestDataAddDTO);
            var result = await impProblemRepository.AddTestDataAsync(testData);

            return Ok(mapper.Map<ProblemTestDataDTO>(result));
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

        [HttpDelete]
        [Route("DeleteTestData/{testId:int}")]
        public async Task<IActionResult> DeleteTestData([FromRoute] int testId)
        {

            var result = await impProblemRepository.DeleteTestDataByIdAsync(testId);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<ProblemTestDataDTO>(result));
        }
        
        [HttpPut]
        [Route("UpdateTestData/{testId:int}")]
        public async Task<IActionResult> UpdateTestData([FromRoute] int testId, [FromBody] ProblemTestDataAddDTO problemUpdateTestDataDTO)
        {
            var testData = mapper.Map<TestDatum>(problemUpdateTestDataDTO);
            var result = await impProblemRepository.UpdateTestDataAsync(testId, testData);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<ProblemTestDataDTO>(result));
        }

        [HttpPut]
        [Route("UpdateProblemStepOne/{problemId:int}")]
        public async Task<IActionResult> UpdateProblemStepOne([FromRoute] int problemId, [FromBody] ProblemAddDTO problemUpdateStepOneDTO)
        {
            var problem = mapper.Map<AlgorithmProblem>(problemUpdateStepOneDTO);
            var result = await impProblemRepository.UpdateAlgorithmProblemStepOneAsync(problemId, problem);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<ProblemDescDTO>(result));
        }
    }
}
