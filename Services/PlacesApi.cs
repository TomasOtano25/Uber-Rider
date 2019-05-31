using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Android.Locations;

namespace Services
{
    public class PlacesApi
    {
        private static readonly AutocompleteResultType DEFAULT_RESULT_TYPE = AutocompleteResultType.ADDRESS;

        private static readonly string PLACES_API_BASE = "https://maps.googleapis.com/maps/api/place";
        private static readonly string PATH_AUTOCOMPLETE = "autocomplete";
        

        private readonly HttpClient httpClient;
        private readonly string googleApiKey;

        private Location currentLocation;

        private long radiusM;
        private string languageCode;
        private bool locationBiasEnabled = true;


        public bool LocationBiasEnabled { get => locationBiasEnabled; set => locationBiasEnabled = value; }
        public long RadiusM { get => radiusM; set => radiusM = value; }
        public Location CurrentLocation { get => currentLocation; set => currentLocation = value; }
        public string LanguageCode { get => languageCode; set => languageCode = value; }

        public PlacesApi(HttpClient httpClient, string googleApiKey)
        {
            this.httpClient = httpClient;
            this.googleApiKey = googleApiKey;
        }

        public PlacesAutoCompleteResponse Autocomplete(string input, AutoCompleteResultType type)
        {
            string finalInput = input == null ? "" : input;
            AutocompleteResultType finalType = type == null ? "" : type;

            UriBuilder uriBuilder = new UriBuilder(PLACES_API_BASE);
                
                
        }







    }
}
