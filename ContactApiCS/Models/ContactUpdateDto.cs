using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ContactApiCS.Models
{
    public class ContactUpdateDto
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; } = string.Empty;
        [JsonProperty("lastName")]
        public string LastName { get; set; } = string.Empty;
        [JsonProperty("companyName")] public string CompanyName { get; set; } = string.Empty;
        [JsonProperty("mobile")] public string Mobile { get; set; } = string.Empty;
        [JsonProperty("email")] public string Email { get; set; } = string.Empty;
    }
}
