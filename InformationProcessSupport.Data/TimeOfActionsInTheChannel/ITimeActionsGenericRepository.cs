using InformationProcessSupport.Data.TimeOfActionsInTheChannel.MicrophoneActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationProcessSupport.Data.TimeOfActionsInTheChannel
{
    public interface ITimeActionsGenericRepository<TEntity> where TEntity : class
    {
        Task AddTurnOffTimeAsync(TEntity entity);
        Task AddendumAOperatingTime(TEntity entity);
    }
}
