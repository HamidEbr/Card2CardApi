using System;
using System.Linq;

namespace Domain.Base
{
    public static class RandomCodeGenerator
    {
        private static readonly Random random = new();

        public static string RandomString(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
