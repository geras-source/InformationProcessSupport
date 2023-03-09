using InformationProcessSupport.Data.Channels;
using InformationProcessSupport.Data.TimeOfActionsInTheChannel.CameraActions;
using InformationProcessSupport.Data.TimeOfActionsInTheChannel.MicrophoneActions;
using InformationProcessSupport.Data.TimeOfActionsInTheChannel.SelfDeafenedActions;
using InformationProcessSupport.Data.TimeOfActionsInTheChannel.StreamActions;
using InformationProcessSupport.Data.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace InformationProcessSupport.Data.Statistics
{
    [Table("Statistics")]
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

                builder.HasOne(x => x.UserEntity)
                    .WithMany(x => x.StatisticsEntity)
                    .HasForeignKey(x => x.UserId);
                builder.HasOne(x => x.ChannelEntity)
                    .WithMany(x => x.StatisticEntities)
                    .HasForeignKey(x => x.ChannelId);
                builder.HasMany(x => x.MicrophoneActionsEntity)
                    .WithOne(x => x.StatisticEntities);
                builder.HasMany(x => x.CameraActionsEntity)
                    .WithOne(x => x.StatisticEntities);
                builder.HasMany(x => x.StreamActionsEntity)
                    .WithOne(x => x.StatisticEntities);
                builder.HasMany(x => x.SelfDeafenedActionsEntities)
                    .WithOne(x => x.StatisticEntities);
            }
        }
    }
}