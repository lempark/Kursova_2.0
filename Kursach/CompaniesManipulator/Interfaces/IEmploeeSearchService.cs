using System;
using System.Collections.Generic;
using System.Text;
using Contracts.Models;

namespace Application.Interfaces
{
    public interface IEmploeeSearchService
    {
        IEnumerable<Emploee> GetAll();
        IEnumerable<Emploee> GetEmploeesByCompany(string Company);
        Emploee GetEmploeeByFullName(string fullName);
        void CreateEmploee(Emploee entity);
        void DeleteEmploee(Emploee entity);
        void SortByName(bool desc);
        void SortByCompany(bool desc);
    }
}
