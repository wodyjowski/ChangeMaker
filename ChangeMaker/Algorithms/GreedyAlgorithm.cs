using ChangeMaker.Logic.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeMaker.Logic
{
    class GreedyAlgorithm : Algorithm
    {
        private int numOfCoins;
        private int[] coinArray;

        public GreedyAlgorithm(IEnumerable<int> coins) : base(coins)
        {
            numOfCoins = coins.Count();
            coinArray = coins.ToArray();
        }

        public Dictionary<int, int> CalculateResult(float amount)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();

            int n = numOfCoins - 1;

            while (n >= 0)
            {
                if(amount < coinArray[n])
                {
                    --n;
                }
                else
                {
                    amount -= coinArray[n];
                    if(result.ContainsKey(coinArray[n]))
                    {
                        result[coinArray[n]]++;
                    }
                    else
                    {
                        result[coinArray[n]] = 1;
                    }
                }
            }

            return result;
        }
    }
}
