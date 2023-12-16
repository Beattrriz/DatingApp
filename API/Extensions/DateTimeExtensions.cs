using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalcularIdade(this DateOnly dob)
        {
            var hoje = DateOnly.FromDateTime(DateTime.UtcNow);

            var idade = hoje.Year - dob.Year;

            if (dob > hoje.AddYears(-idade)) idade--;

            return idade;
        }
    }
}