using Microsoft.EntityFrameworkCore;
using InformationProcessSupport.Data.Channels;
using InformationProcessSupport.Data.Users;
using InformationProcessSupport.Data.Statistics;
using InformationProcessSupport.Data.TimeOfActionsInTheChannel.MicrophoneActions;
using InformationProcessSupport.Data.TimeOfActionsInTheChannel.CameraActions;
using InformationProcessSupport.Data.TimeOfActionsInTheChannel.StreamActions;
using InformationProcessSupport.Data.TimeOfActionsInTheChannel.SelfDeafenedActions;
using InformationProcessSupport.Data.ScheduleOfSubjects;
using InformationProcessSupport.Data.Groups;

namespace InformationProcessSupport.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> dbContextOptions) : base(dbContextOptions)
        {
            //Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntity.UserConfiguration());
            modelBuilder.ApplyConfiguration(new ChannelEntity.ChannelConfiguration());
            modelBuilder.ApplyConfiguration(new StatisticEntity.StatisticConfiguration());
            modelBuilder.ApplyConfiguration(new MicrophoneActionsEntity.MicrophoneActionsConfiguration());
            modelBuilder.ApplyConfiguration(new CameraActionsEntity.CameraActionsConfiguration());
            modelBuilder.ApplyConfiguration(new StreamActionsEntity.StreamActionsConfiguration());
            modelBuilder.ApplyConfiguration(new SelfDeafenedActionsEntity.SelfDeafenedActionsConfiguration());
            modelBuilder.ApplyConfiguration(new ScheduleEntity.ScheduleConfiguration());
            modelBuilder.ApplyConfiguration(new GroupEntity.GroupConfiguration());
        }
        public DbSet<UserEntity> UsersEntity { get; set; }
        public DbSet<ChannelEntity> ChannelsEntity { get; set; }
        public DbSet<StatisticEntity> StatisticEntities { get; set; }
        public DbSet<MicrophoneActionsEntity> MicrophoneActionsEntity { get; set; }
        public DbSet<CameraActionsEntity> CameraActionsEntity { get; set; }
        public DbSet<StreamActionsEntity> StreamActionsEntity { get; set; }
        public DbSet<SelfDeafenedActionsEntity> SelfDeafenedActionsEntities { get; set; }
        public DbSet<ScheduleEntity> ScheduleEntities { get; set; }
        public DbSet<GroupEntity> GroupEntities { get; set; }
    }
}