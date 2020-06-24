using PLMS.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace PLMS.BLL
{
    public class AdminBLL : BaseBLL
    {
        #region 管理员
        public static bool AddAdmin(Admin admin)
        {
            return admins.AddAdmin(admin);
        }

        public static bool RemoveAdminById(string adminId)
        {
            return admins.RemoveAdminById(adminId);
        }

        public static Admin GetAdminById(string adminId)
        {
            return admins.GetAdminById(adminId);
        }

        public static List<Admin> GetAdmins()
        {
            return admins.GetAdmins();
        }

        public static bool ResetErrorNum(string adminId)
        {
            try
            {
                admins.GetAdmins().FirstOrDefault(a => a.Id == adminId).ErrorNum = 0;
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 正式会员
        public static bool AddFormalUser(FormalUser user, int months, out string err)
        {
            if (formalUsers.AddUser(user))            
                if (DelayExpirationTime(user.UserId, months, out err))
                    return true;            
            err = "已存在此牌照";
            return false;
        }

        public static bool RemoveFormalUserById(string userId)
        {
            return formalUsers.RemoveUserById(userId);
        }

        public static User GetFormalUserById(string userId)
        {
            return formalUsers.GetUserById(userId);
        }

        public static List<FormalUser> GetFormalUsers()
        {
            List<FormalUser> users = new List<FormalUser>();
            foreach (User user in formalUsers.GetUsers())
            {
                users.Add((FormalUser)user);
            }
            return users;
        }

        public static bool DelayExpirationTime(string userId, int months, out string err)
        {
            FormalUser formalUser = (FormalUser)formalUsers.GetUsers().Find(f => f.UserId == userId);
            if (formalUser != null)
            {
                float price;
                if (!prices.ContainsKey(months))
                {
                    err = "开通时长不规范";
                    return false;
                }
                prices.TryGetValue(months, out price);
                Order order = new Order(userId, formalUser.LicensePlateNum, price, "开通 " + months + "月");
                orders.AddOrder(order);
                formalUser.ExpirationTime = formalUser.ExpirationTime.AddMonths(months);
                err = "支付正常";
                return true;
            }
            err = "未知错误";
            return false;
        }
        #endregion

        #region 临时会员
        public static bool AddCasualUser(CasualUser user)
        {
            return casualUsers.AddUser(user);
        }

        public static bool RemoveCasualUserById(string userId)
        {
            return casualUsers.RemoveUserById(userId);
        }

        public static User GetCasualUsersById(string userId)
        {
            return casualUsers.GetUserById(userId);
        }

        public static List<CasualUser> GetCasualUsers()
        {
            List<CasualUser> users = new List<CasualUser>();
            foreach (User user in casualUsers.GetUsers())
            {
                users.Add((CasualUser)user);
            }
            return users;
        }
        #endregion

        #region 停车记录
        public static bool AddPark(Park park)
        {
            return parks.AddPark(park);
        }

        public static Park GetParkById(string parkId)
        {
            return parks.GetParkById(parkId);
        }

        public static int GetActiveParkNum()
        {
            return parks.GetAllParks().Count(p => p.LeaveTime == DateTime.MinValue);
        }

        public static int GetParkingSpotNum()
        {
            return parkingSpotNum;
        }

        public static Park GetParkByUserId(string userId)
        {
            List<Park> parks1 = parks.GetAllParks().Where(p => p.UserId == userId).ToList();
            return parks1.FirstOrDefault(e => e.EntryTime == parks1.Max(a => a.EntryTime));
        }

        public static List<Park> GetParks()
        {
            return parks.GetAllParks();
        }

        public static bool RemoveParkById(string parkId)
        {
            return parks.RemoveParkById(parkId);
        }
        #endregion

        #region 订单
        public static bool AddOrder(Order order)
        {
            return orders.AddOrder(order);
        }

        public static Order GetOrderById(string userId)
        {
            return orders.GetAllOrders().Find(o => o.UserId == userId);
        }

        public static List<Order> GetOrders()
        {
            return orders.GetAllOrders();
        }

        public static bool RemoveOrderById(string orderId)
        {
            return orders.RemoveOrderById(orderId);
        }
        #endregion
    }
}
