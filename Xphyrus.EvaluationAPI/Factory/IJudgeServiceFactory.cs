using Xphyrus.EvaluationAPI.Service.IService;

namespace Xphyrus.EvaluationAPI.Factory
{
    public interface IJudgeServiceFactory
    {
        IJudgeService CreateScopedJudgeService();
    }
}
