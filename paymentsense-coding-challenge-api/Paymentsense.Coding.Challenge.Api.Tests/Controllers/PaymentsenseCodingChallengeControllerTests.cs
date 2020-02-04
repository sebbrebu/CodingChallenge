using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Paymentsense.Coding.Challenge.Api.Controllers;
using Paymentsense.Coding.Challenge.Api.Services;
using Paymentsense.Coding.Challenge.Api.Models;
using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Paymentsense.Coding.Challenge.Api.Tests.Controllers
{
    public class PaymentsenseCodingChallengeControllerTests
    {
        private readonly Mock<ICountriesService> _mockCountriesService;

        public PaymentsenseCodingChallengeControllerTests()
        {
            _mockCountriesService = new Mock<ICountriesService>();
        }

        [Fact]
        public async Task Get_OnInvoke_ReturnsOk_WhenExists()
        {
            var controller = new PaymentsenseCodingChallengeController(_mockCountriesService.Object);
            var countries = new List<Country>
            {
                new Country 
                { 
                    Flag = "UnionJack", 
                    Name = "United Kingdom" 
                }
            };

            _mockCountriesService.Setup(x => x.GetCountries()).ReturnsAsync(countries);

            var result = await controller.Get() as OkObjectResult;

            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().BeEquivalentTo(countries);
        }

        [Fact]
        public async Task Get_OnInvoke_ReturnsNotFound_WhenNotExists()
        {
            var controller = new PaymentsenseCodingChallengeController(_mockCountriesService.Object);

            _mockCountriesService.Setup(x => x.GetCountries()).ReturnsAsync(Array.Empty<Country>());

            var result = await controller.Get() as NotFoundObjectResult;

            result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            result.Value.Should().Be("Countries not found.");
        }

        [Fact]
        public async Task GetByName_OnInvoke_ReturnsOk_WhenExists()
        {
            var controller = new PaymentsenseCodingChallengeController(_mockCountriesService.Object);
            var country = new CountryDetails 
            { 
                Flag = "UnionJack", 
                Name = "United Kingdom"
            };

            _mockCountriesService.Setup(x => x.GetCountryByName(It.IsAny<string>())).ReturnsAsync(country);

            var result = await controller.GetByName("United Kingdom") as OkObjectResult;

            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().BeEquivalentTo(country);
        }

        [Fact]
        public async Task GetByName_OnInvoke_ReturnsNotFound_WhenNotExists()
        {
            var controller = new PaymentsenseCodingChallengeController(_mockCountriesService.Object);

            var result = await controller.GetByName("United Kingdom") as NotFoundObjectResult;

            result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            result.Value.Should().Be("Country details not found.");
        }
    }
}
