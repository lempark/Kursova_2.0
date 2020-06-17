using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Models
{
    public class CuttingOperation
    {
        public OperationInput OperationInput { get; set; }
        public OperationOutput OperationOutput { get; set; }
        public Dictionary<double, double> SupplyTable { get; set; }
        public List<CoeficientsAndIndicators> CoeficientsAndIndicatorsTable { get; set; }
    }
}
