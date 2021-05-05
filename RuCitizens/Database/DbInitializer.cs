using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RuCitizens.Database
{
    public class DbInitializer
    {
        public static void Initialize(DatabaseContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Citizens.Any())
            {
                return;   // DB has been seeded
            }

            var citizens = new Citizen[]
            {
                new Citizen() { FullName = "Иванов Иван Иванович", Inn = "111111111111", Snils="111-111-111-11", BirthDate = new DateTime(1980, 01, 01) },
                new Citizen() { FullName = "Петров Петр Петрович", Inn = "222222222222", Snils="222-222-222-22", BirthDate = new DateTime(1970, 01, 01) },
                new Citizen() { FullName = "Пупкин Василий Иванович", BirthDate = new DateTime(1936, 01, 01), DeathDate = new DateTime(1980, 01, 01) },           
            };

            foreach (Citizen s in citizens)
            {
                context.Citizens.Add(s);
            }
            context.SaveChanges();

        }
}
}
