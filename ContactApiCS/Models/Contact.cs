using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ContactApiCS.Models
{
    [Table("contacts")]
    public class Contact
    {
        [Column("Id")]
        public int Id { get; set; }
        [JsonProperty("firstName")]
        [Required(ErrorMessage = "First name is required")]
        [StringLength(60, ErrorMessage = "First name can't be longer than 60 characters")]
        public string FirstName { get; set; } = string.Empty;
        [JsonProperty("lastName")]
        [Required(ErrorMessage = "Last name is required")]
        [StringLength(60, ErrorMessage = "Last name can't be longer than 60 characters")]
        public string LastName { get; set; } = string.Empty;
        [JsonProperty("companyName")]
        [Required(ErrorMessage = "Company name is required")]
        [StringLength(60, ErrorMessage = "Company name can't be longer than 60 characters")]
        public string CompanyName { get; set; } = string.Empty;
        [JsonProperty("mobile")]
        [Required(ErrorMessage = "Mobile is required")]
        [StringLength(60, ErrorMessage = "Mobile can't be longer than 20 characters")]
        public string Mobile { get; set; } = string.Empty;
        [JsonProperty("email")]
        [Required(ErrorMessage = "Email is required")]
        [StringLength(60, ErrorMessage = "Email can't be longer than 60 characters")]
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public int LastUpdatedBy { get; set; }
    }
}
