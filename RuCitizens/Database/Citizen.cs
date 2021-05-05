using System;
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
    }
}
