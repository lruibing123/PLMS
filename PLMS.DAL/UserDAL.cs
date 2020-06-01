using PLMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLMS.DAL
{
	/// <summary>
	/// UserDAL 的摘要说明。
	/// </summary>
	[Serializable]
	public class UserDAL
	{
		protected List<User> users;

		public UserDAL()
		{
			users = new List<User>();
		}

		/// <summary>
		/// 增加用户
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public bool AddUser(User user)
		{
			if (users.Exists(u => u.LicensePlateNum == user.LicensePlateNum))
				return false;
			users.Add(user);
			return true;
		}

		/// <summary>
		/// 根据ID删除用户
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public bool RemoveUserById(string userId)
		{
			foreach (User user in users)
			{
				if (userId == user.UserId)
				{
					users.Remove(user);
					return true;
				}					
			}
			return false;
		}		

		/// <summary>
		/// 根据ID获得用户
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public User GetUserById(string userId)
		{
			for (int i = 0; i < users.Count; i++)
			{
				if (userId == users[i].UserId)
				{
					return users[i];
				}
			}
			return null;
		}

		/// <summary>
		/// 获取所有用户
		/// </summary>
		/// <returns></returns>
		public List<User> GetUsers()
		{
			return users;
		}
	}
}
