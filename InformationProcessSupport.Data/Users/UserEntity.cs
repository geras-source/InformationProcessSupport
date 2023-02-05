using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using InformationProcessSupport.Data.Statistics;
using InformationProcessSupport.Data.Groups;

namespace InformationProcessSupport.Data.Users
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
        /// <summary>
        /// One-to-many collection (don't used)
        /// </summary>
        public ICollection<StatisticEntity> StatisticsEntity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? GroupId { get; set; }
        /// <summary>   
        /// 
        /// </summary>
        public virtual GroupEntity? GroupEntity { get; set; }
        public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
        {
            public void Configure(EntityTypeBuilder<UserEntity> builder)
            {
                builder.HasKey(x => x.UserId);

                builder
                    .HasOne(x => x.GroupEntity)
                    .WithMany(x => x.UserEntity)
                    .HasForeignKey(x => x.GroupId);
                //TODO: убрать альтернативный ключ
                //builder.HasAlternateKey(x => x.AlternateKey);
            }
        }
    }
}