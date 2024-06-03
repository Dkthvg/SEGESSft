using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEGES.Shared.DTOs
{
    public class RequirementDTO
    {
        public int RequirementID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string RequirementDescription { get; set; } = string.Empty;
        public int Project_ID { get; set; }
    }
}
