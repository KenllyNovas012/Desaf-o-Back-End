using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Infrastructure.Services;
using System.Threading.Tasks;

namespace System.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersGoalsController : ControllerBase
    {
        private readonly UsersGoalsServices  _usersGoalsServices;

        public UsersGoalsController(UsersGoalsServices  usersGoalsServices)
        {
            _usersGoalsServices = usersGoalsServices;
        }

        // GET: api/UsersGoals
        /// <summary>
        /// Traer un usuario:1. path: /users/{id}
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="UserId"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUser(int UserId)
        {
            try
            {
                var response = await _usersGoalsServices.GetUser(UserId);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest("Hubo  un error al cargar los datos");
            }
        }


        // GET: api/UsersGoals
        /// <summary>
        /// Traer el resumen del usuario actual (considerar moneda)* 
        /// </summary>     
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUsersummary(int UserId)
        {
            try
            {
                var response =  _usersGoalsServices.GetUsersummary(UserId);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest("Hubo  un error al cargar los datos");
            }
        }


        // GET: api/UsersGoals
        /// <summary>
        /// Traer metas de un usuario 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> UsersGoals( int UserId)
        {
            try
            {
                var response = await _usersGoalsServices.UsersGoals( UserId);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest("Hubo  un error al cargar los datos");
            }
        }


        // GET: api/Category
        /// <summary>
        /// Traer detalle de una meta
        /// </summary>      
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> UsersGoalsDetails(int UserId, int GoalsId)
        {
            try
            {
                var response = await _usersGoalsServices.UsersGoalsDetails(UserId, GoalsId);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest("Hubo  un error al cargar los datos");
            }
        }

    }
}
