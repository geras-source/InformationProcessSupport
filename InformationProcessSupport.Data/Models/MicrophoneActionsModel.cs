using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InformationProcessSupport.Data.Models
{
    public class MicrophoneActionsModel
    {
        public int MicrophoneTimeEntityId { get; set; }
        public TimeSpan? MicrophoneOperatingTime { get; set; }
        public DateTime MicrophoneTurnOnTime { get; set; }
        public DateTime? MicrophoneTurnOffTime { get; set; }

        public StatisticModel StatisticEntities { get; set; }
        public int StatisticId { get; set; }
        public class MicrophoneActionsConfiguration : IEntityTypeConfiguration<MicrophoneActionsModel>
        {
            public void Configure(EntityTypeBuilder<MicrophoneActionsModel> builder)
            {
                builder.HasKey(x => x.MicrophoneTimeEntityId);
                builder.HasOne(x => x.StatisticEntities)
                    .WithMany(x => x.MicrophoneActionsEntity)
                    .HasForeignKey(x => x.StatisticId);

            }
        }
    }
}
