using EvaluationService.Dtos;
using EvaluationService.Models;

namespace EvaluationService.Service.IService
{
    public interface IResultService
    {
        Task AddResult(CodingAssessmentSubmission codingAssessmentSubmission, SubmissionStatusResponse submissionStatusResponse);

        Task Migrate();
    }
}
