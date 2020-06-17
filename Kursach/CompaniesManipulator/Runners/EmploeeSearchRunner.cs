using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Application.Interfaces;
using Contracts.Models;

namespace Application.Runners
{
    public class EmploeeSearchRunner : IRunner
    {
        protected readonly IPrinter printer;
        protected readonly IEmploeeSearchService service;

        public EmploeeSearchRunner(IPrinter _printer, IEmploeeSearchService _service)
        {
            printer = _printer;
            service = _service;
        }

        public void Start()
        {
            try
            {

                var emploees = service.GetAll();

                printer.Write("Id \t Company \t Surname \t Initials \t PhoneNumber\n");
                foreach (var emp in emploees)
                    printer.Write($"{emp.Id} \t {emp.Company} \t {emp.Surname} \t {emp.Initials} \t\t {emp.PhoneNumber}");

                printer.Write("\n\nInput Surname and initials ");
                var fullName = printer.Read();

                var foundEmp = service.GetEmploeeByFullName(fullName);

                if (foundEmp != null)
                    printer.Write($"Emploee was found:\n\n{foundEmp.Id} \t {foundEmp.Company} \t {foundEmp.Surname} \t {foundEmp.Initials} \t\t {foundEmp.PhoneNumber}");
                else
                    printer.Write("Don't exist");

                printer.Write("\n\nInput Company ");
                var company = printer.Read();

                var foundEploees = service.GetEmploeesByCompany(company);

                if(foundEploees != null)
                    foreach (var emp in foundEploees)
                        printer.Write($"{emp.Id} \t {emp.Company} \t {emp.Surname} \t {emp.Initials} \t\t {emp.PhoneNumber}");
                else
                    printer.Write("Don't exist");

                var emploee = new Emploee();

                printer.Write("\n\nEmploee creating\nInput Company ");
                emploee.Company = printer.Read();

                printer.Write("Input Surname ");
                emploee.Surname = printer.Read();

                printer.Write("Input Initials ");
                emploee.Initials = printer.Read();

                printer.Write("Input Phone number ");
                emploee.PhoneNumber = printer.Read();

                service.CreateEmploee(emploee);

                emploees = service.GetAll();

                printer.Write("Id \t Company \t Surname \t Initials \t PhoneNumber\n");
                foreach (var emp in emploees)
                    printer.Write($"{emp.Id} \t {emp.Company} \t {emp.Surname} \t {emp.Initials} \t\t {emp.PhoneNumber}");

                printer.Write("\n\nInput id for deleting");
                var deletingId = int.Parse(printer.Read());

                service.DeleteEmploee(emploees.Where(x => x.Id == deletingId).FirstOrDefault());

                emploees = service.GetAll();

                printer.Write("Id \t Company \t Surname \t Initials \t PhoneNumber\n");
                foreach (var emp in emploees)
                    printer.Write($"{emp.Id} \t {emp.Company} \t {emp.Surname} \t {emp.Initials} \t\t {emp.PhoneNumber}");

                printer.Write("\n\nSorted by name: \n\n");

                service.SortByName(false);

                emploees = service.GetAll();

                printer.Write("Id \t Company \t Surname \t Initials \t PhoneNumber\n");
                foreach (var emp in emploees)
                    printer.Write($"{emp.Id} \t {emp.Company} \t {emp.Surname} \t {emp.Initials} \t\t {emp.PhoneNumber}");

                printer.Write("\n\nSorted by company descending: \n\n");

                service.SortByCompany(true);

                emploees = service.GetAll();

                printer.Write("Id \t Company \t Surname \t Initials \t PhoneNumber\n");
                foreach (var emp in emploees)
                    printer.Write($"{emp.Id} \t {emp.Company} \t {emp.Surname} \t {emp.Initials} \t\t {emp.PhoneNumber}");
            }
            catch(Exception ex)
            {
                printer.Write($"\n\n{ex.Message}\n\n");
            }
        }
    }
}
