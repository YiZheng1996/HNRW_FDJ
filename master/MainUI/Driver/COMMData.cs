using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetorSignalSimulator.UI.Model
{
    public struct COMMData
    {
        public int Port { get; set; }
        public int Offset { get; set; }
        public int Bit { get; set; }

        public override string ToString()
        {
            return string.Format("0x{0:X}.{1:000}.{2:00}", Port, Offset, Bit);
            //return base.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is COMMData)) return false;
            COMMData o = (COMMData)obj;
            return this.Port == o.Port && this.Offset == o.Offset && this.Bit == o.Bit;
        }

    }
}
