using PLMS.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace PLMS.DAL
{
    [Serializable]
	public class DataFileAccess
	{
        #region 字段
        private static string adminDocPath;
		private static string casualUserDocPath;
		private static string formalUserDocPath;
		private static string parkDocPath;
		private static string parkingSpotDocPath;
		private static string orderDocPath;
        #endregion

        private DataFileAccess() { }

		static DataFileAccess()
		{
			adminDocPath = @"data/adminDoc.dat";
			casualUserDocPath = @"data/casualUserDoc.dat";
			formalUserDocPath = @"data/formalUserDoc.dat";
			parkDocPath = @"data/parkDoc.dat";
			parkingSpotDocPath = @"data/parkingSpotDoc.dat";
			orderDocPath = @"data/orderDoc.dat";
		}

		/// <summary>
		/// 读取临时用户
		/// </summary>
		/// <returns></returns>
		public static CasualUserDAL GetCasualUsers()
		{
			CasualUserDAL casualUsers;
			if (File.Exists(casualUserDocPath))
			{
				using (FileStream fs = new FileStream(casualUserDocPath, FileMode.Open, FileAccess.Read))
				{
					BinaryFormatter bf = new BinaryFormatter();
					casualUsers = (CasualUserDAL)bf.Deserialize(fs);
				}
			}
			else
			{
				using (FileStream fs = new FileStream(casualUserDocPath, FileMode.CreateNew, FileAccess.Write))
				{
					BinaryFormatter bf = new BinaryFormatter();
					casualUsers = new CasualUserDAL();					
					bf.Serialize(fs, casualUsers);
				}
			}
			return casualUsers;
		}

		/// <summary>
		/// 保存临时用户
		/// </summary>
		/// <param name="casualUsers"></param>
		public static void SaveCasualUsers(CasualUserDAL casualUsers)
		{
			using (FileStream fs = new FileStream(casualUserDocPath, FileMode.Create, FileAccess.Write))
			{
				BinaryFormatter bf = new BinaryFormatter();
				bf.Serialize(fs, casualUsers);
			}
		}

		/// <summary>
		/// 读取正式会员
		/// </summary>
		/// <returns></returns>
		public static FormalUserDAL GetFormalUsers()
		{
			FormalUserDAL formalUsers;
			if (File.Exists(formalUserDocPath))
			{
				using (FileStream fs = new FileStream(formalUserDocPath, FileMode.Open, FileAccess.Read))
				{
					BinaryFormatter bf = new BinaryFormatter();
					formalUsers = (FormalUserDAL)bf.Deserialize(fs);
				}
			}
			else
			{
				using (FileStream fs = new FileStream(formalUserDocPath, FileMode.CreateNew, FileAccess.Write))
				{
					BinaryFormatter bf = new BinaryFormatter();
					formalUsers = new FormalUserDAL();
					bf.Serialize(fs, formalUsers);
				}
			}
			return formalUsers;
		}

		/// <summary>
		/// 保存正式会员
		/// </summary>
		/// <param name="teachers"></param>
		public static void SaveFormalUsers(FormalUserDAL teachers)
		{
			using (FileStream fs = new FileStream(formalUserDocPath, FileMode.Create, FileAccess.Write))
			{
				BinaryFormatter bf = new BinaryFormatter();
				bf.Serialize(fs, teachers);
			}
		}

		/// <summary>
		/// 读取管理员
		/// </summary>
		/// <returns></returns>
		public static AdminDAL GetAdmins()
		{
			AdminDAL admins;
			if (File.Exists(adminDocPath))
			{
				using (FileStream fs = new FileStream(adminDocPath, FileMode.Open, FileAccess.Read))
				{
					BinaryFormatter bf = new BinaryFormatter();
					admins = (AdminDAL)bf.Deserialize(fs);
				}
			}
			else
			{
				using (FileStream fs = new FileStream(adminDocPath, FileMode.CreateNew, FileAccess.Write))
				{
					BinaryFormatter bf = new BinaryFormatter();
					admins = new AdminDAL();
					Admin admin = new Admin("admin", "123");
					admins.AddAdmin(admin);
					bf.Serialize(fs, admins);
				}
			}
			return admins;
		}

		/// <summary>
		/// 保存管理员
		/// </summary>
		/// <param name="admins"></param>
		public static void SaveAdmins(AdminDAL admins)
		{
			using (FileStream fs = new FileStream(adminDocPath, FileMode.Create, FileAccess.Write))
			{
				BinaryFormatter bf = new BinaryFormatter();
				bf.Serialize(fs, admins);
			}
		}

		/// <summary>
		/// 读取停车信息
		/// </summary>
		/// <returns></returns>
		public static ParkDAL GetParks()
		{
			ParkDAL parks;
			if (File.Exists(parkDocPath))
			{
				using (FileStream fs = new FileStream(parkDocPath, FileMode.Open, FileAccess.Read))
				{
					BinaryFormatter bf = new BinaryFormatter();
					parks = (ParkDAL)bf.Deserialize(fs);
				}
			}
			else
			{
				using (FileStream fs = new FileStream(parkDocPath, FileMode.CreateNew, FileAccess.Write))
				{
					BinaryFormatter bf = new BinaryFormatter();
					parks = new ParkDAL();
					bf.Serialize(fs, parks);
				}
			}
			return parks;
		}

		/// <summary>
		/// 保存停车信息
		/// </summary>
		/// <param name="courses"></param>
		public static void SaveParks(ParkDAL courses)
		{
			using (FileStream fs = new FileStream(parkDocPath, FileMode.Create, FileAccess.Write))
			{
				BinaryFormatter bf = new BinaryFormatter();
				bf.Serialize(fs, courses);
			}
		}

		/// <summary>
		/// 读取订单
		/// </summary>
		/// <returns></returns>
		public static OrderDAL GetOrders()
		{
			OrderDAL orders;
			if (File.Exists(orderDocPath))
			{
				using (FileStream fs = new FileStream(orderDocPath, FileMode.Open, FileAccess.Read))
				{
					BinaryFormatter bf = new BinaryFormatter();
					orders = (OrderDAL)bf.Deserialize(fs);
				}
			}
			else
			{
				using (FileStream fs = new FileStream(orderDocPath, FileMode.CreateNew, FileAccess.Write))
				{
					BinaryFormatter bf = new BinaryFormatter();
					orders = new OrderDAL();
					bf.Serialize(fs, orders);
				}
			}
			return orders;
		}

		/// <summary>
		/// 保存订单
		/// </summary>
		/// <param name="courses"></param>
		public static void SaveOrders(OrderDAL courses)
		{
			using (FileStream fs = new FileStream(orderDocPath, FileMode.Create, FileAccess.Write))
			{
				BinaryFormatter bf = new BinaryFormatter();
				bf.Serialize(fs, courses);
			}
		}
	}
}
