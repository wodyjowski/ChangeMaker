using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeMaker.Logic.Base
{
    abstract class Algorithm
    {
        protected IEnumerable<int> coins;
        public Algorithm(IEnumerable<int> coins)
        {
            this.coins = coins.OrderBy(c => c) ?? throw new ArgumentNullException();
        }
    }
}
