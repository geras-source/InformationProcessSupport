using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using InformationProcessSupport.Data.Statistics;

namespace InformationProcessSupport.Data.Users
{
    public class UserEntity
    {
        /// <summary>
        /// Primary key in a database (don't used)
        /// </summary>
        public ulong UserId { get; set; }
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
        public string? GuildName { get; set; } //TODO: переделать на гуилдАйди
        /// <summary>
        /// One-to-many collection (don't used)
        /// </summary>
        public ICollection<StatisticEntity> StatisticsEntity { get; set; }

        public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
        {
            public void Configure(EntityTypeBuilder<UserEntity> builder)
            {
                builder.HasKey(x => x.UserId);
                //TODO: убрать альтернативный ключ
                //builder.HasAlternateKey(x => x.AlternateKey);
            }
        }
    }
}