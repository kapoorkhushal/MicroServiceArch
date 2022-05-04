using System.Collections.Generic;

namespace ServiceReceivers.Domain.DataServiceConfiguration
{
    public class DataServices
    {
        public Dictionary<string, DataServiceSettings> ServiceSettings { get; } = new Dictionary<string, DataServiceSettings>();

        public DataServiceSettings GetDataServiceSettings(string key)
        {
            if (!ServiceSettings.TryGetValue(key, out DataServiceSettings dataServiceSettings))
            {
                throw new KeyNotFoundException(key);
            }

            return dataServiceSettings;
        }
    }
}
