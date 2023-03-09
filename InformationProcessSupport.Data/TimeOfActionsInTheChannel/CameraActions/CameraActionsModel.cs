using InformationProcessSupport.Data.Statistics;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InformationProcessSupport.Data.TimeOfActionsInTheChannel.CameraActions
{
    public class CameraActionsModel
    {
        public int CameraActionsId { get; set; }
        public TimeSpan? CameraOperationTime { get; set; }
        public DateTime? CameraTurnOnTime { get; set; }
        public DateTime? CameraTurnOffTime { get; set; }

        public StatisticModel StatisticEntities { get; set; }
        public int StatistisId { get; set; }
        public class CameraActionsConfiguration : IEntityTypeConfiguration<CameraActionsModel>
        {
            public void Configure(EntityTypeBuilder<CameraActionsModel> builder)
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