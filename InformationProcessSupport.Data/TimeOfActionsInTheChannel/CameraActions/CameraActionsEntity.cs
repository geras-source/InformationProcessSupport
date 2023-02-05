using InformationProcessSupport.Data.Statistics;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InformationProcessSupport.Data.TimeOfActionsInTheChannel.CameraActions
{
    public class CameraActionsEntity
    {
        public int CameraActionsId { get; set; }
        public TimeSpan? CameraOperationTime { get; set; }
        public DateTime? CameraTurnOnTime { get; set; }
        public DateTime? CameraTurnOffTime { get; set; }

        public StatisticEntity StatisticEntities { get; set; }
        public int StatistisId { get; set; }
        public class CameraActionsConfiguration : IEntityTypeConfiguration<CameraActionsEntity>
        {
            public void Configure(EntityTypeBuilder<CameraActionsEntity> builder)
            {
                builder.HasKey(x => x.CameraActionsId);
                builder
                    .HasOne(x => x.StatisticEntities)
                    .WithMany(x => x.CameraActionsEntity)
                    .HasForeignKey(x => x.StatistisId);

            }
        }
    }
}