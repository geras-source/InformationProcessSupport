using InformationProcessSupport.Data.Statistics;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InformationProcessSupport.Data.TimeOfActionsInTheChannel.SelfDeafenedActions
{
    public class SelfDeafenedActionsEntity
    {
        public int SelfDeafenedActionsId { get; set; }
        public TimeSpan? SelfDeafenedOperationTime { get; set; }
        public DateTime SelfDeafenedTurnOnTime { get; set; }
        public DateTime? SelfDeafenedTurnOffTime { get; set; }

        public StatisticEntity StatisticEntities { get; set; }
        public int StatistisId { get; set; }
        public class SelfDeafenedActionsConfiguration : IEntityTypeConfiguration<SelfDeafenedActionsEntity>
        {
            public void Configure(EntityTypeBuilder<SelfDeafenedActionsEntity> builder)
            {
                builder.HasKey(x => x.SelfDeafenedActionsId);
                builder.HasOne(x => x.StatisticEntities)
                    .WithMany(x => x.SelfDeafenedActionsEntities)
                    .HasForeignKey(x => x.StatistisId);

            }
        }
    }
}