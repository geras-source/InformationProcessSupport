namespace InformationProcessSupport.Core.TimeOfActionsInTheChannel.SelfDeafenedActions
{
    public class SelfDeafenedActionsEntity
    {
        public int SelfDeafenedActionsId { get; set; }
        public TimeSpan? SelfDeafenedOperationTime { get; set; }
        public DateTime SelfDeafenedTurnOnTime { get; set; }
        public DateTime? SelfDeafenedTurnOffTime { get; set; }

        //public StatisticModel StatisticEntities { get; set; }
        public int StatistisId { get; set; }
        //public class SelfDeafenedActionsConfiguration : IEntityTypeConfiguration<SelfDeafenedActionsEntity>
        //{
        //    public void Configure(EntityTypeBuilder<SelfDeafenedActionsEntity> builder)
        //    {
        //        builder.HasKey(x => x.SelfDeafenedActionsId);
        //        builder.HasOne(x => x.StatisticEntities)
        //            .WithMany(x => x.SelfDeafenedActionsEntities)
        //            .HasForeignKey(x => x.StatistisId);

        //    }
        //}
    }
}