using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paymentsense.Coding.Challenge.Api.Models;

namespace Paymentsense.Coding.Challenge.Api.Services
{
    public interface ICountriesService
    {
        Task<IEnumerable<Country>> GetCountries();
    }
}
