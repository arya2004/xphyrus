using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xphyrus.CreationAPI.Models;
using Xphyrus.CreationAPI.Service.IService;

namespace Xphyrus.CreationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpacesController : ControllerBase
    {   
        private readonly ISpacesService _spacesService;
        public SpacesController(ISpacesService spacesService)
        {
            _spacesService = spacesService;
        }
        [HttpGet]
        public async Task<ActionResult<Spaces>> GetSpace(string SpaceId)
        {
            var Space = await _spacesService.GetSpaces(SpaceId);
            return Ok(Space);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteSpace(string SpaceId)
        {
            bool success = await _spacesService.DeleteSpace(SpaceId);
            return Ok(success);
        }

        [HttpPut]
        public async Task<ActionResult<Spaces>> UpdateSpace(Spaces spaces)
        {
           Spaces updatedSpace = await _spacesService.UpdateSpace(spaces);
            return Ok(updatedSpace);
           
        }
    }
}
