namespace InformationProcessSupport.Core.Domains
{
    public class GroupEntity
    {
        public int GroupId { get; set; }
        public ulong AlternateKey { get; set; }
        public string GroupName { get; set; }
        public ulong GuildId { get; set; }
        public string? GuildName { get; set; }
    }
}