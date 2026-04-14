using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetorSignalSimulator.UI.Model
{
    public class SendDataModel
    {
        public List<int> portAddress { get; set; }
        public List<int> portIndex { get; set; }
        public int portCount { get; set; }
        public int cycle { get; set; }
    }
}
