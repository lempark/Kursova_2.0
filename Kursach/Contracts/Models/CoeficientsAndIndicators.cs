using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Models
{
    public class CoeficientsAndIndicators
    {
        public string Material { get; set; }
        public string InstrumentMaterial { get; set; }
        public double SupplyStart { get; set; }
        public double SupplyEnd { get; set; }
        public double Cv { get; set; }
        public double Q { get; set; }
        public double Y { get; set; }
        public double M { get; set; }
    }
}
