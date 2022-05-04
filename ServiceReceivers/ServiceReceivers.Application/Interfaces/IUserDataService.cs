using ServiceReceivers.Domain.Models;
using System;

namespace ServiceReceivers.Application.Interfaces
{
    public interface IUserDataService
    {
        /// <summary>
        /// Add new user into the existing users list
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Guid AddNewUser(User user);

        /// <summary>
        /// get user details as per the provided user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        User GetUserDetails(Guid userId);

    }
}
