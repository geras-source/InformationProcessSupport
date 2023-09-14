using Microsoft.EntityFrameworkCore;
using InformationProcessSupport.Data.Models;

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
            modelBuilder.ApplyConfiguration(new UserModel.UserConfiguration());
            modelBuilder.ApplyConfiguration(new ChannelModel.ChannelConfiguration());
            modelBuilder.ApplyConfiguration(new StatisticModel.StatisticConfiguration());
            modelBuilder.ApplyConfiguration(new MicrophoneActionsModel.MicrophoneActionsConfiguration());
            modelBuilder.ApplyConfiguration(new CameraActionsModel.CameraActionsConfiguration());
            modelBuilder.ApplyConfiguration(new StreamActionsModel.StreamActionsConfiguration());
            modelBuilder.ApplyConfiguration(new SelfDeafenedActionsModel.SelfDeafenedActionsConfiguration());
            modelBuilder.ApplyConfiguration(new ScheduleModel.ScheduleConfiguration());
            modelBuilder.ApplyConfiguration(new GroupModel.GroupConfiguration());
        }
        public DbSet<UserModel> UserEntities { get; set; }
        public DbSet<ChannelModel> ChannelEntities { get; set; }
        public DbSet<StatisticModel> StatisticEntities { get; set; }
        public DbSet<MicrophoneActionsModel> MicrophoneActionEntities { get; set; }
        public DbSet<CameraActionsModel> CameraActionEntities { get; set; }
        public DbSet<StreamActionsModel> StreamActionEntities { get; set; }
        public DbSet<SelfDeafenedActionsModel> SelfDeafenedActionsEntities { get; set; }
        public DbSet<ScheduleModel> ScheduleEntities { get; set; }
        public DbSet<GroupModel> GroupEntities { get; set; }
        public DbSet<UserAuthenticationModel> UserAuthenticationEntities { get; set; }
    }
}