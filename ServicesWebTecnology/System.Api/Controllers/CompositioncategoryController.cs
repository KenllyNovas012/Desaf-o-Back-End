using Microsoft.AspNetCore.Mvc;
using System.Infrastructure.DTO;
using System.Infrastructure.Services;
using System.Threading.Tasks;
using System.WebApi.Errors;

namespace System.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompositioncategoryController : ControllerBase
    {
        private readonly CompositioncategoryServices _compositioncategoryServices;

        public CompositioncategoryController(CompositioncategoryServices compositioncategoryServices)
        {
            _compositioncategoryServices = compositioncategoryServices;
        }

        // GET: api/Category
        /// <summary>
        /// Obtiene un resultado paginado de los objetos de la BD.
        /// </summary>
        /// <param name="Search"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCompositioncategory(string Search, int page = 1, int count = 0)
        {
            try
            {
                var response = await _compositioncategoryServices.Get(Search, page, count);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest("Hubo  un error al cargar los datos");
            }
        }


        // GET: api/Category/GetCategory/5
        /// <summary>
        /// Obtiene  la categoria atravéz de su Id.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id">Id (GUID) del objeto.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>      
        [HttpGet("[action]/{id}", Name = "GetCategory")]
        public async Task<IActionResult> GetCompositioncategory(int id)
        {
            try
            {
                var Language = await _compositioncategoryServices.GetByIdAsync(id);
                if (Language == null)
                {
                    return NotFound(new CodeErrorResponse(404));
                }
                return Ok(Language);
            }
            catch (Exception)
            {
                return BadRequest("Hubo  un error al cargar");
            }

        }

        // Post: api/Category/PostCategory
        /// <summary>
        /// Metodo utilizado para guardar los categoria
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="model"></param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="201">OK. Devuelve el objeto creado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response> 
        [HttpPost("[action]")]
        public async Task<IActionResult> PostCompositioncategory([FromForm] CompositioncategoryDto model)
        {

            try
            {
                var result = await _compositioncategoryServices.Add(model);
                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest("Hubo  un error al guardar" + e.Message);
            }
        }

        // Update: api/Category/UpdateCategory
        /// <summary>
        /// Metodo utilizado para actualizar los categoria
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="model"></param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="201">OK. Devuelve el objeto creado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response> 
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateCompositioncategory([FromBody] CompositioncategoryDto model)
        {
            try
            {
                var result = await _compositioncategoryServices.Update(model);
                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest("Hubo  un error al cargar" + e.Message);
            }

        }

        // Update: api/Category/5
        /// <summary>
        /// Metodo utilizado para eliminar los paises
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id"></param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="201">OK. Devuelve el objeto creado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response> 
        [HttpDelete("[action]/")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _compositioncategoryServices.Delete(id);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest("Hubo  un error al cargar");
            }


        }
    }
}
