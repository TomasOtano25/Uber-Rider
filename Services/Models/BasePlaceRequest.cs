namespace Services.Models
{
    using System;
    using System.Collections.Generic;

    public abstract class BasePlaceRequest: BaseRequest, IRequestQueryString
    {
        protected internal override string BaseUrl => "maps.googleapis.com/maps/api/place/";

        public override bool IsSsl
        {
            get => true;
            set => throw new NotSupportedException("This operation is not supported, Request must use SSL");
        }

        public override IList<KeyValuePair<string, string>> GetQueryStringParameters()
        {
            if (string.IsNullOrEmpty(this.Key))
                throw new ArgumentException("Key is required");

            var parameters = base.GetQueryStringParameters();

            return parameters;
        }
    }
}