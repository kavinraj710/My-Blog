using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlogApplication.Models
{
    [Table("BlogUsers")]
    public class User : BaseModel
    {
        [PrimaryKey("id", true)]
        public Guid id { get; set; } = Guid.NewGuid(); // Unique identifier

        [Column("name")]
        [Required(ErrorMessage = "Name is required.")]
        public string name { get; set; } = string.Empty;

        [Column("email")]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string email { get; set; } = string.Empty;

        [Column("role")]
        public string role { get; set; } = "user";

        [Column("profile_image")]
        public byte[]? profile_image { get; set; }


        [Column("created_at")]
        public DateTime created_at { get; set; } = DateTime.UtcNow;
    }
}
