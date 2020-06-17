using System.Collections.Generic;
using Persistance.Interfaces;
using Contracts.Models;
using System.Linq;
using Application.Interfaces;
using System.Text.RegularExpressions;
using System;

namespace Application.Services
{
    public class EmploeeSearchService : IEmploeeSearchService
    {
        protected readonly IRepository<Emploee> _repository;

        public EmploeeSearchService(IRepository<Emploee> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Emploee> GetAll()
        {
            return _repository.GetAllQueryable();
        }

        public IEnumerable<Emploee> GetEmploeesByCompany(string Company)
        {
            return _repository.GetAllQueryable().Where(x => x.Company.ToLower() == Company.ToLower());   
        }

        public Emploee GetEmploeeByFullName(string fullName)
        {
            return _repository.GetAllQueryable().Where(x => $"{x.Surname} {x.Initials}".ToLower() == fullName.ToLower()).FirstOrDefault();
        }

        public void CreateEmploee(Emploee entity)
        {
            if (!Regex.IsMatch(entity.PhoneNumber, @"\d{11}"))
                throw new ArgumentException("invlid phone number");

            entity.Id = _repository.GetAllQueryable().Select(x => x.Id).Max() + 1;
            _repository.Add(entity);
        }

        public void DeleteEmploee(Emploee entity)
        {
            _repository.Remove(entity);
        }

        public void SortByName(bool desc)
        {
            var emploees = _repository.GetAllQueryable();
            _repository.RemoveRange(emploees);

            _repository.AddRange(desc ? 
                                 emploees.OrderByDescending(x => x.Surname).ThenBy(x => x.Initials) :
                                 emploees.OrderBy(x => x.Surname).ThenBy(x => x.Initials));
        }

        public void SortByCompany(bool desc)
        {
            var emploees = _repository.GetAllQueryable();
            _repository.RemoveRange(emploees);

            _repository.AddRange(desc ?
                                emploees.OrderByDescending(x => x.Company) :
                                emploees.OrderBy(x => x.Company));
        }
    }
}
