using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEGES.Shared.Entities
{
    public class Role
    {
        public int RoleId { get; set; }

        [Display(Name = "Rol")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string RoleName { get; set; } = null!;

        [Display(Name = "Descripción del Rol")]
        [MaxLength(1500, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string? RoleDescription { get; set; }

        public List<UserApp> Users { get; set; }
        public List<Rel_RolPermission> RelPermissions { get; set; }

    }
}
