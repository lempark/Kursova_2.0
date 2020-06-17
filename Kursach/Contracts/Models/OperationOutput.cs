using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Models
{
    public class OperationOutput
    {
        public double DepthOfCutting { get; set; }
        public double Supply { get; set; }
        public double PassportSupply { get; set; }
        public double CuttingSpeed { get; set; }
        public double Torque { get; set; }
        public double CuttingForce { get; set; }
        public double CuttingPower { get; set; }
        public double RotationalSpeed { get; set; }
        public double OperationTime { get; set; }
    }
}
