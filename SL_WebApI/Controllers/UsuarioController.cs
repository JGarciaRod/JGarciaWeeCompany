using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace SL_WebApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Usuario.GetAll();
            if (result.Correct.Value)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(400, result);
            }
        }

        [HttpGet]
        [Route("GetById/{IdUsuario?}")]
        public IActionResult GetById(int IdUsuario)
        {
            ML.Result result = BL.Usuario.GetById(IdUsuario);
            if (result.Correct.Value)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(400,result);
            }
        }

        [HttpPost]
        [Route("")]
        public IActionResult Add(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.Add(usuario);
            if (result.Correct.Value)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(400, result);
            }
        }

        [HttpPut]
        [Route("{IdUsuario?}")]
        public IActionResult Update(int IdUsuario,[FromBody] ML.Usuario usuario)
        {
            usuario.IdUsuario = IdUsuario;
            ML.Result result = BL.Usuario.Update(usuario);
            if (result.Correct.Value)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(400, result);
            }
        }

        [HttpDelete]
        [Route("{IdUsuario?}")]
        public IActionResult Delete(int IdUsuario)
        {
            ML.Result result = BL.Usuario.Delete(IdUsuario);

            if (result.Correct.Value)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(400, result);
            }
        }
    }
}
