using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Dto;
using UserManagement.Helper;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;

namespace UserManagement.MediatR.Handlers
{
    public class GetAllEmailTemplateQueryHandler : IRequestHandler<GetAllEmailTemplateQuery, ServiceResponse<List<EmailTemplateDto>>>
    {
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        private readonly IMapper _mapper;
        public GetAllEmailTemplateQueryHandler(
           IEmailTemplateRepository emailTemplateRepository,
            IMapper mapper

            )
        {
            _emailTemplateRepository = emailTemplateRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<EmailTemplateDto>>> Handle(GetAllEmailTemplateQuery request, CancellationToken cancellationToken)
        {
            var entities = await _emailTemplateRepository.All.ToListAsync();
            return ServiceResponse<List<EmailTemplateDto>>.ReturnResultWith200(_mapper.Map<List<EmailTemplateDto>>(entities));
        }
    }
}
