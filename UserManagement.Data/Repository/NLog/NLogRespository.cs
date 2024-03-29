﻿using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Data.Context;
using UserManagement.Data.Dto.NLog;
using UserManagement.Data.Entities;
using UserManagement.Data.GenericRespository;
using UserManagement.Data.PropertyMapping;
using UserManagement.Data.Repository.Contracts;
using UserManagement.Data.Resources;
using UserManagement.Data.UnitOfWork;

namespace UserManagement.Data.Repository.NLog
{
    public class NLogRespository : GenericRepository<Entities.NLog, UserContext>,
          INLogRespository
    {
        private readonly IPropertyMappingService _propertyMappingService;
        public NLogRespository(IUnitOfWork<UserContext> uow,
            IPropertyMappingService propertyMappingService) : base(uow)
        {
            _propertyMappingService = propertyMappingService;
        }

        public async Task<NLogList> GetNLogsAsync(NLogResource nLogResource)
        {
            var allNlogResource = All;

            var collectionBeforePaging = allNlogResource.OrderBySerializedString(nLogResource.OrderBy);
            //collectionBeforePaging =
            //   collectionBeforePaging.ApplySort(nLogResource.OrderBy,
            //   _propertyMappingService.GetPropertyMapping<NLogDto, Entities.NLog>());

            if (!string.IsNullOrWhiteSpace(nLogResource.Message))
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(c => EF.Functions.Like(c.Message, $"%{nLogResource.Message.Trim()}%"));
            }

            if (!string.IsNullOrWhiteSpace(nLogResource.Level))
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(c => c.Level == nLogResource.Level);
            }

            if (!string.IsNullOrWhiteSpace(nLogResource.Source))
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(c => c.Source == nLogResource.Source);
            }

            var nLogList = new NLogList();
            return await nLogList.Create(
                collectionBeforePaging,
                nLogResource.Skip,
                nLogResource.PageSize
                );
        }
    }
}
