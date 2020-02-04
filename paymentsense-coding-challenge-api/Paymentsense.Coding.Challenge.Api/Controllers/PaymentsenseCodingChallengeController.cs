using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Paymentsense.Coding.Challenge.Api.Services;

namespace Paymentsense.Coding.Challenge.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentsenseCodingChallengeController : ControllerBase
    {
        private const string CountriesNotFoundMsg = "Countries not found.";
        private const string CountryDetailsNotFoundMsg = "Country details not found.";

        private readonly ICountriesService _countriesService;

        public PaymentsenseCodingChallengeController (ICountriesService countriesService)
        {
            _countriesService = countriesService ?? throw new ArgumentNullException(nameof(countriesService));
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var countries = await _countriesService.GetCountries();

            if (countries.Any())
            {
                return Ok(countries);
            }
            else
            {
                return NotFound(CountriesNotFoundMsg);
            }
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<ActionResult> GetByName(string name)
        {
            var countryDetails = await _countriesService.GetCountryByName(name);            

            if (countryDetails != null)
            {
                return Ok(countryDetails);
            }
            else
            {
                return NotFound(CountryDetailsNotFoundMsg);
            }
        }
    }
}
