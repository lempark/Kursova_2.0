using System;
using System.Collections.Generic;
using Persistance.Interfaces;
using Contracts.Models;
using System.Linq;
using System.IO;

namespace Persistance.Repositories
{
    public class EmploeeRepository : IRepository<Emploee>
    {
        protected readonly string _filePath;
        public EmploeeRepository(string filePath)
        {
            _filePath = filePath;
        }

        public void Add(Emploee entity)
        {
            File.AppendAllLines(_filePath, new string[] { $"{entity.Id} {entity.Company} {entity.Surname} {entity.Initials} {entity.PhoneNumber}" }.AsEnumerable());
        }

        public void AddRange(IEnumerable<Emploee> entities)
        {
            File.AppendAllLines(_filePath, entities.Select(x => $"{x.Id} {x.Company} {x.Surname} {x.Initials} {x.PhoneNumber}"));
        }

        public IQueryable<Emploee> GetAllQueryable()
        {
            var strings = File.ReadAllLines(_filePath);
            return strings.Select(
                x => new Emploee 
                { 
                    Id = int.Parse(x.Split(new char[] { ' ' } , StringSplitOptions.RemoveEmptyEntries)[0]),
                    Company = x.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1],
                    Surname = x.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[2],
                    Initials = x.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[3],
                    PhoneNumber = x.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[4]
                }
            ).AsQueryable();
        }

        public void Remove(Emploee entity)
        {
            var strings = File.ReadAllLines(_filePath);
            strings = strings.Where(x => x.ToLower() != $"{entity.Id} {entity.Company} {entity.Surname} {entity.Initials} {entity.PhoneNumber}".ToLower()).ToArray();
            File.Delete(_filePath);
            File.WriteAllLines(_filePath, strings);
        }

        public void RemoveRange(IEnumerable<Emploee> entities)
        {
            var strings = File.ReadAllLines(_filePath);
            strings = strings.Where(x => !entities.Select(e => $"{e.Id} {e.Company} {e.Surname} {e.Initials} {e.PhoneNumber}".ToLower()).Contains(x.ToLower())).ToArray();
            File.Delete(_filePath);

            if (strings != null)
                File.WriteAllLines(_filePath, strings);
            else
                File.Create(_filePath);

        }
    }
}
