using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarFix.Web.Models;

namespace CarFix.Web.Services
{
    public interface IHolidaysApiService
    {
        Task<List<HolidayModel>> GetHolidays(string countryCode, int year);
    }
}
