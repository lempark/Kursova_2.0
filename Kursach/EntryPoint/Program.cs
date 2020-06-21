using System;
using System.Linq;
using System.Collections.Generic;
using Application.Interfaces;
using Application.Printers;
using Application.Runners;
using Application.Services;
using Contracts.Models;
using Persistance.Interfaces;
using Persistance.Repositories;

namespace EntryPoint
{
    class Program
    {
        static IRunner CuttingOerationInitialize(IPrinter printer)
        {
            var workbench = new Workbench();
            workbench.Name = "2H135";
            workbench.PassportSupply = new List<double>() { 0.1, 0.14, 0.2, 0.28, 0.4, 0.56, 0.8, 1.12, 1.6 };
            workbench.PassportRotationalSpeed = new List<double>() { 31.5, 45, 63, 90, 125, 180, 250, 355, 500, 710, 1000, 1400 };

            var inp = new OperationInput(workbench)
            {
                Material = "Бронза БрА7, Алюміній АЛ7",
                Diametr = 30,
                Depth = 12,
                CuttingInstrument = "ТУ-2-035-721-80",
                InstrumentMaterial = "P6M5"
            };

            var coefTable = new List<CoeficientsAndIndicators>()
            {
                new CoeficientsAndIndicators()
                {
                    Material = "Бронза БрА7",
                    InstrumentMaterial = "P6M5",
                    SupplyStart = 0,
                    SupplyEnd = 0.3,
                    Cv = 28.1,
                    Q = 0.25,
                    Y = 0.55,
                    M = 0.125
                },
                new CoeficientsAndIndicators()
                {
                    Material = "Бронза БрА7",
                    InstrumentMaterial = "P6M5",
                    SupplyStart = 0.3,
                    SupplyEnd = 10,
                    Cv = 32.6,
                    Q = 0.25,
                    Y = 0.4,
                    M = 0.125
                },
                new CoeficientsAndIndicators()
                {
                    Material = "Алюміній АЛ7",
                    InstrumentMaterial = "P6M5",
                    SupplyStart = 0,
                    SupplyEnd = 0.3,
                    Cv = 36.3,
                    Q = 0.25,
                    Y = 0.55,
                    M = 0.125
                },
                new CoeficientsAndIndicators()
                {
                    Material = "Алюміній АЛ7",
                    InstrumentMaterial = "P6M5",
                    SupplyStart = 0.3,
                    SupplyEnd = 10,
                    Cv = 40.7,
                    Q = 0.25,
                    Y = 0.4,
                    M = 0.125
                }
            };

            var stabTable = new List<Stability>()
            {
                new Stability()
                {
                    Material = "Бронза БрА7",
                    InstrumentMaterial = "P6M5",
                    DiametrStart = 21,
                    DiametrEnd = 30,
                    StabilityValue = 75
                },
                new Stability()
                {
                    Material = "Бронза БрА7",
                    InstrumentMaterial = "P6M5",
                    DiametrStart = 31,
                    DiametrEnd = 40,
                    StabilityValue = 105
                }
            };

            var rotateTable = new List<RotateMomentCoeficients>()
            {
                new RotateMomentCoeficients()
                {
                    Material = "Бронза БрА7",
                    InstrumentMaterial = "P6M5",
                    Cm = 0.012,
                    Qm = 2,
                    Ym = 0.8,
                    Cp = 31.5,
                    Qp = 1,
                    Yp = 0.8
                },
                new RotateMomentCoeficients()
                {
                    Material = "Алюміній АЛ7",
                    InstrumentMaterial = "P6M5",
                    Cm = 0.005,
                    Qm = 2,
                    Ym = 0.8,
                    Cp = 9.8,
                    Qp = 1,
                    Yp = 0.7
                }
            };

            var operation = new CuttingOperation()
            {
                Kp = 1,
                Kiv = 1,
                Kos = 0.5,
                Kjs = 0.75,
                OperationInput = inp,
                SupplyTable = new Dictionary<double, double>() { { 25, 0.89 }, { 30, 0.96 }, { 35, 1.04 }, { 40, 1.19 } },
                CoeficientsAndIndicatorsTable = coefTable,
                StabilityTable = stabTable,
                K1vTable = new Dictionary<double, double>() { { 3*inp.Diametr,1},{ 4 * inp.Diametr, 0.85 },{ 5 * inp.Diametr, 0.75 },{ 6 * inp.Diametr, 0.7 },{ 8 * inp.Diametr, 0.6 } },
                RotateMomentTable = rotateTable
            };

            ICuttingOperationService service = new CuttingOperationService(operation);
            IRunner cuttingOperationRunner = new CuttingOperationRunner(service, printer);

            return cuttingOperationRunner;
        }

        static void Main(string[] args)
        {
            IRepository<Emploee> repo = new EmploeeRepository(@"..\..\..\..\Emploees.txt");
            IPrinter printer = new ConsolePrinter();
            IEmploeeSearchService empSrevice = new EmploeeSearchService(repo);
            IRunner empRunner = new EmploeeSearchRunner(printer, empSrevice);
            var cuttingRunner = CuttingOerationInitialize(printer);

            var menu = new Dictionary<string, IRunner>() { { "1", empRunner }, { "2", cuttingRunner } };

            Console.WriteLine("choose task (1/2)");
            var temp = Console.ReadLine();
            try
            {
                menu.Where(x => x.Key == temp).FirstOrDefault().Value.Start();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
