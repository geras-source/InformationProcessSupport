using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InformationProcessSupport.Web.Dtos
{
    public class UsersDto
    {

        [JsonPropertyName("user_id")]
        public int UserId { get; set; }
        
        [JsonPropertyName("user_name")]
        public string? Name { get; set; }

        [JsonPropertyName("user_nickname")]
        public string? Nickname { get; set; }

        [JsonPropertyName("user_roles")]
        public string? Roles { get; set; }

        [JsonPropertyName("user_group")]
        public string? GroupName { get; set; }

        [JsonIgnore]
        [DefaultValue(false)]
        public bool IsSelected { get; set; }

        [JsonIgnore]
        [DefaultValue(false)]
        public bool IsModified { get; set; }
    }
}
