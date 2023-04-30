using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InformationProcessSupport.Data.Models
{
    public class SelfDeafenedActionsModel
    {
        public int SelfDeafenedActionsId { get; set; }
        public TimeSpan? SelfDeafenedOperationTime { get; set; }
        public DateTime SelfDeafenedTurnOnTime { get; set; }
        public DateTime? SelfDeafenedTurnOffTime { get; set; }

        public StatisticModel StatisticEntities { get; set; }
        public int StatisticId { get; set; }
        public class SelfDeafenedActionsConfiguration : IEntityTypeConfiguration<SelfDeafenedActionsModel>
        {
            public void Configure(EntityTypeBuilder<SelfDeafenedActionsModel> builder)
            {
                builder.HasKey(x => x.SelfDeafenedActionsId);
                builder.HasOne(x => x.StatisticEntities)
                    .WithMany(x => x.SelfDeafenedActionsEntities)
                    .HasForeignKey(x => x.StatisticId);

            }
        }
    }
}
