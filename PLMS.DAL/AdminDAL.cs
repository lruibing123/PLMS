using PLMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace PLMS.DAL
{
	/// <summary>
	/// AdminDAL 的摘要说明。
	/// </summary>
	[Serializable]
	public class AdminDAL
	{
		private List<Admin> admins;

		public AdminDAL()
		{
			admins = new List<Admin>();
		}

		/// <summary>
		/// 增加管理员
		/// </summary>
		/// <param name="admin"></param>
		/// <returns></returns>
		public bool AddAdmin(Admin admin)
		{
			if (admins.Exists(a => a.Id == admin.Id))
				return false;
			admin.Password = Md5Hash(admin.Password);
			admins.Add(admin);
			return true;
		}

		/// <summary>
		/// 32位MD5加密
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		private static string Md5Hash(string input)
		{
			MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
			byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
			StringBuilder sBuilder = new StringBuilder();
			for (int i = 0; i < data.Length; i++)
			{
				sBuilder.Append(data[i].ToString("x2"));
			}
			return sBuilder.ToString();
		}

		/// <summary>
		/// 根据ID删除管理员
		/// </summary>
		/// <param name="adminId"></param>
		/// <returns></returns>
		public bool RemoveAdminById(string adminId)
		{
			int index = admins.FindIndex(a => a.Id == adminId);
			if (index >= 0)
			{
				admins.RemoveAt(index);
				return true;
			}
			return false;
		}

		/// <summary>
		/// 根据ID获得管理员
		/// </summary>
		/// <param name="adminId"></param>
		/// <returns></returns>
		public Admin GetAdminById(string adminId)
		{
			return admins.FirstOrDefault(a => a.Id == adminId);
		}

		/// <summary>
		/// 获得所有管理员
		/// </summary>
		/// <returns></returns>
		public List<Admin> GetAdmins()
		{
			return admins;
		}
	}
}
