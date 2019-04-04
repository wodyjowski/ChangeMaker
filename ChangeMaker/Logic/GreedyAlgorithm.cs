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
        public GreedyAlgorithm(IEnumerable<int> coins) : base(coins)
        {
        }

        public override IEnumerable<int> CalculateResult(int amount)
        {
            List<int> resultList = new List<int>();

            int i = 0;
            while (amount > 0)
            {
                if (i >= coins.Count())
                {
                    resultList = null;
                    break;
                }

                var maxCoin = coins.ElementAt(i);

                if (amount >= maxCoin)
                {
                    resultList.Add(maxCoin);
                    amount -= maxCoin;
                }
                else
                {
                    ++i;
                }
            }

            return resultList;
        }
    }
}
