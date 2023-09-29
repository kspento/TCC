﻿using System.Collections.Generic;
using UserManagement.Data.Dto;
using UserManagement.Data.Dto.User;

namespace UserManagement.Repository
{
    public interface IConnectionMappingRepository
    {
        bool AddUpdate(SignlarUser tempUserInfo, string connectionId);
        void Remove(SignlarUser tempUserInfo);
        IEnumerable<SignlarUser> GetAllUsersExceptThis(SignlarUser tempUserInfo);
        SignlarUser GetUserInfo(SignlarUser tempUserInfo);
        SignlarUser GetUserInfoByName(string id);
        SignlarUser GetUserInfoByConnectionId(string connectionId);

    }
}