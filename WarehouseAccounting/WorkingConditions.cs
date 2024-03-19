using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseAccounting
{
    public class WorkingConditions
    {
        public class RangeOfValues
        {
            public float min { get; set; }
            public float max { get; set; }

            public RangeOfValues(float min, float max)
            {
                this.min = min;
                this.max = max;
            }
        }

        public RangeOfValues temperature;
        public RangeOfValues voltage;

        public WorkingConditions(RangeOfValues temperature, RangeOfValues voltage)
        {
            this.temperature = temperature;
            this.voltage = voltage;
        }
    }
}
