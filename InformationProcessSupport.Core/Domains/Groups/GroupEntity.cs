namespace InformationProcessSupport.Core.Groups
{
    public class GroupEntity
    {
        public int GroupId { get; set; }
        public ulong AlternateKey { get; set; }
        public string GroupName { get; set; }
        public ulong GuildId { get; set; }
        public string? GuildName { get; set; }
        //public virtual ICollection<UserEntity> UserEntity { get; set; }
        //public virtual ScheduleEntity ScheduleEntity { get; set; }
        //public class GroupConfiguration : IEntityTypeConfiguration<GroupEntity>
        //{
        //    public void Configure(EntityTypeBuilder<GroupEntity> builder)
        //    {
        //        builder.HasKey(x => x.GroupId);
        //    }
        //}
    }
}