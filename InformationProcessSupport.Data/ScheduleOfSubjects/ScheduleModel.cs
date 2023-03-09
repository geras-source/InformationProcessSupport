using InformationProcessSupport.Core.ScheduleOfSubjects;
using InformationProcessSupport.Data.Channels;
using InformationProcessSupport.Data.Groups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InformationProcessSupport.Data.ScheduleOfSubjects
{
    public class ScheduleModel
    {
        /// <summary>
        /// Primary key in a database
        /// </summary>
        public int ScheduleId { get; set; }
        /// <summary>
        /// Name of the subject
        /// </summary>
        public string SubjectName { get; set; }
        /// <summary>
        /// Subject start time
        /// </summary>
        public TimeSpan StartTimeTheSubject { get; set; }
        /// <summary>
        /// Subject end time
        /// </summary>
        public TimeSpan EndTimeTheSubject { get; set; }
        /// <summary>
        /// Day of the week
        /// </summary>
        public DayOfTheWeek DayOfTheWeek { get; set; } = DayOfTheWeek.Monday;
        /// <summary>
        /// 
        /// </summary>
        public virtual GroupModel GroupEntity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? GroupId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual ChannelModel ChannelEntity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? ChannelId { get; set; }
        
        public class ScheduleConfiguration : IEntityTypeConfiguration<ScheduleModel>
        {
            public void Configure(EntityTypeBuilder<ScheduleModel> builder)
            {
                builder.HasKey(x => x.ScheduleId);
               
                builder
                    .HasOne(x => x.ChannelEntity)
                    .WithOne(x => x.ScheduleEntity)
                    .HasForeignKey<ScheduleModel>(x => x.ChannelId);

                builder
                    .HasOne(x => x.GroupEntity)
                    .WithOne(x => x.ScheduleEntity)
                    .HasForeignKey<ScheduleModel>(x => x.GroupId);
            }
        }
    }
}