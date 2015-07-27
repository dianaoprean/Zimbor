using System;
using System.ComponentModel.DataAnnotations;

namespace UI.Admin.Models
{
    public class StudiuPiataModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nume { get; set; }

        [Required]
        [Display(Name = "Numar Turisti")]
        public int NrTuristi { get; set; }

        [Required]
        [Display(Name = "Data Studiului")]
        public DateTime DataStudiu { get; set; }

        public string Detalii { get; set; }
    }
}