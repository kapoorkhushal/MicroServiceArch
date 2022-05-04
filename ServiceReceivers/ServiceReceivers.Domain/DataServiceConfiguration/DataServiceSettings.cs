using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;

namespace ServiceReceivers.Domain.DataServiceConfiguration
{
    public class DataServiceSettings
    {
        public Uri BaseUrl { get; set; }
        public Dictionary<string, EndPoint> EndPoints { get; } = new Dictionary<string, EndPoint>();

        public (HttpMethod RequestMethod, Uri RequestUri) GetEndPoint(string key)
        {
            if(!EndPoints.TryGetValue(key, out EndPoint endPoint))
            {
                throw new KeyNotFoundException();
            }

            return (new HttpMethod(endPoint.Verb.ToUpperInvariant()), new Uri(endPoint.Path.Trim(), UriKind.Relative));
        }

        public (HttpMethod RequestMethod, Uri RequestUri) GetEndPoint(string key, params string[] urlArgs)
        {
            if (!EndPoints.TryGetValue(key, out var endPoint))
            {
                throw new KeyNotFoundException();
            }

            return (new HttpMethod(endPoint.Verb.ToUpperInvariant()), new Uri(string.Format(CultureInfo.InvariantCulture, endPoint.Path, urlArgs).Trim(), UriKind.Relative));
        }

        public class EndPoint
        {
            public string Path { get; set; }
            public string Verb { get; set; }

        }

    }
}
