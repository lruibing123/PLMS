using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLMS.Model
{
    [Serializable]
    public class Park
    {
        public string ParkId { get; }
        public string UserId { get; }
        public string LicensePlateNum { get; }
        public DateTime EntryTime { get; }
        public DateTime LeaveTime { get; set; }        

        public Park(string userId, string licensePlateNum)
        {
            ParkId = Guid.NewGuid().ToString();
            UserId = userId;
            LicensePlateNum = licensePlateNum;
            EntryTime = DateTime.Now;
        }
    }
}
