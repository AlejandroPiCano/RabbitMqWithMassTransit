using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OcelotApiGateway.DTOs
{
    public class ObservationDTO
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime? CreateDateTime { get; set; }
                
        public bool? Applyed { get; set; }

        public List<ProgressNoteDTO> ProgressNotes { get; set; }
    }
}
