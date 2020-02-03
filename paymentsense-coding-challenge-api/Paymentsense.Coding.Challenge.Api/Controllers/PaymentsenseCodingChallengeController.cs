using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Paymentsense.Coding.Challenge.Api.Services;

namespace Paymentsense.Coding.Challenge.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentsenseCodingChallengeController : ControllerBase
    {
        private readonly ICountriesService _countriesService;

        public PaymentsenseCodingChallengeController (ICountriesService countriesService)
        {
            _countriesService = countriesService ?? throw new ArgumentNullException(nameof(countriesService));
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            var dummy = await _countriesService.GetCountries();

            return Ok(dummy);
        }
    }
}
