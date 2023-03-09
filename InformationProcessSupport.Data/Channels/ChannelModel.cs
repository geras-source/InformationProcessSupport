using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using InformationProcessSupport.Data.Statistics;
using InformationProcessSupport.Data.ScheduleOfSubjects;

namespace InformationProcessSupport.Data.Channels
{
    public class ChannelModel
    {
        /// <summary>
        /// Primary key in a database (don't used)
        /// </summary>
        public int ChannelId { get; set; }
        /// <summary>
        /// The real id this channel
        /// </summary>
        public ulong AlternateKey { get; set; }
        /// <summary>
        /// The name that channel
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Voice or text channel
        /// </summary>
        public string? CategoryType { get; set; }
        /// <summary>
        /// The name of the guild to which this channel belongs
        /// </summary>
        public string? GuildName { get; set; }
        /// <summary>
        /// The id of the guild to which this channel belongs
        /// </summary>
        public ulong GuildId { get; set; }
        /// <summary>
        /// One-to-many collection (don't used)
        /// </summary>
        public ICollection<StatisticModel> StatisticEntities { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ScheduleModel ScheduleEntity { get; set; }
        public class ChannelConfiguration : IEntityTypeConfiguration<ChannelModel>
        {
            public void Configure(EntityTypeBuilder<ChannelModel> builder)
            {
                builder.HasKey(x => x.ChannelId);
                builder.HasAlternateKey(x => x.AlternateKey);
            }
        }
    }
}