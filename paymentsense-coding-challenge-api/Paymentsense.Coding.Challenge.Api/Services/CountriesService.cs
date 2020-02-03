using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Paymentsense.Coding.Challenge.Api.Models;

namespace Paymentsense.Coding.Challenge.Api.Services
{
    public class CountriesService : ICountriesService
    {
        private IHttpClientFactory _clientFactory;

        public CountriesService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        }        

        public async Task<IEnumerable<Country>> GetCountries()
        {
            IEnumerable<Country> countries;

            var request = new HttpRequestMessage(HttpMethod.Get, "https://restcountries.eu/rest/v2/all");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync(); 
                countries = JsonConvert.DeserializeObject<IEnumerable<Country>>(responseContent);
            }
            else
            {
                countries = Array.Empty<Country>();
            }

            return countries;
        }
    }
}
