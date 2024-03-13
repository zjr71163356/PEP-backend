using PEP.Models.Domain;

namespace PEP.Repositories.Interface
{
    public interface IProblemRepository
    {

        //teacher/admin manage all the courses
        Task<List<AlgorithmProblem>> GetAllProblemsListAsync(string? fitlerQuery, int pageNumber, int? pageSize);
        Task<AlgorithmProblem?> GetProblemByIdAsync(int problemId);

        Task<AlgorithmProblem> AddProblemAsync(AlgorithmProblem problem);

        Task<AlgorithmProblem?> DeleteProblemByIdAsync(int problemId);
        Task<AlgorithmProblem?> UpdateAlgorithmProblemStepOneAsync(int problemId, AlgorithmProblem problem);

        Task<AlgorithmProblem?> UpdateAlgorithmProblemStepTwoAsync(int problemId, AlgorithmProblem problem);

        Task<TestDatum?>UpdateTestDataAsync(int dataId, TestDatum testdata);

        Task<List<TestDatum>?> GetProblemTestDataByProblemIdAsync(int problemId);
        Task<TestDatum?> GetTestDataByIdAsync(int dataId);

        Task<TestDatum> AddTestDataAsync(TestDatum testdata);

        Task<TestDatum?> DeleteTestDataByIdAsync(int dataId);




    }
}
