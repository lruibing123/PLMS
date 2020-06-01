using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLMS.Model
{
    [Serializable]
    public class ParkingSpot
    {
        protected static int pid = 0;
        public string ParkingSpotId { get; }
        public bool Occupied { get; set; }
        public bool Enabled { get; set; }     

        public ParkingSpot(string parkingSpotId = null, bool enabled = true)
        {
            ParkingSpotId = parkingSpotId != null ? parkingSpotId : GetNewID();
            Occupied = false;
            Enabled = enabled;
        }

        public string GetNewID()
        {
            return string.Format("P00{0}", pid++);
        }
    }
}
