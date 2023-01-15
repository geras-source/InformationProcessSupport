using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationProcessSupport.Data.TimeOfActionsInTheChannel.MicrophoneActions
{
    public interface IMicrophoneActionsRepository
    {
        Task AddMicrophoneTurnOffTimeAsync(MicrophoneActionsEntity microphoneActionsEntity);
        Task AddendumAMicrophoneOperatingTime(MicrophoneActionsEntity microphoneActionsEntity);
    }
}
