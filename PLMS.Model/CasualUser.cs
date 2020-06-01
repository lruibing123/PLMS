using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLMS.Model
{
    [Serializable]
    public class CasualUser : User
    {
        public static int cid = 1;

        public CasualUser(string licensePlateNum, string id = null)
            : base(licensePlateNum)
        {
            UserId = id != null ? id : GetNewID();            
        }

        protected override string GetNewID()
        {
            return string.Format("C00{0}", cid++);
        }
    }
}
