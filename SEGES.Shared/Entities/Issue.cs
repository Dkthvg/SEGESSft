using SEGES.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEGES.Shared.Entities
{
    public class Issue : IEntityWithCreationDate
    {
        public int IssueId { get; set; }

        [Display(Name = "Título Problema")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string IssueName { get; set; }

        [Display(Name = "Descripción Problema")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string IssueDescription { get; set; }


        public DateTime IssueStartDate { get; set; }
        public DateTime IssueEndDate { get; set; }
        public DateTime CreationDate { get; set; }

        public int Project_ID { get; set; }
        public Project Project { get; set; }


    }
}
