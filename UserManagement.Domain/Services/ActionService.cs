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
using UserManagement.Domain.Exception;

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

        public async Task<ActionDto> AddAction(ActionModel request)
        {
            var entityExist = await _actionRepository.FindBy(c => c.Name.Trim().ToLower() == request.Name.Trim().ToLower()).FirstOrDefaultAsync();
            if (entityExist != null)
            {
                _logger.LogError("Action already exist.");
                throw new AlreadyExistsException("Action already exist.");
            }
            var entity = _mapper.Map<Data.Entities.Action>(request);
            entity.Id = Guid.NewGuid();
            _actionRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                throw new System.Exception();
            }
            var entityDto = _mapper.Map<ActionDto>(entity);
            return entityDto;
        }


        public async Task<ActionDto> UpdateAction(ActionModel request)
        {
            var entityExist = await _actionRepository.FindBy(c => c.Name == request.Name && c.Id != request.Id)
                .FirstOrDefaultAsync();
            if (entityExist != null)
            {
                _logger.LogError("Action Name Already Exist.");
                throw new AlreadyExistsException("Action Name Already Exist.");
            }
            entityExist = await _actionRepository.FindBy(v => v.Id == request.Id).FirstOrDefaultAsync();
            entityExist.Name = request.Name;
            _actionRepository.Update(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                throw new System.Exception();
            }
            var entityDto = _mapper.Map<ActionDto>(entityExist);

            return entityDto;
        }

        public async Task DeleteAction(ActionModel request)
        {
            var entityExist = await _actionRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                _logger.LogError("Not found");
                throw new NotFoundException("Not found");
            }

            _actionRepository.Delete(request.Id);
            if (await _uow.SaveAsync() <= 0)
            {
                throw new System.Exception();
            }
        }

        public async Task<ActionDto> GetAction(ActionModel request)
        {
            var entity = await _actionRepository.FindAsync(request.Id);
            if (entity != null)
                return _mapper.Map<ActionDto>(entity);            
            else
                throw new NotFoundException(string.Empty);
        }

        public async Task<List<ActionDto>> GetAllAction()
        {

            var result = new List<ActionDto>(); 
            var entities = await _actionRepository.All.ToListAsync();

            foreach (var item in entities)
            {
                var mapped = _mapper.Map<ActionDto>(item);
                result.Add(mapped);
            }

            return result;            
        }

    }
}
