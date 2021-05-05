using System;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RuCitizens.Database
{
    public class Citizen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        public string FullName { get; set; }

        [MaxLength(14)]
        public string Snils { get; set; }

        [MaxLength(12)]
        public string Inn { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime? DeathDate { get; set; }

        public bool Validate()
        {
            if (string.IsNullOrEmpty(this.FullName))
                throw new Exception("FullName is empty");

            if (this.BirthDate > DateTime.Now)
                throw new Exception("Birth date must be less then current date");

            if (this.DeathDate.HasValue && this.DeathDate.Value > DateTime.Now)
                throw new Exception("Death date must be less then current date");


            if (this.DeathDate.HasValue && this.DeathDate.Value < this.BirthDate)
                throw new Exception("Check birth and death dates");


            if (!string.IsNullOrEmpty(this.Inn) && !Regex.IsMatch(this.Inn, "\\d{12}"))
            {
                throw new Exception("Check Inn value");
            }

            if (!string.IsNullOrEmpty(this.Snils) && !Regex.IsMatch(this.Snils, "\\d{3}-\\d{3}-\\d{3}-\\d{2}"))
            {
                throw new Exception("Check Snils value");
            }

            return true;
        }
    }
}
