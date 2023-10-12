using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using UserManagement.Data.Context;
using UserManagement.Data.Dto.Action;
using UserManagement.Data.UnitOfWork;
using UserManagement.Domain.Model.Action;
using UserManagement.Helper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UserManagement.Domain.Contracts.Services;
using UserManagement.Data.Repository.Contracts;

namespace UserManagement.Domain.Services
{
    public class ActionService : IActionService
    {
        private readonly IActionRepository _actionRepository;
        private readonly IUnitOfWork<UserContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<ActionModel> _logger;
        public ActionService(IActionRepository actionRepository,
            IMapper mapper,
            IUnitOfWork<UserContext> uow,
            ILogger<ActionModel> logger)
        {
            _actionRepository = actionRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }

        public async Task<ActionDto> AddAction(ActionModel request, CancellationToken cancellationToken)
        {
            var entityExist = await _actionRepository.FindBy(c => c.Name.Trim().ToLower() == request.Name.Trim().ToLower()).FirstOrDefaultAsync();
            if (entityExist != null)
            {
                _logger.LogError("Action already exist.");
                //return ServiceResponse<ActionDto>.Return409("Action already exist.");
            }
            var entity = _mapper.Map<Data.Entities.Action>(request);
            entity.Id = Guid.NewGuid();
            _actionRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                //return ServiceResponse<ActionDto>.Return500();
            }
            var entityDto = _mapper.Map<ActionDto>(entity);
            return entityDto;
            //return ServiceResponse<ActionDto>.ReturnResultWith200(entityDto);
        }


        public async Task<ActionDto> UpdateAction(ActionModel request, CancellationToken cancellationToken)
        {
            var entityExist = await _actionRepository.FindBy(c => c.Name == request.Name && c.Id != request.Id)
                .FirstOrDefaultAsync();
            if (entityExist != null)
            {
                _logger.LogError("Action Name Already Exist.");
                //return ServiceResponse<ActionDto>.Return409("Action Name Already Exist.");
            }
            entityExist = await _actionRepository.FindBy(v => v.Id == request.Id).FirstOrDefaultAsync();
            entityExist.Name = request.Name;
            _actionRepository.Update(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                //return ServiceResponse<ActionDto>.Return500();
            }
            var entityDto = _mapper.Map<ActionDto>(entityExist);

            return entityDto;
            //return ServiceResponse<ActionDto>.ReturnSuccess();
        }

        public async Task DeleteAction(ActionModel request, CancellationToken cancellationToken)
        {
            var entityExist = await _actionRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                _logger.LogError("Not found");
                //return null;
                //return ServiceResponse<ActionDto>.Return404();
            }

            _actionRepository.Delete(request.Id);
            if (await _uow.SaveAsync() <= 0)
            {
                //return ServiceResponse<ActionDto>.Return500();
            }

            //return ServiceResponse<ActionDto>.ReturnSuccess();
        }

        public async Task<ActionDto> GetAction(ActionModel request, CancellationToken cancellationToken)
        {
            var entity = await _actionRepository.FindAsync(request.Id);
            if (entity != null)
                return _mapper.Map<ActionDto>(entity);
            //return ServiceResponse<ActionDto>.ReturnResultWith200(_mapper.Map<ActionDto>(entity));
            else
                return null;
            /*return ServiceResponse<ActionDto>.Return404()*/
            ;
        }

        public async Task<List<ActionDto>> GetAllAction(ActionModel request, CancellationToken cancellationToken)
        {
            var entities = await _actionRepository.All.ToListAsync();

            return _mapper.Map<List<ActionDto>>(entities);
            //return ServiceResponse<List<ActionDto>>.ReturnResultWith200(_mapper.Map<List<ActionDto>>(entities));
        }

    }
}
