using EvaluationService.Service.IService;

namespace EvaluationService.Factory
{
    public interface IJudgeServiceFactory
    {
        IJudgeService CreateScopedJudgeService();
    }
}
