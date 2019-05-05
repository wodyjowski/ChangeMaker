using ChangeMaker.Logic.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeMaker.Logic
{
    class DynamicAlgorithm : Algorithm
    {
        private int numOfCoins;
        private int[] coinArray;
        public DynamicAlgorithm(IEnumerable<int> coins) : base(coins)
        {
            numOfCoins = coins.Count();
            coinArray = coins.ToArray();
        }

        public Dictionary<int, int> CalculateResult(float x)
        {
            int amount = (int) x;

            Dictionary<int, int> result = new Dictionary<int, int>();

            int[] o = new int[amount + 1];
            int[] s = new int[amount + 1];

            int opt;

            o[0] = s[0] = 0;

            for (int i = 1; i <= amount; i++)
            {
                opt = 0;

                for (int j = 1; j < numOfCoins; j++)
                {
                    if((coinArray[j] <= i) && (o[i - coinArray[j]] <= o[i - coinArray[opt]]))
                    {
                        opt = j;
                    }
                }

                o[i] = (o[i - coinArray[opt]]) + 1;
                s[i] = opt;

            }


            while(amount > 0)
            {
                var coin = coinArray[s[amount]];
                amount -= coin;

                if (result.ContainsKey(coin))
                {
                    result[coin]++;
                }
                else
                {
                    result[coin] = 1;
                }
            }


            return result;
        }
    }
}
