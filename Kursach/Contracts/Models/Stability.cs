using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Models
{
    public class Stability
    {
        public string Material { get; set; }
        public string InstrumentMaterial { get; set; }
        public double Diametr { get; set; }
        public double StabilityValue { get; set; }
    }
}
