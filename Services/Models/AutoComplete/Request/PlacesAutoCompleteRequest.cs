namespace Services.Models.AutoComplete.Request
{
    using System;
    using System.Linq;
    using System.Globalization;
    using System.Collections.Generic;
    using Enums;
    using Common;
    using Common.Enums;
    using Common.Enums.Extensions;
    using Common.Extensions;
    

    public class PlacesAutoCompleteRequest: BasePlaceRequest
    {
        protected internal override string BaseUrl => base.BaseUrl + "autocomplete/json";

        public virtual string Input { get; set; }

        public virtual string Offset { get; set; }

        public virtual string SessionToken { get; set; }

        public virtual Location Location { get; set; }

        public virtual double? Radius { get; set; }

        public virtual bool Strictbounds { get; set; }

        public virtual Language Language { get; set; } = Language.English;

        public virtual IEnumerable<RestrictPlaceType> Types { get; set; }

        public virtual IEnumerable<KeyValuePair<Component, string>> Components { get; set; }

        public override IList<KeyValuePair<string, string>> GetQueryStringParameters()
        {
            var parameters = base.GetQueryStringParameters();

            if (string.IsNullOrEmpty(this.Input))
                throw new ArgumentException("Input is required");

            if (this.Radius.HasValue && (this.Radius > 50000 || this.Radius < 1))
                throw new ArgumentException("Radius must be greater than or equal to 1 and less than or equal to 50.000");

            parameters.Add("input", this.Input);
            parameters.Add("language", this.Language.ToCode());

            if (!string.IsNullOrEmpty(this.Offset))
                parameters.Add("offset", this.Offset);

            if (!string.IsNullOrEmpty(this.SessionToken))
                parameters.Add("sessiontoken", this.SessionToken);

            if (this.Location != null)
                parameters.Add("location", this.Location.ToString());

            if (this.Radius.HasValue)
                parameters.Add("radius", this.Radius.Value.ToString(CultureInfo.InvariantCulture));

            if (this.Strictbounds)
                parameters.Add("strictbounds");

            if (this.Types != null && this.Types.Any())
            {
                parameters.Add("types", string.Join("|", this.Types.Select(x =>
                {
                    if (x == RestrictPlaceType.Cities || x == RestrictPlaceType.Regions)
                        return $"({x.ToString().ToLower()})";

                    return $"{x.ToString().ToLower()}";
                })));
            }

            if (this.Components != null && this.Components.Any())
                parameters.Add("components", string.Join("|", this.Components.Select(x => $"{x.Key.ToString().ToLower()}:{x.Value}")));
            

            return parameters;
        }
    }
}
