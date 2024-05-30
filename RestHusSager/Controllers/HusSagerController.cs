using HusSagLib;
using Microsoft.AspNetCore.Mvc;

namespace RestHusSager.Controllers
{
    [Route("api/hussager")]
    [ApiController]
    public class HusSagerController : ControllerBase
    {
        private readonly HusSagRepository _repo = new HusSagRepository();

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
      
        public IActionResult Get()
        {
            return Ok(_repo.GetAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var husSag = _repo.GetById(id);
            if (husSag == null)
            {
                return NotFound();
            }
            return Ok(husSag);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] HusSag husSag)
        {
            if (husSag == null)
            {
                return BadRequest();
            }
            _repo.Add(husSag);
            return CreatedAtAction(nameof(Get), new { id = husSag.Id }, husSag);
        }
    }
}
