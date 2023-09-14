using System.Text.Json.Serialization;

namespace InformationProcessSupport.Server.Dtos
{
    public class UsersDto
    {
        /// <summary>
        /// Primary key in a database (don't used)
        /// </summary>
        [JsonPropertyName("user_id")]
        public int UserId { get; set; }
        /// <summary>
        /// The real id that user
        /// </summary>
        [JsonPropertyName("user_name")]
        public string? Name { get; set; }
        /// <summary>
        /// The nickname that user
        /// </summary>
        [JsonPropertyName("user_nickname")]
        public string? Nickname { get; set; }
        /// <summary>
        /// His affiliation by roles
        /// </summary>
        [JsonPropertyName("user_roles")]
        public string? Roles { get; set; }
        [JsonPropertyName("user_group")]
        public string? GroupName { get; set; }
    }
}
