﻿using System.Text.Json.Serialization;

namespace InformationProcessSupport.Web.Dtos
{
    public class GroupDto
    {
        [JsonPropertyName("group_name")]
        public string GroupName { get; set; }
        [JsonPropertyName("guild_name")]
        public string? GuildName { get; set; }
    }
}
