using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEGES.Shared.Entities
{
    public class LearnMoreComments
    {
        public int Id { get; set; }

        [Display(Name = "Comentario")]
        [MaxLength(5000, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Comment { get; set; } = null!;

        public DateTime CreationDate { get; set; }
        public Guid CreatedBy { get; set; }

        public int? Rating { get; set; }
        public Guid SessionId { get; set; }
    }
}