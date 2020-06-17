using System;
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
        static void Main(string[] args)
        {
            IRepository<Emploee> repo = new EmploeeRepository(@"..\..\..\..\Emploees.txt");
            IPrinter printer = new ConsolePrinter();
            IEmploeeSearchService empSrevice = new EmploeeSearchService(repo);
            IRunner empRunner = new EmploeeSearchRunner(printer, empSrevice);
            empRunner.Start();
        }
    }
}
