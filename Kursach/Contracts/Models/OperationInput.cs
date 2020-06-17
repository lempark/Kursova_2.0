

namespace Contracts.Models
{
    public class OperationInput
    {
        public OperationInput(Workbench workbench)
        {
            Workbench = workbench;
        }

        public Workbench Workbench { get; set; }
        public string Material { get; set; }
        public string Hardness { get; set; }
        public double Diametr { get; set; }
        public double Depth { get; set; }
        public bool Through { get; set; }
        public double Quality { get; set; }
        public double Rigidity { get; set; }
        public string CuttingInstrument { get; set; }
        public string InstrumentMaterial { get; set; }
    }
}
