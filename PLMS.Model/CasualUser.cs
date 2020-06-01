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
        public CasualUser(string licensePlateNum)
            : base(licensePlateNum)
        {      
            UserId = "C" + IdCreator.Default.Create();
        }
    }
}
