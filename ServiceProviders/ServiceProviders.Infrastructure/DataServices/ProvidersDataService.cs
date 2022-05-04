using ServiceProviders.Application.Interfaces;
using ServiceProviders.Domain.Models;
using System;
using System.Collections.Generic;

namespace ServiceProviders.Infrastructure.DataService
{
    public class ProvidersDataService : IProvidersDataService
    {
        private readonly List<Provider> providers;

        public ProvidersDataService()
        {
            providers = new List<Provider>
            {
                new Provider{Id = new Guid("6005C213-407E-4957-833A-005CEDD59A67"),Name = "ServiceProvider_1", IsAvailable = true, ServiceId = new Guid("10279281-5128-433C-ACF6-30F732D4B39B"), Pincodes = new List<int> { 1234, 4567} },
                new Provider{Id = new Guid("9EB6931E-33AB-4C94-B832-53442B4C6651"),Name = "ServiceProvider_2", IsAvailable = true, ServiceId = new Guid("E8E7E0FF-E5FF-464E-82F8-25042871615F"), Pincodes = new List<int> { 2345, 5678} },
                new Provider{Id = new Guid("EF6F0447-62D4-44D5-8188-647307FC9A1F"),Name = "ServiceProvider_3", IsAvailable = true, ServiceId = new Guid("789BF4F7-151C-4709-8EFA-8763CEBFFB8E"), Pincodes = new List<int> { 1234, 3456, 6789} },
                new Provider{Id = new Guid("D1828ED2-01B8-4801-B10F-94DABBF37E45"),Name = "ServiceProvider_4", IsAvailable = true, ServiceId = new Guid("277D85F0-6510-464B-91D8-7757E99DF280"), Pincodes = new List<int> { 3456, 5678 } },
                new Provider{Id = new Guid("9A97305F-2864-4F76-A926-0556B61648E0"),Name = "ServiceProvider_5", IsAvailable = true, ServiceId = new Guid("9B9D4E8A-34B0-476E-980B-FD8656C27041"), Pincodes = new List<int> { 2345} }
            };
        }

        /// <inheritdoc/>
        public List<Provider> GetAll()
        {
            return providers;
        }
    }
}
