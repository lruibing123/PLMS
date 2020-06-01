using PLMS.DAL;
using PLMS.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLMS.BLL
{
	/// <summary>
	/// UserBLL 的摘要说明。
	/// </summary>
	public class BaseBLL
	{
        #region 字段        
		protected static AdminDAL admins;
		protected static CasualUserDAL casualUsers;
		protected static FormalUserDAL formalUsers;
		protected static ParkDAL parks;
		protected static ParkingSpotDAL parkingSpots;
		protected static OrderDAL orders;

		protected static double halfHourPrice = double.Parse(ConfigurationManager.AppSettings["HalfHourPrice"].ToString());
		protected static Dictionary<int, float> prices = new Dictionary<int, float> {
			{3, float.Parse(ConfigurationManager.AppSettings["ThreeMonthsPrice"].ToString())},
			{12, float.Parse(ConfigurationManager.AppSettings["OneYearPrice"].ToString())}
		};
		#endregion

		protected BaseBLL() { }

		public static void InitAll()
		{
			if (!Directory.Exists("data"))
			{
				Directory.CreateDirectory("data");
			}
			admins = DataFileAccess.GetAdmins();
			casualUsers = DataFileAccess.GetCasualUsers();
			formalUsers = DataFileAccess.GetFormalUsers();
			parks = DataFileAccess.GetParks();
			//parkingSpots = DataFileAccess.GetParkingSpots();
			orders = DataFileAccess.GetOrders();
		}

		/// <summary>
		/// 保存所有数据
		/// </summary>
		public static void SaveALL()
		{
			DataFileAccess.SaveAdmins(admins);			
			DataFileAccess.SaveCasualUsers(casualUsers);
			DataFileAccess.SaveFormalUsers(formalUsers);
			DataFileAccess.SaveParks(parks);
			//DataFileAccess.SaveParkingSpots(parkingSpots);
			DataFileAccess.SaveOrders(orders);
		}
	}
}
