using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLMS.Model
{
    [Serializable]
    public class Order
    {
        public string OrderId { get; }
        public string UserId { get; }
        public string LicensePlateNum { get; }
        public string ParkId { get; }
        public string Details { get; }
        public float Money { get; }        
        public DateTime CreateTime { get; }
        public bool IsCasual { get { return UserId[0] == 'C'; } }

        public Order(string userId, string licensePlateNum, float money, string details = null, string parkId = null)
        {
            OrderId = Guid.NewGuid().ToString();
            UserId = userId;
            LicensePlateNum = licensePlateNum;
            ParkId = parkId;            
            Money = money;
            Details = details;
            CreateTime = DateTime.Now;
        }
    }
}
