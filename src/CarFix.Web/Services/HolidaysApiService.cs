using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using CarFix.Web.Models;
using Microsoft.Extensions.Logging;

namespace CarFix.Web.Services
{
    public class HolidaysApiService : IHolidaysApiService
    {
        private readonly HttpClient _client;
        private readonly ILogger _logger;
        public HolidaysApiService(IHttpClientFactory clientFactory,ILogger<HolidaysApiService> logger)
        {
            _logger = logger;
            _logger.LogInformation("Initialize HolidaysApiService - Create HttpClient");
            _client = clientFactory.CreateClient("PublicHolidaysApi");
        }

        public async Task<List<HolidayModel>> GetHolidays(string countryCode, int year)
        {
            
            var url = string.Format("/api/v2/PublicHolidays/{0}/{1}", year, countryCode);
            _logger.LogInformation("GetHolidays calling external url: " + url);
            var result = new List<HolidayModel>();
            var response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("Response Received");
                _logger.LogTrace("GetHolidays: Deserialize to json length=" + stringResponse.Length);
                result = JsonSerializer.Deserialize<List<HolidayModel>>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            else
            {
                _logger.LogError("If you're seeing this, something bad happened");
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return result;
        }
    }
}
