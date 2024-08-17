using EvaluationService;
using Microsoft.AspNetCore.Mvc;

namespace EvaluationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
{
    "Optimize code structure",
    "Improve naming conventions",
    "Enhance algorithm efficiency",
    "Refactor for readability",
    "Address edge cases",
    "Implement error handling",
    "Enhance test coverage",
    "Improve documentation",
    "Apply design patterns",
    "Consider scalability"
};

        private static readonly string[] Emails = new[]
        {
        "a@a.com", "string@s.com", "z@z.com", "p@p.com", "arya.pathak22vit.edu", "qqq@q.com", "www@w.com", "t@t.com", "s@s.com", "cc@cc.com"
    };


        private Random rnd = new Random();


        private bool gener()
        {
            if (rnd.Next() % 2 == 0)
            {
                return true;

            }
            else { return false; }
        }

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetResults/{assignmentId}")]
        public IEnumerable<Result> Get(int assignmentId)
        {
            int n = rnd.Next(Summaries.Length);
            return Enumerable.Range(1, n).Select(index => new Result
            {
                AssignmentId = assignmentId.ToString(),
                isPass = gener(),
                email = Emails[Random.Shared.Next(Summaries.Length)],
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet("GetMyResult/{_email}")]
        public Result GetOne(string _email)
        {
            return new Result
            {
                AssignmentId = rnd.Next().ToString(),
                email = _email,
                isPass = gener(),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };
        }
    }
}