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

            return result;
        }
    }
}
