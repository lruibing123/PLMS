using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLMS.Model
{
    [Serializable]
    public abstract class User
    {
        public string UserId { get; protected set; }
        public string LicensePlateNum { get; set; }
        public bool IsFormal { get { return UserId[0] == 'F'; } }
        public User(string licensePlateNum)
        {
            LicensePlateNum = licensePlateNum;
        }
    }
}
