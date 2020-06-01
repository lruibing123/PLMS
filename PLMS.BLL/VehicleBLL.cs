using Newtonsoft.Json;
using PLMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLMS.BLL
{
    public class VehicleBLL : BaseBLL
    {          
        public static bool VehicleEnter(string licensePlateNum, out string result)
        {
            try
            {
                User user = GetUserByLicensePlateNum(licensePlateNum);                
                if (user == null)                                    
                {
                    user = new CasualUser(licensePlateNum);
                    casualUsers.AddUser(user);
                }
                Park park = new Park(user.UserId, user.LicensePlateNum);
                parks.AddPark(park);                
                result = ShowToUser(user);
                return true;
            }
            catch (Exception)
            {
                result = "信息录入错误";
                return false;
            }
        }        

        public static bool VehicleLeave(string licensePlateNum, out string result)
        {              
            try
            {
                User user = GetUserByLicensePlateNum(licensePlateNum);
                Park park = AdminBLL.GetParkByUserId(user.UserId);
                park.LeaveTime = DateTime.Now;
                if (IsNeedToPay(user))
                    PayParkingFee(user.UserId, user.LicensePlateNum, park);
                result = ShowToUser(user);
                return true;
            }
            catch (Exception)
            {
                result = "信息录入错误";
                return false;
            }
        }

        public static User GetUserByLicensePlateNum(string licensePlateNum)
        {
            User user = formalUsers.GetUsers().Find(f => f.LicensePlateNum == licensePlateNum);
            if (user != null)
            {
                FormalUser formalUser = (FormalUser)user;
                return formalUser;
            }
            else
            {
                user = casualUsers.GetUsers().Find(c => c.LicensePlateNum == licensePlateNum);
                CasualUser casualUser = (CasualUser)user;
                return casualUser;
            }
        }

        private static bool IsNeedToPay(User user)
        {
            if (user == null)
            {
                if (user.IsFormal)
                {
                    FormalUser formalUser = (FormalUser)user;
                    if (formalUser.ExpirationTime.CompareTo(DateTime.Now.Date) >= 0)
                        return false;
                }
            }
            return true;
        }

        private static bool PayParkingFee(string userId, string licensePlateNum, Park park)
        {
            if (park == null) return false;
            double halfHours = Math.Ceiling(DateTime.Now.Subtract(park.EntryTime).TotalHours);
            float price = Convert.ToInt32(halfHourPrice * halfHours);
            Alipay(price);
            Order order = new Order(userId, licensePlateNum, price, "停车时长 " + 0.5 * halfHours + "h", park.ParkId);
            orders.AddOrder(order);
            return true;
        }

        private static void Alipay(float price)
        {
            return;
        }

        private static string ShowToUser(User user)
        {
            string head;
            head = user.IsFormal ? "正式车" : "临时车";
            return head + "\n" + user.LicensePlateNum + "\n" + DateTime.Now.ToLongTimeString();
        }
    }
}
