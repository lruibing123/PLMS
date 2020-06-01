using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLMS.Model
{
    [Serializable]
    public class FormalUser : User
    {
        protected static int fid = 1;
        public string Phone { get; set; }
        public DateTime ExpirationTime { get; set; }
        public DateTime CreateTime { get; }

        public FormalUser(string licensePlateNum, string phone, string id = null)
            : base(licensePlateNum)
        {
            UserId = id != null ? id : GetNewID();
            Phone = phone;
            CreateTime = ExpirationTime = DateTime.Now.Date;
        }

        protected override string GetNewID()
        {
            return string.Format("F00{0}", fid++);
        }
    }
}
