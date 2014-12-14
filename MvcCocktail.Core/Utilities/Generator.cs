using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcCocktail.Core.Utilities
{
    public static class Generator
    {
        /// <exclude />
        private static readonly Random Random = new Random();

        /// <summary>
        /// Generate passwords
        /// </summary>
        /// <param name="passwordLength"></param>
        /// <param name="strongPassword"> </param>
        /// <returns></returns>
        public static string RandomPassword(int passwordLength, bool strongPassword = false)
        {
            int seed = Random.Next(1, int.MaxValue);
            const string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            const string specialCharacters = @"!#$%&'()*+,-./:;<=>?@[\]_";

            var chars = new char[passwordLength];
            var rd = new Random(seed);

            for (var i = 0; i < passwordLength; i++)
            {
                if (strongPassword && i % Random.Next(3, passwordLength) == 0)
                {
                    chars[i] = specialCharacters[rd.Next(0, specialCharacters.Length)];
                }
                else
                {
                    chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
                }
            }

            return new string(chars);
        }
    }
}
