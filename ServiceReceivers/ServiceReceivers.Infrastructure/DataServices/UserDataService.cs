using ServiceReceivers.Application.Interfaces;
using ServiceReceivers.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceReceivers.Infrastructure.DataService
{
    public class UserDataService : IUserDataService
    {
        private readonly List<User> users;

        public UserDataService()
        {
            users = new List<User>
            {
                new User{Id = new Guid("8611E98C-6627-4F5F-9D24-876BDD9E4DFC"), Name = "User1", Address = "User_Address_1", Pincode = 1234, MobileNumber = "9876564576", IsServiceActive = false},
                new User{Id = new Guid("506182E9-E389-4C99-90D3-C052FFD55D96"), Name = "User2", Address = "User_Address_2", Pincode = 2345, MobileNumber = "7656245342", IsServiceActive = false},
                new User{Id = new Guid("7DF73866-80DB-4B32-B30A-62060132915C"), Name = "User3", Address = "User_Address_3", Pincode = 4567, MobileNumber = "8765790382", IsServiceActive = false},
                new User{Id = new Guid("D55AA5D8-2F10-4BDB-87D0-CB7F276BDF90"), Name = "User4", Address = "User_Address_4", Pincode = 5678, MobileNumber = "9988226654", IsServiceActive = false},
                new User{Id = new Guid("2CCFBBD8-5BCB-4ED2-9645-8F9C2CE67161"), Name = "User5", Address = "User_Address_5", Pincode = 6789, MobileNumber = "8746093300", IsServiceActive = false}
            };
        }

        /// <inheritdoc/>
        public Guid AddNewUser(User user)
        {
            var userId = Guid.NewGuid();

            if(users.Exists(x => x.MobileNumber == user.MobileNumber))
            {
                throw new Exception("User already exist");
            }

            while(users.Exists(x => x.Id == userId))
            {
                userId = Guid.NewGuid();
            }
            user.Id = userId;
            users.Add(user);
            return user.Id;
        }

        /// <inheritdoc/>
        public User GetUserDetails(Guid userId)
        {
            return users.FirstOrDefault(user => user.Id == userId);
        }
    }
}
