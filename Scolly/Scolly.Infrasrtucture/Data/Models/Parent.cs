using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scolly.Infrastructures.Data.Models
{
    public class Parent
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        public List<Child> Children { get; set; } = new List<Child>();
    }
}
