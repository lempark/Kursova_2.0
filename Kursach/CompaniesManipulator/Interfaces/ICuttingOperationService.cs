

namespace Application.Interfaces
{
    public interface ICuttingOperationService
    {
        double CalculateDepthOfCutting();
        double CalculateSupply();
        double GetPassportSupply();
        double CalculateCuttingSpeed();
        double CalculateRotateMoment();
        double CalculateCuttingForce();
        double CalculateCuttingPower();
        double CalculateOperationTime();
        string GetInputInfo();
        string GetOutputInfo();
    }
}
