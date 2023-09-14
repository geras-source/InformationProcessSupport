namespace InformationProcessSupport.Core.Domains
{
    public class ChannelEntity
    {
        /// <summary>
        /// Primary key in a database (don't used)
        /// </summary>
        public int ChannelId { get; set; }
        /// <summary>
        /// The real id this channel
        /// </summary>
        public ulong AlternateKey { get; set; }
        /// <summary>
        /// The name that channel
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Voice or text channel
        /// </summary>
        public string? CategoryType { get; set; }
        /// <summary>
        /// The name of the guild to which this channel belongs
        /// </summary>
        public string? GuildName { get; set; }
        /// <summary>
        /// The id of the guild to which this channel belongs
        /// </summary>
        public ulong GuildId { get; set; }
    }
}