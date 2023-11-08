using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmisorController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetAll()
        {
            BL.Emisor emisor = BL.Emisor.GetAllEF();

            if (emisor.Correct)
            {
                return Ok(emisor);
            }
            else
            {
                return BadRequest(emisor);
            }
        }


        [Route("{idEmisor}")]
        [HttpGet]
        public IActionResult GetById(string idEmisor)
        {
            BL.Emisor emisor = BL.Emisor.GetByIdEF(idEmisor);

            if (emisor.Correct)
            {
                return Ok(emisor);
            }
            else
            {
                return BadRequest(emisor);
            }
        }

        [Route("")]
        [HttpPost]
        public IActionResult Add(BL.Emisor emisor)
        {
            string idDevuelto = BL.Emisor.AddEF(emisor);

                if (idDevuelto == "" || idDevuelto == null)
            {
                return BadRequest(idDevuelto);  
            }
            else
            {
                return Ok(idDevuelto);
            }
        }

        [Route("{idEmisor}")]
        [HttpPut]
        public IActionResult Update(string idEmisor,[FromBody]BL.Emisor emisor)
        {
            emisor.IdEmisor = idEmisor;

            string idDevuelto = BL.Emisor.UpdateEF(emisor);

            if (idDevuelto == "" || idDevuelto == null)
            {
                return BadRequest(idDevuelto);  
            }
            else
            {
                return Ok(idDevuelto);
            }
        }


        [Route("{idEmisor}")]
        [HttpDelete]
        public IActionResult Delete(string idEmisor) 
        { 
            string idDevuelto = BL.Emisor.DeleteEF(idEmisor);

            if (idDevuelto == "" || idDevuelto == null)
            {
                return BadRequest(idDevuelto);
            }
            else
            {
                 return Ok(idDevuelto);
            }
        }
    }
}
