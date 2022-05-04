using MediatR;
using ServiceReceivers.Application.Interfaces;
using ServiceReceivers.Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceReceivers.Application.Features
{
    public class CreateUserService : IRequest<Guid>
    {
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public int Pincode { get; set; }

        public class CreateUserHandler : IRequestHandler<CreateUserService, Guid>
        {
            private readonly IUserDataService _services;
            public CreateUserHandler(IUserDataService context)
            {
                _services = context;
            }
            public async Task<Guid> Handle(CreateUserService user, CancellationToken cancellationToken)
            {
                var newUser = new User();

                newUser.Name = user.Name;
                newUser.MobileNumber = user.MobileNumber;
                newUser.Address = user.Address;
                newUser.Pincode = user.Pincode;

                return _services.AddNewUser(newUser);
            }
        }
    }
}
