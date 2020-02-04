using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NSubstitute;
using Paymentsense.Coding.Challenge.Api.Models;
using Paymentsense.Coding.Challenge.Api.Models.Configuration;
using Paymentsense.Coding.Challenge.Api.Services;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Services
{
    public class CountriesServiceTests
    {
        private readonly IHttpClientFactory _mockClientFactory;
        private readonly IMemoryCache _cache;
        private readonly IOptions<CountriesApiOptions> _apiOptions;

        public CountriesServiceTests()
        {
            _mockClientFactory = Substitute.For<IHttpClientFactory>();
            var cacheOptions = new MemoryCacheOptions();
            _cache = new MemoryCache(cacheOptions);
            _apiOptions = Options.Create(new CountriesApiOptions 
            {
                BaseAddress = "https://restcountries.eu/rest/v2/",
                CountryNamePath =  "name/",
                AllCountriesPath = "all"
            });
        }

        [Fact]
        public async Task GetCountries_OnInvoke_ReturnsCountries_WhenOK()
        {
            var countries = new List<Country>
            {
                new Country
                {
                    Flag = "UnionJack",
                    Name = "United Kingdom"
                }
            };

            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(countries), Encoding.UTF8, "application/json")
            });
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);

            _mockClientFactory.CreateClient().Returns(fakeHttpClient);

            var service = new CountriesService(_mockClientFactory, _cache, _apiOptions);

            var result = await service.GetCountries();

            result.Should().BeEquivalentTo(countries);
        }

        [Fact]
        public async Task GetCountries_OnInvoke_ReturnsEmptyArray_WhenNotFound()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode =  HttpStatusCode.NotFound
            });
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);

            _mockClientFactory.CreateClient().Returns(fakeHttpClient);

            var service = new CountriesService(_mockClientFactory, _cache, _apiOptions);

            var result = await service.GetCountries();

            result.Should().BeEquivalentTo(Array.Empty<Country>());
        }

        [Fact]
        public async Task GetCountryByName_OnInvoke_ReturnsCountryDetails_WhenOK()
        {
            var country = new List<CountryDetails>
            {
                new CountryDetails
                {
                    Flag = "UnionJack",
                    Name = "United Kingdom"
                }
            };

            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(country), Encoding.UTF8, "application/json")
            });
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);

            _mockClientFactory.CreateClient().Returns(fakeHttpClient);

            var service = new CountriesService(_mockClientFactory, _cache, _apiOptions);

            var result = await service.GetCountryByName("United Kingdom");

            result.Should().BeEquivalentTo(country.First());
        }

        [Fact]
        public async Task GetCountryByName_OnInvoke_ReturnsNull_WhenNotFound()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NotFound
            });
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);

            _mockClientFactory.CreateClient().Returns(fakeHttpClient);

            var service = new CountriesService(_mockClientFactory, _cache, _apiOptions);

            var result = await service.GetCountryByName("United Kingdom");

            result.Should().BeNull();
        }
    }
}
