

namespace Contracts.Models
{
    public class OperationOutput
    {
        public double DepthOfCutting { get; set; }
        public double Supply { get; set; }
        public double PassportSupply { get; set; }
        public double CuttingSpeed { get; set; }
        public double RotateMoment { get; set; }
        public double CuttingForce { get; set; }
        public double CuttingPower { get; set; }
        public double RotationalSpeed { get; set; }
        public double OperationTime { get; set; }

        new public string ToString()
        {
            return $"DepthOfCutting: {DepthOfCutting}\nSupply: {Supply}\nPassportSupply: {PassportSupply}\nCuttingSpeed: {CuttingSpeed}\n" +
                   $"RotateMoment: {RotateMoment}\nCuttingForce: {CuttingForce}\nCuttingPower: {CuttingPower}\nRotationalSpeed: {RotationalSpeed}\nOperationTime: {OperationTime}";
        }
    }
}
