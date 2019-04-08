using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeMaker
{
    class ViewModel : INotifyPropertyChanged
    {

        public string CoinString { get; set; }
        public string Amount { get; set; }
        public bool Greedy { get; set; } = true;
        public bool Dynamic { get; set; }
        public bool Time { get; set; } = true;

        #region Execution time variables

        private string _ExeTimeGreedy;
        public string ExeTimeGreedy
        {
            get => _ExeTimeGreedy;
            set
            {
                _ExeTimeGreedy = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(ExeTimeGreedy)));
            }
        }

        private string _ExeTimeDynamic;
        public string ExeTimeDynamic
        {
            get => _ExeTimeDynamic;
            set
            {
                _ExeTimeDynamic = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(ExeTimeDynamic)));
            }
        }

        #endregion



        public event PropertyChangedEventHandler PropertyChanged;
    }
}
