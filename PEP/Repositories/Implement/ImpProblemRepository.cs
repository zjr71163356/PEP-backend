using Microsoft.EntityFrameworkCore;
using PEP.Data;
using PEP.Models.Domain;
using PEP.Repositories.Interface;

namespace PEP.Repositories.Implement
{
    public class ImpProblemRepository : IProblemRepository
    {
        private readonly FinalDesignContext dbContext;

        public ImpProblemRepository(FinalDesignContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<AlgorithmProblem> AddProblemAsync(AlgorithmProblem problem)
        {
            await dbContext.AlgorithmProblems.AddAsync(problem);
            await dbContext.SaveChangesAsync();
            return problem;

        }

        public async Task<TestDatum> AddTestDataAsync(TestDatum testdata)
        {
            await dbContext.TestData.AddAsync(testdata);
            await dbContext.SaveChangesAsync();
            return testdata;
        }

        public async Task<AlgorithmProblem?> DeleteProblemByIdAsync(int problemId)
        {
            var existingProblem = await dbContext.AlgorithmProblems.Include(p=>p.ProblemTags).Include(p=>p.SubmissionRecords).Include(p=>p.TestData).Include(p=>p.Posts).FirstOrDefaultAsync(p => p.ProblemId == problemId);
            if (existingProblem == null)
            {
                return null;
            }
            dbContext.AlgorithmProblems.Remove(existingProblem);
            await dbContext.SaveChangesAsync();
            return existingProblem;
        }

        public async Task<TestDatum?> DeleteTestDataByIdAsync(int dataId)
        {
            var existingProblemData = await dbContext.TestData.FirstOrDefaultAsync(t => t.TestDataId == dataId);
            if (existingProblemData == null)
            {
                return null;
            }
            dbContext.TestData.Remove(existingProblemData);
            await dbContext.SaveChangesAsync();
            return existingProblemData;
        }

        public async Task<List<AlgorithmProblem>> GetAllProblemsListAsync(string? fitlerQuery, int pageNumber, int? pageSize)
        {
            var allQueryProblems = dbContext.AlgorithmProblems.Include(p => p.ProblemTags).AsQueryable();





            if (!string.IsNullOrEmpty(fitlerQuery))
            {
                fitlerQuery = fitlerQuery.Trim();
                allQueryProblems = allQueryProblems.Where(c => c.Title.Contains(fitlerQuery));
            }

            if (pageSize == null)
            {
                return await allQueryProblems.ToListAsync();
            }
            else
            {
                int skipResult = (pageNumber - 1) * pageSize.Value;
                return await allQueryProblems.Skip(skipResult).Take(pageSize.Value).ToListAsync();
            }
        }

        public async Task<AlgorithmProblem?> GetProblemByIdAsync(int problemId)
        {
            var existingProblem = await dbContext.AlgorithmProblems.FirstOrDefaultAsync(p => p.ProblemId == problemId);
            if (existingProblem == null)
            {
                return null;

            }

            return existingProblem;

        }

        public async Task<List<TestDatum>?> GetProblemTestDataByProblemIdAsync(int problemId)
        {
            var problemTestData = await dbContext.TestData.Where(td => td.ProblemId == problemId).ToListAsync();
            if (problemTestData.Count == 0)
            {
                return null;
            }

            return problemTestData;

        }

        public async Task<TestDatum?> GetTestDataByIdAsync(int dataId)
        {
            var testData = await dbContext.TestData.FirstOrDefaultAsync(td => td.TestDataId == dataId);
            if (testData == null)
            {
                return null;
            }

            return testData;
        }

        public async Task<AlgorithmProblem?> UpdateAlgorithmProblemStepOneAsync(int problemId, AlgorithmProblem problem)
        {

            var existingProblem = await dbContext.AlgorithmProblems.Include(p => p.ProblemTags).FirstOrDefaultAsync(p => p.ProblemId == problemId);
            if (existingProblem == null)
            {
                return null;
            }
            existingProblem.Title = problem.Title;
            existingProblem.Difficulty = problem.Difficulty;
            existingProblem.ProblemTags = problem.ProblemTags;
            existingProblem.ProblemContent = problem.ProblemContent;
            
            await dbContext.SaveChangesAsync();
            return existingProblem;

        }

        public Task<AlgorithmProblem?> UpdateAlgorithmProblemStepTwoAsync(int problemId, AlgorithmProblem problem)
        {
            throw new NotImplementedException();
        }
    }
}
