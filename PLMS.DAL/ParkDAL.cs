using PLMS.Model;
using System;
using System.Collections.Generic;

namespace PLMS.DAL
{
	/// <summary>
	/// ParkDAL 的摘要说明。
	/// </summary>
	[Serializable]
	public class ParkDAL
	{
		private List<Park> parks;

		public ParkDAL()
		{
			parks = new List<Park>();
		}

		/// <summary>
		/// 增加停车信息
		/// </summary>
		/// <param name="park"></param>
		/// <returns></returns>
		public bool AddPark(Park park)
		{
			for (int i = 0; i < parks.Count; i++)
			{
				if (park.ParkId == parks[i].ParkId)
				{
					return false;
				}
			}
			parks.Add(park);
			return true;
		}

		/// <summary>
		/// 根据parkId获得停车信息
		/// </summary>
		/// <param name="parkId"></param>
		/// <returns></returns>
		public Park GetParkById(string parkId)
		{
			for (int i = 0; i < parks.Count; i++)
			{
				if (parkId == parks[i].ParkId)
				{
					return parks[i];
				}
			}
			return null;
		}

		/// <summary>
		/// 获得所有停车信息
		/// </summary>
		/// <returns></returns>
		public List<Park> GetAllParks()
		{
			return parks;
		}

		/// <summary>
		/// 根据parkId删除停车信息
		/// </summary>
		/// <param name="parkId"></param>
		/// <returns></returns>
		public bool RemoveParkById(string parkId)
		{
			for (int i = 0; i < parks.Count; i++)
			{
				if (parkId == parks[i].ParkId)
				{
					parks.RemoveAt(i);
					return true;
				}
			}
			return false;
		}
	}
}