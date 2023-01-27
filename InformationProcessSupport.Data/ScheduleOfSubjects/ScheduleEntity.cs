using InformationProcessSupport.Data.Channels;
using InformationProcessSupport.Data.Groups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InformationProcessSupport.Data.ScheduleOfSubjects
{
    public class ScheduleEntity
    {
        public int ScheduleId { get; set; }
        public string SubjectName { get; set; }
        public TimeSpan StartTimeTheSubject { get; set; }
        public TimeSpan EndTimeTheSubject { get; set; }
        public DayOfTheWeek DayOfTheWeek { get; set; } = DayOfTheWeek.Monday;
        public virtual  GroupEntity GroupEntity { get; set; }
        public int GroupId { get; set; }
        public virtual ChannelEntity ChannelEntity { get; set; }
        public int? ChannelId { get; set; }
        
        public class ScheduleConfiguration : IEntityTypeConfiguration<ScheduleEntity>
        {
            public void Configure(EntityTypeBuilder<ScheduleEntity> builder)
            {
                builder.HasKey(x => x.ScheduleId);
               
                builder
                    .HasOne(x => x.ChannelEntity)
                    .WithOne(x => x.ScheduleEntity)
                    .HasForeignKey<ScheduleEntity>(x => x.ChannelId);

                builder
                    .HasOne(x => x.GroupEntity)
                    .WithOne(x => x.ScheduleEntity)
                    .HasForeignKey<ScheduleEntity>(x => x.GroupId);
            }
        }
    }
}