using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChangeMaker.Helpers
{
    public static class CoinParser
    {
        /// <summary>
        /// Searches specific text and returns enumerable of all integer numbers seperated by any character different than digit
        /// </summary>
        /// <param name="text"></param>
        /// <returns>Enumerable of found integers</returns>
        public static IEnumerable<int> ParseCoins(this string text)
        {
            List<int> result = new List<int>();

            if (string.IsNullOrWhiteSpace(text)) return result;
            text = text.Trim();

            Regex reg = new Regex(@"\d+");
            var matches = reg.Matches(text);

            foreach (Match match in matches)
            {
                int res;
                if(int.TryParse(match.Value, out res))
                {
                    result.Add(res);
                }
            }

            return result.Where(d => d != 0);
        }

        /// <summary>
        /// Parses specific text and returns single float. Returns null if float cannot be parsed.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static float? ParseSingeValue(this string text)
        {
            float result;
            if(!float.TryParse(text, out result))
            {
                return null;
            }

            return result;
        }

    }
}
