using System;
using Contracts.Models;
using Application.Interfaces;
using System.Linq;

namespace Application.Services
{
    public class CuttingOperationService : ICuttingOperationService
    {
        protected readonly CuttingOperation operation;

        public CuttingOperationService(CuttingOperation _operation)
        {
            operation = _operation;
        }

        public double CalculateDepthOfCutting()
        {
            operation.OperationOutput.DepthOfCutting = 0.5 * operation.OperationInput.Diametr;
            return operation.OperationOutput.DepthOfCutting;
        }

        public double CalculateSupply()
        {
            operation.OperationOutput.Supply = operation.SupplyTable.Where(x => Math.Abs(x.Key - operation.OperationInput.Diametr) < 3).FirstOrDefault().Value * operation.Kjs * operation.Kos;
            return operation.OperationOutput.Supply;
        }

        public double GetPassportSupply()
        {
            operation.OperationOutput.PassportSupply = operation.OperationInput.Workbench.PassportSupply
                                                        .Where(x => x - operation.OperationOutput.Supply <= 0 || (x - operation.OperationOutput.Supply) / operation.OperationOutput.Supply <= 0.05)
                                                        .OrderByDescending(x => x)
                                                        .FirstOrDefault();
            return operation.OperationOutput.PassportSupply;
        }

        public double CalculateCuttingSpeed()
        {
            double Cv = operation.CoeficientsAndIndicatorsTable
                        .Where(x => operation.OperationInput.Material.Contains(x.Material) && operation.OperationInput.InstrumentMaterial.Contains(x.InstrumentMaterial))
                        .Where(x => operation.OperationOutput.Supply > x.SupplyStart && operation.OperationOutput.Supply <= x.SupplyEnd)
                        .OrderByDescending(x => x.Cv).FirstOrDefault().Cv;

            double T = operation.StabilityTable
                       .Where(x => operation.OperationInput.Material.Contains(x.Material) && operation.OperationInput.InstrumentMaterial.Contains(x.InstrumentMaterial))
                       .Where(x => operation.OperationInput.Diametr >= x.DiametrStart && operation.OperationInput.Diametr <= x.DiametrEnd).FirstOrDefault().StabilityValue;

            double K1v = operation.K1vTable.Where(x => x.Key / 100 >= 1).OrderBy(x => x.Key).FirstOrDefault().Value;

            operation.OperationOutput.CuttingSpeed = Cv * operation.OperationInput.Diametr * operation.Kiv * K1v / (T * operation.OperationOutput.Supply);

            return operation.OperationOutput.CuttingSpeed;
        }

        public double CalculateRotateMoment()
        {
            double Cm = operation.RotateMomentTable
                        .Where(x => operation.OperationInput.Material.Contains(x.Material) && operation.OperationInput.InstrumentMaterial.Contains(x.InstrumentMaterial))
                        .OrderByDescending(x => x.Cm).FirstOrDefault().Cm;

            operation.OperationOutput.RotateMoment = 10 * Cm * operation.OperationInput.Diametr * operation.OperationOutput.Supply * operation.Kp;

            return operation.OperationOutput.RotateMoment;
        }

        public double CalculateCuttingForce()
        {
            double Cp = operation.RotateMomentTable
                        .Where(x => operation.OperationInput.Material.Contains(x.Material) && operation.OperationInput.InstrumentMaterial.Contains(x.InstrumentMaterial))
                        .OrderByDescending(x => x.Cp).FirstOrDefault().Cp;

            operation.OperationOutput.CuttingForce = 10 * Cp * operation.OperationInput.Diametr * operation.OperationOutput.Supply * operation.Kp;

            return operation.OperationOutput.CuttingForce;
        }

        public double CalculateCuttingPower()
        {
            double n = 1000 * operation.OperationOutput.CuttingSpeed / (3.14 * operation.OperationInput.Diametr);
            double n_cor = operation.OperationInput.Workbench.PassportRotationalSpeed
                           .Where(x => x - n <= 0 || (x - n) / n <= 0.05)
                           .OrderByDescending(x => x)
                           .FirstOrDefault();

            operation.OperationOutput.RotationalSpeed = n;
            operation.OperationOutput.CuttingPower = operation.OperationOutput.RotateMoment * n_cor / 9750;

            return operation.OperationOutput.CuttingPower;
        }

        public double CalculateOperationTime()
        {
            double y = operation.OperationInput.Diametr * 0.4;

            operation.OperationOutput.OperationTime = (operation.OperationInput.Depth + y) / operation.OperationOutput.RotationalSpeed * operation.OperationOutput.PassportSupply;

            return operation.OperationOutput.OperationTime;
        }

        public string GetInputInfo()
        {
            return operation.OperationInput.ToString();
        }

        public string GetOutputInfo()
        {
            return operation.OperationOutput.ToString();
        }

    }
}
