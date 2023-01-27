using InformationProcessSupport.Data.TimeOfActionsInTheChannel.MicrophoneActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationProcessSupport.Data.TimeOfActionsInTheChannel.SelfDeafenedActions
{
    public interface ISelfDeafenedActionsRepository
    {
        Task AddSelfDeafenedTurnOffTimeAsync(SelfDeafenedActionsEntity selfDeafenedActionsEntity);
        Task AddendumASelfDeafenedOperatingTime(SelfDeafenedActionsEntity selfDeafenedActionsEntity);
    }
}