using System;
using System.Collections.Generic;

namespace RuCitizens.Database
{
    interface ICitizenRepository
    {
        IEnumerable<Citizen> FindByFIOAndDates(string FIO, DateTime? birthDate, DateTime? deathDate);
        Citizen FindById(int id);
        Citizen FindByInn(string inn);
        Citizen FindBySnils(string snils);
        Citizen GetCitizen(int id);
        void Create(Citizen item);
        void Create(IEnumerable<Citizen> items);
        void Update(int id, Citizen item);
        void Delete(int id);
        void Save();
    }
}
