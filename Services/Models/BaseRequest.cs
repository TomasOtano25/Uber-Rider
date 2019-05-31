namespace Services.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common.Extensions;

    public abstract class BaseRequest: IRequest
    {
        protected internal abstract string BaseUrl { get; }

        protected internal virtual string KeyName { get; set; } = "key";

        [JsonIgnore]
        public virtual string Key { get; set; }

        [JsonIgnore]
        public virtual string ClientId { get; set; }

        [JsonIgnore]
        public virtual bool IsSsl { get; set; } = true;

        public virtual Uri GetUri()
        {
            var schema = this.IsSsl ? "https://" : "http://";
            var queryString = string.Join("&", this.GetQueryStringParameters().Select(x => 
                x.Value == null
                    ? Uri.EscapeDataString(x.Key)
                    : Uri.EscapeDataString(x.Key) + "=" + Uri.EscapeDataString(x.Value)));
            var uri = new Uri(schema + this.BaseUrl + "?" + queryString);

            return uri;
        }

        public virtual IList<KeyValuePair<string, string>> GetQueryStringParameters()
        {
            var parameters = new List<KeyValuePair<string, string>>();

            if (this.ClientId == null)
            {
                if (!string.IsNullOrWhiteSpace(this.Key))
                    parameters.Add(this.KeyName, this.Key);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(this.Key))
                    throw new ArgumentException("Key is required");

                if (!this.ClientId.StartsWith("gme-"))
                    throw new ArgumentException("ClientId must begin with 'gme-'");
            }

            return parameters;
        }
    }
}