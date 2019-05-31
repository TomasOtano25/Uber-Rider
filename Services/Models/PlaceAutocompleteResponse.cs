namespace Services.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class PlaceAutoCompleteResponse : BasePlacesResponse // PlaceApiResponse
    {
        /// <summary>
        /// Contains an array of predictions, with information about the prediction
        /// </summary>
        [JsonProperty("predictions")]
        public virtual IEnumerable<Prediction> Predictions { get; set; }
    }
}
