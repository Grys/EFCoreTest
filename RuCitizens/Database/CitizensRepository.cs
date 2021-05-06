using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RuCitizens.Database
{
    public class CitizensRepository : ICitizenRepository
    {
        private DatabaseContext _databaseContext;
        public CitizensRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Citizen FindById(int id)
        {
            return this._databaseContext.Citizens.Find(id);
        }
        public void Create(Citizen item)
        {
            this._databaseContext.Citizens.Add(item);
        }

        public void Create(IEnumerable<Citizen> items)
        {
            var toInsert = items.ToArray();

            Array.ForEach(toInsert, item =>
            {
                item.Id = 0;
            });

            this._databaseContext.Citizens.AddRange(toInsert);
        }
        public void Delete(int id)
        {
            var toDelete = this._databaseContext.Citizens.Find(id);
            if (toDelete != null)
                this._databaseContext.Citizens.Remove(toDelete);
        }

        public IEnumerable<Citizen> FindByFIOAndDates(string FIO, DateTime? birthDate, DateTime? deathDate)
        {
            if (string.IsNullOrEmpty(FIO) && !birthDate.HasValue && !deathDate.HasValue)
                return Enumerable.Empty<Citizen>();

            IQueryable<Citizen> ret = Enumerable.Empty<Citizen>().AsQueryable();

            if (!string.IsNullOrEmpty(FIO))
                ret = this._databaseContext.Citizens.Where(x => EF.Functions.Like(x.FullName, $"%{FIO}%"));
            
            if (birthDate.HasValue)
                ret = this._databaseContext.Citizens.Where(x => x.BirthDate == birthDate.Value);

            if (deathDate.HasValue)
                ret = this._databaseContext.Citizens.Where(x => x.DeathDate == deathDate);

            return ret.AsEnumerable();
        }

        public Citizen FindByInn(string inn)
        {
            return this._databaseContext.Citizens.FirstOrDefault(x => x.Inn == inn);
        }

        public Citizen FindBySnils(string snils)
        {
            return this._databaseContext.Citizens.FirstOrDefault(x => x.Snils == snils);
        }

        public Citizen GetCitizen(int id)
        {
            return this._databaseContext.Citizens.Find(id);
        }

        public void Update(int id, Citizen item)
        {
            var toUpdate = this._databaseContext.Citizens.Find(id);
            
            if (toUpdate == null)
                throw new Exception("Citizen not found");

            var attached = this._databaseContext.Citizens.Attach(toUpdate);

            if (attached == null)
                throw new Exception("Citizen not found");


            attached.Entity.FullName = item.FullName;
            attached.Entity.BirthDate = item.BirthDate;
            attached.Entity.DeathDate = item.DeathDate;
            attached.Entity.Inn = item.Inn;
            attached.Entity.Snils = item.Snils;

        }

        public void Save()
        {
            this._databaseContext.SaveChanges();
        }

    }
}
