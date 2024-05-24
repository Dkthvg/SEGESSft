using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEGES.Shared.DTOs
{
    public class ProjectDTO
    {
        public string? ProjectName { get; set; }
        public string? ProjectDescription { get; set; }
        public DateTime? ProjectStartDate { get; set; }
        public DateTime? ProjectEndDate { get; set; }
        public int? ProjectStatus_ID { get; set; }
    }
}
