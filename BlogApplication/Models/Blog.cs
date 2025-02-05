using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlogApplication.Models
{
    [Table("Blog")]
    public class blog : BaseModel
    {
        [PrimaryKey("id", true)]
        public Guid id { get; set; } = Guid.NewGuid(); // Unique identifier

        [Column("title")]
        [Required(ErrorMessage = "Name is required.")]
        public string title { get; set; } = string.Empty;

        [Column("summary")]
        [Required(ErrorMessage = "Summary is required.")]
        public string summary { get; set; } = string.Empty;

        [Column("content")]
        [Required(ErrorMessage = "Content is required.")]
        public string content { get; set; } = string.Empty;
        [Column("email")]
        [Required(ErrorMessage = "Email is required.")]
        public string email { get; set; } = string.Empty;

        [Column("published_at")]
        public DateTime created_at { get; set; } = DateTime.UtcNow;
    }
}
