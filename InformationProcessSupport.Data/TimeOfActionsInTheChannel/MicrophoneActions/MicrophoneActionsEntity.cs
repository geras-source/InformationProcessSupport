using InformationProcessSupport.Data.Statistics;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InformationProcessSupport.Data.TimeOfActionsInTheChannel.MicrophoneActions
{
    public class MicrophoneActionsEntity
    {
        public int MicrophoneTimeEntityId { get; set; }
        public TimeSpan? MicrophoneOperatingTime { get; set; }
        public DateTime MicrophoneTurnOnTime { get; set; }
        public DateTime? MicrophoneTurnOffTime { get; set; }
        
        public StatisticEntity StatisticEntities { get; set; }
        public int StatistisId { get; set; }
        public class MicrophoneActionsConfiguration : IEntityTypeConfiguration<MicrophoneActionsEntity>
        {
            public void Configure(EntityTypeBuilder<MicrophoneActionsEntity> builder)
            {
                builder.HasKey(x => x.MicrophoneTimeEntityId);
                builder.HasOne(x => x.StatisticEntities)
                    .WithMany(x => x.MicrophoneActionsEntity)
                    .HasForeignKey(x => x.StatistisId);

            }
        }
    }
}