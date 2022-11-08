using System.ComponentModel.DataAnnotations;

namespace OcelotApiGateway.DTOs
{
    public class ProgressNoteDTO
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime CreateDateTime{ get; set; }

        public int ObservationId { get; set; }
    }
}