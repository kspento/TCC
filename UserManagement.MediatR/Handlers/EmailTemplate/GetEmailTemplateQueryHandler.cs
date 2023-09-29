using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Dto;
using UserManagement.Helper;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;

namespace UserManagement.MediatR.Handlers
{
    public class GetEmailTemplateQueryHandler : IRequestHandler<GetEmailTemplateQuery, ServiceResponse<EmailTemplateDto>>
    {
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetEmailTemplateQueryHandler> _logger;

        public GetEmailTemplateQueryHandler(
           IEmailTemplateRepository emailTemplateRepository,
            IMapper mapper,
            ILogger<GetEmailTemplateQueryHandler> logger
            )
        {
            _emailTemplateRepository = emailTemplateRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceResponse<EmailTemplateDto>> Handle(GetEmailTemplateQuery request, CancellationToken cancellationToken)
        {
            var emailTemplate = await _emailTemplateRepository.FindBy(c => c.Id == request.Id).FirstOrDefaultAsync();
            if (emailTemplate == null)
            {
                _logger.LogError("Email Template is not available");
                return ServiceResponse<EmailTemplateDto>.Return404();
            }
            return ServiceResponse<EmailTemplateDto>.ReturnResultWith200(_mapper.Map<EmailTemplateDto>(emailTemplate));
        }
    }
}
