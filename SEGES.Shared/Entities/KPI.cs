using SEGES.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEGES.Shared.Entities
{
    public class KPI : IEntityWithCreationDate
    {
        public int KPI_ID { get; set; }

        [Display(Name = "KPI")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string KPI_Name { get; set; }

        [Display(Name = "Descripción KPI")]
        [MaxLength(4000, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string KPI_Description { get; set; }

        public string? KPI_Formula { get; set; }

        public DateTime CreationDate { get; set; }

        public int? Goal_Id { get; set; }
        public Goal? Goal { get; set; }

        public ICollection<SecundaryKPI> SecundaryKPIs { get; set; }
    }
}
