using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Infrastructure.Configuration;
using System.Infrastructure.DTO;
using System.Infrastructure.Helpers;
using System.Infrastructure.IRepository;
using System.Linq;
using System.Threading.Tasks;

namespace System.Infrastructure.Services
{
    public class UsersGoalsServices
    {
        private readonly IUsersRepository _iusersRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersGoalsServices> _logger;
        private int _pagination;
        public UsersGoalsServices(IConfiguration config, IMapper mapper, IUsersRepository  usersRepository, ILogger<UsersGoalsServices> logger)
        {
            _iusersRepository = usersRepository;
            _mapper = mapper;
            _logger = logger;
            _pagination = Convert.ToInt32(config["Paginator"]);
        }

        public Task<Result<UsersDto>> GetUser(int UserId, int page = 1, int count = 0)
        {
            try
            {
                var Entities = _iusersRepository.GetUser(UserId).ToList();                

                var results = _mapper.Map<List<UsersDto>>(Entities);
                var _count = Entities.Count();
                var paginador = new GenericPage<UsersDto>();
                var result = paginador.Get(results, _count, page, count == 0 ? _pagination : count);
                return Task.FromResult(result);

            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al cargar  {0}, {@e}", e.Message, e);
                throw;
            }

        }
        public Response GetUsersummary(int UserId)
        {
            try
            {
                var Entities = _iusersRepository.GetUsersummary(UserId);
                if (Entities == null)
                    return new Response {  IsSuccess = false, Message = "Datos no encontrado" };

                 return new Response { IsSuccess = true, Result = Entities };
            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al cargar  {0}, {@e}", e.Message, e);
                throw;
            }

        }
        public Task<Result<UsersGoalsDto>> UsersGoals( int UserId, int page = 1, int count = 0)
        {
            try
            {
                var Entities = _iusersRepository.UsersGoals(UserId).ToList();
                var results = _mapper.Map<List<UsersGoalsDto>>(Entities);
                var _count = Entities.Count();
                var paginador = new GenericPage<UsersGoalsDto>();
                var result = paginador.Get(results, _count, page, count == 0 ? _pagination : count);
                return Task.FromResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al cargar  {0}, {@e}", e.Message, e);
                throw;
            }

        }
        public Task<Result<UsersDto>> UsersGoalsDetails(int userId, int GoalsId, int page = 1, int count = 0)
        {
            try
            {
                var Entities = _iusersRepository.UsersGoalsDetails(userId, GoalsId).ToList();
                var results = _mapper.Map<List<UsersDto>>(Entities);
                var _count = Entities.Count();
                var paginador = new GenericPage<UsersDto>();
                var result = paginador.Get(results, _count, page, count == 0 ? _pagination : count);
                return Task.FromResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al cargar  {0}, {@e}", e.Message, e);
                throw;
            }

        }

    }
}
