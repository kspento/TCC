using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using UserManagement.Data.Context;
using UserManagement.Data.Dto.PageAction;
using UserManagement.Data.Entities;
using UserManagement.Data.Repository.PageAction;
using UserManagement.Data.UnitOfWork;
using UserManagement.Domain.Model.PageAction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserManagement.Domain.Contracts.Services;
using UserManagement.Domain.Exception;

public class PageActionService : IPageActionService
{
    private readonly IPageActionRepository _pageActionRepository;
    private readonly IUnitOfWork<UserContext> _uow;
    private readonly IMapper _mapper;
    private readonly ILogger<GetPageActionModel> _logger;


    public PageActionService(
        IPageActionRepository pageActionRepository,
        IUnitOfWork<UserContext> uow,
        IMapper mapper,
        ILogger<GetPageActionModel> logger)
    {
        _pageActionRepository = pageActionRepository;
        _uow = uow;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PageActionDto> AddPageAction(AddPageActionModel request)
    {
        var entity = await _pageActionRepository.FindBy(c => c.PageId == request.PageId && c.ActionId == request.ActionId).FirstOrDefaultAsync();
        if (entity == null)
        {
            entity = _mapper.Map<PageAction>(request);
            entity.Id = Guid.NewGuid();
            _pageActionRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                throw new System.Exception();
            }
        }
        return _mapper.Map<PageActionDto>(entity);
    }

    public async Task DeletePageAction(DeletePageActionModel request)
    {
        var entityExist = await _pageActionRepository.FindBy(c => c.Id == request.Id).FirstOrDefaultAsync();
        if (entityExist == null)
        {
            throw new NotFoundException(string.Empty);
        }

        _pageActionRepository.Remove(entityExist);
        if (await _uow.SaveAsync() <= 0)
        {
            throw new System.Exception();
        }
    }

    public async Task<List<PageActionDto>> GetAllPageActions()
    {
        var entities = await _pageActionRepository.All.ToListAsync();

        return _mapper.Map<List<PageActionDto>>(entities);
    }

    public async Task<PageActionDto> GetPageAction(GetPageActionModel request)
    {
        var entity = await _pageActionRepository.FindAsync(request.Id);
        if (entity != null)
            return _mapper.Map<PageActionDto>(entity);
        else
        {
            // Log a warning since the page action was not found
            _logger.LogWarning("PageAction Not Found");
            throw new NotFoundException(string.Empty);
        }
    }
}
