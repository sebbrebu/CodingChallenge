using System.Collections.Generic;

namespace Paymentsense.Coding.Challenge.Api.Models
{
    public class CountryDetails : Country
    {
        public int? Population { get; set; }

        public IEnumerable<string> Timezones { get; set; }

        public IEnumerable<Currency> Currencies { get; set; }

        public IEnumerable<Language> Languages { get; set; }

        public string Capital { get; set; }

        public IEnumerable<string> Borders { get; set; }
    }
}
