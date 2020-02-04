using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Paymentsense.Coding.Challenge.Api.Models;
using Paymentsense.Coding.Challenge.Api.Models.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Paymentsense.Coding.Challenge.Api.Services
{
    public class CountriesService : ICountriesService
    {
        private const string CountriesCacheKey = "countries-cache-key";

        private readonly IHttpClientFactory _clientFactory;
        private readonly IMemoryCache _cache;
        private readonly CountriesApiOptions _apiOptions;

        public CountriesService(IHttpClientFactory clientFactory, IMemoryCache cache, IOptions<CountriesApiOptions> apiOptions)
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _apiOptions = apiOptions.Value;
        }        

        public async Task<IEnumerable<Country>> GetCountries()
        {
            if (_cache.TryGetValue(CountriesCacheKey, out IEnumerable<Country> ccountries))
            {
                return ccountries;
            }

            IEnumerable<Country> countries;

            var request = new HttpRequestMessage(HttpMethod.Get, $"{_apiOptions.BaseAddress}{_apiOptions.AllCountriesPath}");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync(); 
                countries = JsonConvert.DeserializeObject<IEnumerable<Country>>(responseContent);

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(1));
                _cache.Set(CountriesCacheKey, countries, cacheEntryOptions);
            }
            else
            {
                countries = Array.Empty<Country>();
            }

            return countries;
        }

        public async Task<CountryDetails> GetCountryByName(string name)
        {            
            if (_cache.TryGetValue(name, out CountryDetails ccountry))
            {
                return ccountry;
            }

            IEnumerable<CountryDetails> countryDetails;

            var request = new HttpRequestMessage(HttpMethod.Get, $"{_apiOptions.BaseAddress}{_apiOptions.CountryNamePath}{name}?fullText=true");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                countryDetails = JsonConvert.DeserializeObject<IEnumerable<CountryDetails>>(responseContent);

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(1));
                _cache.Set(name, countryDetails.FirstOrDefault(), cacheEntryOptions);
            }
            else
            {
                countryDetails = Array.Empty<CountryDetails>();
            }

            return countryDetails.FirstOrDefault();
        }
    }
}
