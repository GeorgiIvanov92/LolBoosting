using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LolBoosting.Models.Enums
{
    public enum OrderStatus
    {
        Undefined = 0,
        WaitingVerification = 1,
        WaitingForBooster = 2,
        ClaimedByBooster = 3,
        Paused = 4,
        Finished = 5,
    }
}
