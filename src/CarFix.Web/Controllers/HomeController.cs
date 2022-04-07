using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using CarFix.Web.Models;
using CarFix.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarFix.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHolidaysApiService _holidaysApiService;
        private readonly ILogger _logger;

        public HomeController(IHolidaysApiService holidaysApiService, ILogger<HomeController> logger)
        {
            _logger = logger;
            _holidaysApiService = holidaysApiService;
        }
         
        public async Task<IActionResult> Index(string countryCode, int year)
        {
            _logger.LogInformation("Calling Index action method in HomeController");
            List<HolidayModel> holidays = new List<HolidayModel>();
            try
            {
                if (!string.IsNullOrEmpty(countryCode) && year > 0)
                {
                    if (countryCode.ToUpper() == "BE")
                    {
                        _logger.LogTrace("Requested Holidays for CountryCode: BE");
                        if (year < 2022)
                        {

                            throw new ArgumentException("Error: Invalid data before year 2022 for Belgium");
                        }
                        else
                        {

                            _logger.LogWarning("Warning: Latency expected for data coming from Belgium"); // Writes a warning message  at log level 3

                            Thread.Sleep(3000);
                        }
                    }
                    holidays = await _holidaysApiService.GetHolidays(countryCode, year);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message); // Writes a critical message  at log level 5
            }
            return View(holidays);
        }
    }
}
