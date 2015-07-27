using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UI.Admin.Models
{
    public class PrimireTuristicaModel
    {
        public int Id { get; set; }

        [Required]
        [Range(1, 8, ErrorMessage = "Selectati zona.")]
        public int ZonaId { get; set; }

        [Required]
        [StringLength(50)]
        public string Nume { get; set; }

        [Required]
        [StringLength(200)]
        public string Strada { get; set; }

        [Required]
        [StringLength(100)]
        public string Localitate { get; set; }

        [StringLength(10)]
        public string CodPostal { get; set; }

        [StringLength(50)]
        public string Telefon { get; set; }

        [StringLength(50)]
        [Display(Name = "Nume Proprietar")]
        public string NumeProprietar { get; set; }

        public List<int> ImageIds { get; set; }
    }
}