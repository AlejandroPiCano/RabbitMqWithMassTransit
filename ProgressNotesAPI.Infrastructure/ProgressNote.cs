using System.ComponentModel.DataAnnotations;

namespace ProgressNotesAPI.Infrastructure
{
    public class ProgressNote
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Description { get; set; }

        public DateTime CreateDateTime{ get; set; }
    }
}