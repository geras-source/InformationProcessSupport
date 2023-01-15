using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using InformationProcessSupport.Data.Statistics;

namespace InformationProcessSupport.Data.Channels
{
    public class ChannelEntity
    {
        /// <summary>
        /// Primary key in a database (don't used)
        /// </summary>
        public ulong ChannelId { get; set; }
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
        /// One-to-many collection (don't used)
        /// </summary>
        public ICollection<StatisticEntity> StatisticEntities { get; set; }

        public class ChannelConfiguration : IEntityTypeConfiguration<ChannelEntity>
        {
            public void Configure(EntityTypeBuilder<ChannelEntity> builder)
            {
                builder.HasKey(x => x.ChannelId);
                builder.HasAlternateKey(x => x.AlternateKey);
            }
        }
    }
}