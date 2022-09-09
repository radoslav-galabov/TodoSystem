using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TodoSystem.Enums;

namespace TodoSystem.Entities
{
    [Table("Todo")]
    public class TodoEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
