using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InformationProcessSupport.Data.Models
{
    public class StatisticModel
    {
        public int StatisticId { get; set; }
        public TimeSpan ConnectionTime { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public string? Attendance { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? ScheduleId { get; set; }
        public virtual ScheduleModel ScheduleEntity { get; set; }
        public int UserId { get; set; }
        public virtual UserModel UserEntity { get; set; }
        public int ChannelId { get; set; }
        public virtual ChannelModel ChannelEntity { get; set; }
        public virtual ICollection<MicrophoneActionsModel> MicrophoneActionsEntity { get; set; }
        public virtual ICollection<CameraActionsModel> CameraActionsEntity { get; set; }
        public virtual ICollection<StreamActionsModel> StreamActionsEntity { get; set; }
        public virtual ICollection<SelfDeafenedActionsModel> SelfDeafenedActionsEntities { get; set; }

        public class StatisticConfiguration : IEntityTypeConfiguration<StatisticModel>
        {
            public void Configure(EntityTypeBuilder<StatisticModel> builder)
            {
                builder.HasKey(x => x.StatisticId);
                
                builder
                    .HasOne(x => x.UserEntity)
                    .WithMany(x => x.StatisticsEntity)
                    .HasForeignKey(x => x.UserId);
                builder
                    .HasOne(x => x.ChannelEntity)
                    .WithMany(x => x.StatisticEntities)
                    .HasForeignKey(x => x.ChannelId);
                builder
                    .HasMany(x => x.MicrophoneActionsEntity)
                    .WithOne(x => x.StatisticEntities);
                builder
                    .HasMany(x => x.CameraActionsEntity)
                    .WithOne(x => x.StatisticEntities);
                builder
                    .HasMany(x => x.StreamActionsEntity)
                    .WithOne(x => x.StatisticEntities);
                builder
                    .HasMany(x => x.SelfDeafenedActionsEntities)
                    .WithOne(x => x.StatisticEntities);
                builder
                    .HasOne(x => x.ScheduleEntity)
                    .WithMany(x => x.StatisticsEntity)
                    .HasForeignKey(x => x.ScheduleId);
            }
        }
    }
}
