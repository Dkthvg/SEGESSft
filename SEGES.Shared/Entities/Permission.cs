using SEGES.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;


namespace SEGES.Shared.Entities
{
    public class Permission : IEntityWithCreationDate
    {
        public int Id { get; set; }

        [Display(Name = "Permiso")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Descripción Permiso")]
        [MaxLength(4000, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string? Description { get; set; } = null;


        [Display(Name = "Id del Módulo")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int Module_ID { get; set; }
        public Module Module { get; set; } = null!;

        //public List<Module> Modules { get; set;}
        //public List<Permission> Permissions { get; set; } = null!;

        public DateTime CreationDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
