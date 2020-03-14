namespace LoLBoosting.Contracts.Models
{
    public enum EOrderStatus
    {
        Undefined = 0,
        WaitingVerification = 1,
        WaitingForBooster = 2,
        ClaimedByBooster = 3,
        Paused = 4,
        Finished = 5,
    }
}
