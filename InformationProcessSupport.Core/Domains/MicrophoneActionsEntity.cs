namespace InformationProcessSupport.Core.Domains
{
    public class MicrophoneActionsEntity
    {
        public int MicrophoneTimeEntityId { get; set; }
        public TimeSpan? MicrophoneOperatingTime { get; set; }
        public DateTime MicrophoneTurnOnTime { get; set; }
        public DateTime? MicrophoneTurnOffTime { get; set; }

        //public StatisticModel StatisticEntities { get; set; }
        public int StatisticId { get; set; }
        //public class MicrophoneActionsConfiguration : IEntityTypeConfiguration<MicrophoneActionsEntity>
        //{
        //    public void Configure(EntityTypeBuilder<MicrophoneActionsEntity> builder)
        //    {
        //        builder.HasKey(x => x.MicrophoneTimeEntityId);
        //        builder.HasOne(x => x.StatisticEntities)
        //            .WithMany(x => x.MicrophoneActionsEntity)
        //            .HasForeignKey(x => x.StatisticId);

        //    }
        //}
    }
}