using System;
using Application.Interfaces;

namespace Application.Runners
{
    public class CuttingOperationRunner : IRunner
    {
        protected readonly ICuttingOperationService service;
        protected readonly IPrinter printer;

        public CuttingOperationRunner(ICuttingOperationService _service, IPrinter _printer)
        {
            service = _service;
            printer = _printer;
        }

        public void Start()
        {
            try
            {
                service.CalculateDepthOfCutting();
                service.CalculateSupply();
                service.GetPassportSupply();
                service.CalculateCuttingSpeed();
                service.CalculateRotateMoment();
                service.CalculateCuttingForce();
                service.CalculateCuttingPower();
                service.CalculateOperationTime();

                printer.Write("Operation input: ");
                printer.Write(service.GetInputInfo());

                printer.Write("\n\nOperation output: ");
                printer.Write(service.GetOutputInfo());
            }
            catch(Exception ex)
            {
                printer.Write(ex.Message);
            }
        }

    }
}
