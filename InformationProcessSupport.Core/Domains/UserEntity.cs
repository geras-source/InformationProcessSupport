namespace InformationProcessSupport.Core.Domains
{
    public class UserEntity
    {
        /// <summary>
        /// Primary key in a database (don't used)
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// The real id that user
        /// </summary>
        public ulong AlternateKey { get; set; }
        /// <summary>
        /// The real name that user
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// The nickname that user
        /// </summary>
        public string? Nickname { get; set; }
        /// <summary>
        /// His affiliation by roles
        /// </summary>
        public string? Roles { get; set; }
        /// <summary>
        /// The name of the guild to which this user belongs
        /// </summary>
        public string? GuildName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ulong GuildId { get; set; }
        public int? GroupId { get; set; }
    }
}