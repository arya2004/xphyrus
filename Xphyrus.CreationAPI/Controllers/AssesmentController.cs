using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xphyrus.CreationAPI.Data;
using Xphyrus.CreationAPI.Models;
using Xphyrus.CreationAPI.Service.IService;

namespace Xphyrus.CreationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssesmentController : ControllerBase
    {
        private readonly ApplicatioDbContext _applicatioDbContext;
        public AssesmentController(ApplicatioDbContext applicatioDbContext)
        {
            _applicatioDbContext = applicatioDbContext;
        }
        [HttpGet]
        public async Task<object> GetAssesments(int assesmentId)
        {
           Assesment? assesment =   _applicatioDbContext.Assesments.FirstOrDefault(u => u.AssesmentId == assesmentId);
            return assesment;
        }
        [HttpPost]
        public async Task<bool> CreateAssesments(Assesment assesment)
        {
            _applicatioDbContext.Assesments.Add(assesment);
            _applicatioDbContext.SaveChanges();
            return true; 
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteAssesment(int assesmentId )
        {
            Assesment? assesment = _applicatioDbContext.Assesments.First(u => u.AssesmentId == assesmentId);
            _applicatioDbContext.Remove(assesment);
            _applicatioDbContext.SaveChanges(true);
            return Ok(true);
        }

        [HttpPut]
        public async Task<ActionResult<Assesment>> UpdateAssesment(Assesment assesment)
        {
            _applicatioDbContext.Update(assesment);
            _applicatioDbContext.SaveChanges();
            return Ok();
           
        }
    }
}
