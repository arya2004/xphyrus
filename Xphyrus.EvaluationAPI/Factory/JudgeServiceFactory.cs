using Xphyrus.EvaluationAPI.Service.IService;

namespace Xphyrus.EvaluationAPI.Factory
{
    public class JudgeServiceFactory : IJudgeServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public JudgeServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJudgeService CreateScopedJudgeService()
        {
            // Create a new scoped instance of IJudgeService
            var scope = _serviceProvider.CreateScope();
            return scope.ServiceProvider.GetRequiredService<IJudgeService>();
        }
    }
}
