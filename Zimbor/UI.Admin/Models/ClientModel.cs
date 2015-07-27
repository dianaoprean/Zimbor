using System.ComponentModel.DataAnnotations;

namespace UI.Admin.Models
{
    public class ClientModel
    {
        public int Id { get; set; }

        [Required]
        [Range(1, 42, ErrorMessage = "Selectati judetul.")]
        [Display(Name = "Judetul")]
        public int JudetId { get; set; }

        [Required]
        [StringLength(50)]
        public string Nume { get; set; }

        [Required]
        [StringLength(50)]
        public string Prenume { get; set; }

        [Required]
        [StringLength(300)]
        public string Adresa { get; set; }

        [Required]
        [StringLength(200)]
        public string Istoric { get; set; }

        [Required]
        [StringLength(100)]
        public string Localitate { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Cod Postal")]
        public string CodPostal { get; set; }

    }
}