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
        public string Phone { get; set; }
        public DateTime ExpirationTime { get; set; }
        public DateTime CreateTime { get; }

        public FormalUser(string licensePlateNum, string phone)
            : base(licensePlateNum)
        {            
            UserId = "F" + IdCreator.Default.Create();
            Phone = phone;
            CreateTime = ExpirationTime = DateTime.Now;
        }
    }
}
