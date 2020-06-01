using PLMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			admins.Add(admin);
			return true;
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
