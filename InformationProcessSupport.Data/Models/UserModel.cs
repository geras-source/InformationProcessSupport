using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InformationProcessSupport.Data.Models
{
    public class UserModel
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
        public ICollection<StatisticModel> StatisticsEntity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? GroupId { get; set; }
        /// <summary>   
        /// 
        /// </summary>
        public virtual GroupModel? GroupEntity { get; set; }
        public class UserConfiguration : IEntityTypeConfiguration<UserModel>
        {
            public void Configure(EntityTypeBuilder<UserModel> builder)
            {
                builder.HasKey(x => x.UserId);

                builder
                    .HasOne(x => x.GroupEntity)
                    .WithMany(x => x.UserEntity)
                    .HasForeignKey(x => x.GroupId);
            }
        }
    }
}
