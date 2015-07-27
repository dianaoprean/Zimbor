using System.ComponentModel.DataAnnotations;

namespace UI.Admin.Models
{
    public class ConcurentaModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Nume { get; set; }

        [StringLength(500)]
        public string Descriere { get; set; }
    }
}