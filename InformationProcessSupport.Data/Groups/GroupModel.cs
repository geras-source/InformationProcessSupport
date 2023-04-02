using InformationProcessSupport.Data.ScheduleOfSubjects;
using InformationProcessSupport.Data.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InformationProcessSupport.Data.Groups
{
    public class GroupModel
    {
        public int GroupId { get; set; }
        public ulong? AlternateKey { get; set; }
        public string GroupName { get; set; }
        public ulong? GuildId { get; set; }
        public string? GuildName { get; set; }
        public virtual ICollection<UserModel> UserEntity { get; set; }
        public virtual ScheduleModel ScheduleEntity { get; set; }
        public class GroupConfiguration : IEntityTypeConfiguration<GroupModel>
        {
            public void Configure(EntityTypeBuilder<GroupModel> builder)
            {
                builder.HasKey(x => x.GroupId);
            }
        }
    }
}