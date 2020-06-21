using System.Collections.Generic;

namespace Contracts.Models
{
    public class CuttingOperation
    {
        public CuttingOperation()
        {
            OperationOutput = new OperationOutput();
        }

        public double Kp { get; set; }
        public double Kiv { get; set; }
        public double Kjs { get; set; }
        public double Kos { get; set; }
        public OperationInput OperationInput { get; set; }
        public OperationOutput OperationOutput { get; set; }
        public Dictionary<double, double> SupplyTable { get; set; }
        public List<CoeficientsAndIndicators> CoeficientsAndIndicatorsTable { get; set; }
        public List<Stability> StabilityTable { get; set; }
        public Dictionary<double, double> K1vTable { get; set; }
        public List<RotateMomentCoeficients> RotateMomentTable { get; set; }
    }
}
