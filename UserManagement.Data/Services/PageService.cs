using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserManagement.Data.Context;
using UserManagement.Data.Dto.Page;
using UserManagement.Data.Entities;
using UserManagement.Data.Repository.Contracts;
using UserManagement.Data.UnitOfWork;
using UserManagement.Domain.Contracts.Services;
using UserManagement.Domain.Model.Page;
using UserManagement.Helper;

public class PageService : IPageService
{
    private readonly IPageRepository _pageRepository;
    private readonly IUnitOfWork<UserContext> _uow;
    private readonly IMapper _mapper;
    private readonly ILogger<PageService> _logger;

    public PageService(
        IPageRepository pageRepository,
        IUnitOfWork<UserContext> uow,
        IMapper mapper,
        ILogger<PageService> logger)
    {
        _pageRepository = pageRepository;
        _uow = uow;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ServiceResponse<PageDto>> AddPage(AddPageModel request)
    {
        var existingEntity = await _pageRepository.FindBy(c => c.Name == request.Name).FirstOrDefaultAsync();
        if (existingEntity != null)
        {
            _logger.LogError("Page Already Exists");
            return ServiceResponse<PageDto>.Return409("Page Already Exists.");
        }

        var entity = _mapper.Map<Page>(request);
        _pageRepository.Add(entity);

        if (await _uow.SaveAsync() <= 0)
        {
            _logger.LogError("Save Page has Error");
            return ServiceResponse<PageDto>.Return500();
        }

        return ServiceResponse<PageDto>.ReturnResultWith200(_mapper.Map<PageDto>(entity));
    }

    public async Task<ServiceResponse<PageDto>> UpdatePage(UpdatePageModel request)
    {
        var entityExist = await _pageRepository.FindBy(c => c.Name == request.Name && c.Id != request.Id)
            .FirstOrDefaultAsync();

        if (entityExist != null)
        {
            _logger.LogError("Page Name Already Exists.");
            return ServiceResponse<PageDto>.Return409("Page Name Already Exists.");
        }

        entityExist = await _pageRepository.FindBy(v => v.Id == request.Id).FirstOrDefaultAsync();
        entityExist.Name = request.Name;
        entityExist.Url = request.Url;

        _pageRepository.Update(entityExist);

        if (await _uow.SaveAsync() <= 0)
        {
            return ServiceResponse<PageDto>.Return500();
        }

        return ServiceResponse<PageDto>.ReturnResultWith200(_mapper.Map<PageDto>(entityExist));
    }

    public async Task<ServiceResponse<PageDto>> DeletePage(DeletePageModel request)
    {
        var entityExist = await _pageRepository.FindAsync(request.Id);
        if (entityExist == null)
        {
            return ServiceResponse<PageDto>.Return404();
        }

        _pageRepository.Delete(request.Id);
        if (await _uow.SaveAsync() <= 0)
        {
            return ServiceResponse<PageDto>.Return500();
        }

        return ServiceResponse<PageDto>.ReturnSuccess();
    }

    public async Task<ServiceResponse<PageDto>> GetPage(GetPageModel request)
    {
        var entity = await _pageRepository.FindAsync(request.Id);
        if (entity != null)
            return ServiceResponse<PageDto>.ReturnResultWith200(_mapper.Map<PageDto>(entity));
        else
        {
            _logger.LogError("Not found");
            return ServiceResponse<PageDto>.Return404();
        }
    }

    public async Task<List<PageDto>> GetAllPages()
    {
        var entities = await _pageRepository.All.ToListAsync();
        return _mapper.Map<List<PageDto>>(entities);
    }
}
