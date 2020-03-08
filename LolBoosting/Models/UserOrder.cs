
namespace LolBoosting.Models
{
    public class UserOrder
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
