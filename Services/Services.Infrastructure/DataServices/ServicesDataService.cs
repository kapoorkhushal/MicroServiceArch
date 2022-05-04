using Services.Application.Interfaces;
using Services.Domain;
using System;
using System.Collections.Generic;

namespace Services.Infrastructure.DataServices
{
    public class ServicesDataService : IServicesDataService
    {
        private readonly List<Service> services;

        public ServicesDataService()
        {
            services = new List<Service>
            {
                new Service{ Id = new Guid("10279281-5128-433C-ACF6-30F732D4B39B"), Name = "Electrician", Amount = 500},
                new Service{ Id = new Guid("E8E7E0FF-E5FF-464E-82F8-25042871615F"), Name = "Carpenter", Amount = 500},
                new Service{ Id = new Guid("789BF4F7-151C-4709-8EFA-8763CEBFFB8E"), Name = "Beauty", Amount = 500},
                new Service{ Id = new Guid("277D85F0-6510-464B-91D8-7757E99DF280"), Name = "Plumber", Amount = 500},
                new Service{ Id = new Guid("9B9D4E8A-34B0-476E-980B-FD8656C27041"), Name = "Painter", Amount = 500}
            };
        }

        /// <inheritdoc/>
        public List<Service> GetAll()
        {
            return services;
        }

    }
}
