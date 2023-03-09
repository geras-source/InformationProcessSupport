namespace InformationProcessSupport.Core.TimeOfActionsInTheChannel.StreamActions
{
    public class StreamActionsEntity
    {
        public int StreamActionsEntityId { get; set; }
        public TimeSpan? StreamOperationTime { get; set; }
        public DateTime? StreamTurnOnTime { get; set; }
        public DateTime? StreamTurnOffTime { get; set; }

        //public StatisticModel StatisticEntities { get; set; }
        public int StatistisId { get; set; }
        //public class StreamActionsConfiguration : IEntityTypeConfiguration<StreamActionsEntity>
        //{
        //    public void Configure(EntityTypeBuilder<StreamActionsEntity> builder)
        //    {
        //        builder.HasKey(x => x.StreamActionsEntityId);
        //        builder.HasOne(x => x.StatisticEntities)
        //            .WithMany(x => x.StreamActionsEntity)
        //            .HasForeignKey(x => x.StatistisId);

        //    }
        //}
    }
}
