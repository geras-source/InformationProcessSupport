using InformationProcessSupport.Data.Statistics;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InformationProcessSupport.Data.TimeOfActionsInTheChannel.StreamActions
{
    public class StreamActionsEntity
    {
        public int StreamActionsEntityId { get; set; }
        public TimeSpan? StreamOperationTime { get; set; }
        public DateTime? StreamTurnOnTime { get; set; }
        public DateTime? StreamTurnOffTime { get; set; }

        public StatisticEntity StatisticEntities { get; set; }
        public int StatistisId { get; set; }
        public class StreamActionsConfiguration : IEntityTypeConfiguration<StreamActionsEntity>
        {
            public void Configure(EntityTypeBuilder<StreamActionsEntity> builder)
            {
                builder.HasKey(x => x.StreamActionsEntityId);
                builder.HasOne(x => x.StatisticEntities)
                    .WithMany(x => x.StreamActionsEntity)
                    .HasForeignKey(x => x.StatistisId);

            }
        }
    }
}
