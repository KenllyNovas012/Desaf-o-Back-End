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
using SystemQuickzal.Data.Entity;

namespace System.Infrastructure.Services
{
    public class CompositioncategoryServices
    {

        private readonly ICompositioncategoryRepository _compositioncategoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CompositioncategoryServices> _logger;
        private int _pagination;
        public CompositioncategoryServices(IConfiguration config, IMapper mapper, ICompositioncategoryRepository compositioncategoryRepository, ILogger<CompositioncategoryServices> logger)
        {
            _compositioncategoryRepository = compositioncategoryRepository;
            _mapper = mapper;
            _logger = logger;
            _pagination = Convert.ToInt32(config["Paginator"]);
        }


        public Task<Result<CompositioncategoryDto>> Get(string Name, int page = 1, int count = 0)
        {
            try
            {
                var Entities = _compositioncategoryRepository.GetAll().ToList();

                if (Name != null)
                {
                    Entities.Where(x => x.Name == Name);
                }

                var results = _mapper.Map<List<CompositioncategoryDto>>(Entities);
                var _count = Entities.Count();
                var paginador = new GenericPage<CompositioncategoryDto>();
                var result = paginador.Get(results, _count, page, count == 0 ? _pagination : count);
                return Task.FromResult(result);

            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al cargar  {0}, {@e}", e.Message, e);
                throw;
            }

        }

        public async Task<ResultData<CompositioncategoryDto>> GetByIdAsync(int id)
        {
            try
            {
                var response = await _compositioncategoryRepository.GetByIdAsync(id);
                if (response == null)
                    return new ResultData<CompositioncategoryDto> { Success = false, Message = "Date no exist" };

                var model = _mapper.Map<Compositioncategory, CompositioncategoryDto>(response);
                return new ResultData<CompositioncategoryDto> { Success = true, Data = model };
            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al cargar  {0}, {@e}", e.Message, e);
                throw;
            }

        }

        public async Task<Response> Add(CompositioncategoryDto model)
        {
            try
            {
                var response = _mapper.Map<Compositioncategory>(model);
                await _compositioncategoryRepository.CreateAsync(response);
                var Compositioncategory = _mapper.Map<CompositioncategoryDto>(response);
                return new Response { IsSuccess = true, Result = Compositioncategory };
            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al guardad {0}, {@e}", e.Message, e);
                return new Response { IsSuccess = false };
            }
        }

        public async Task<Response> Update(CompositioncategoryDto model)
        {
            try
            {
                var response = await _compositioncategoryRepository.GetByIdAsync(model.Id);
                if (response == null)
                {
                    return new Response { IsSuccess = false, Message = "No se encontro el id" };
                }

                await _compositioncategoryRepository.UpdateAsync(response);
                var result = _mapper.Map<CompositioncategoryDto>(response);
                return new Response { IsSuccess = true, Result = result };
            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al guardad {0}, {@e}", e.Message, e);
                return new Response { IsSuccess = false };
            }
        }

        public async Task<Response> Delete(int id)
        {
            try
            {
                var entity = await _compositioncategoryRepository.GetByIdAsync(id);
                if (entity == null)
                    return new Response { IsSuccess = false, Message = "categoria no encontrada" };

                await _compositioncategoryRepository.DeleteAsync(entity);

                return new Response { IsSuccess = true, Message = "Operacion realizada exitosamente." };
            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al guardad {0}, {@e}", e.Message, e);
                return new Response { IsSuccess = false };
            }
        }



    }
}
