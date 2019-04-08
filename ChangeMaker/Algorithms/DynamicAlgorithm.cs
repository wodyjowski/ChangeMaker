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
        public DynamicAlgorithm(IEnumerable<int> coins) : base(coins)
        {
        }

        public override IEnumerable<int> CalculateResult(float amount)
        {
            throw new NotImplementedException();
        }
    }
}
