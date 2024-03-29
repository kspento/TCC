﻿using UserManagement.Data.Context;
using UserManagement.Data.GenericRespository;
using UserManagement.Data.Repository.Contracts;
using UserManagement.Data.UnitOfWork;

namespace UserManagement.Data.Repository.Action
{
    public class ActionRepository : GenericRepository<Entities.Action, UserContext>,
          IActionRepository
    {
        public ActionRepository(
            IUnitOfWork<UserContext> uow
            ) : base(uow)
        {
        }
    }
}
