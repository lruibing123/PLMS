using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLMS.Model
{
    [Serializable]
    public class Admin
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public int ErrorNum { get; set; }
        public Admin(string id, string password)
        {
            Id = id;
            Password = password;
            ErrorNum = 0;
        }
    }
}
