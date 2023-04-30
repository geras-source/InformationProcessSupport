using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InformationProcessSupport.Data.Models
{
    public class StreamActionsModel
    {
        public int StreamActionsEntityId { get; set; }
        public TimeSpan? StreamOperationTime { get; set; }
        public DateTime? StreamTurnOnTime { get; set; }
        public DateTime? StreamTurnOffTime { get; set; }

        public StatisticModel StatisticEntities { get; set; }
        public int StatisticId { get; set; }
        public class StreamActionsConfiguration : IEntityTypeConfiguration<StreamActionsModel>
        {
            public void Configure(EntityTypeBuilder<StreamActionsModel> builder)
            {
                builder.HasKey(x => x.StreamActionsEntityId);
                builder.HasOne(x => x.StatisticEntities)
                    .WithMany(x => x.StreamActionsEntity)
                    .HasForeignKey(x => x.StatisticId);
            }
        }
    }
}
