using SEGES.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEGES.Shared.Entities
{
    public class Goal : IEntityWithCreationDate
    {
        public int GoalId { get; set; }

        [Display(Name = "Objetivo")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string GoalName { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(4000, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string GoalDescription { get; set; }

        public int Project_ID { get; set; }
        public Project? Project { get; set; }



        [Display(Name = "Fecha de Creación")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public DateTime CreationDate { get; set; }

        public ICollection<KPI>? KPIs { get; set; }
        public ICollection<Requirement>? Requirements { get; set; }


    }
}
