using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeMaker
{
    class ViewModel
    {
        public string CoinString { get; set; }
        public string Amount { get; set; }
        public bool Greedy { get; set; }
        public bool Dynamic { get; set; }
        public bool Time { get; set; }
    }
}
