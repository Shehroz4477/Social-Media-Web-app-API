using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace serverSite.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateOnly dob)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var age = today.Year - dob.Year;
            if(dob > today.AddYears(-age)) return age--;
            return age;
        }
    }
}