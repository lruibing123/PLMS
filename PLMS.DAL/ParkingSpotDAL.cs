using PLMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLMS.DAL
{
	[Serializable]
	public class ParkingSpotDAL
	{
		private List<ParkingSpot> parkingSpots;

		public ParkingSpotDAL()
		{
			parkingSpots = new List<ParkingSpot>();
		}

		/// <summary>
		/// 增加停车位
		/// </summary>
		/// <param name="parkingSpot"></param>
		public bool AddParkingSpot(ParkingSpot parkingSpot)
		{
			if (parkingSpots.Exists(p => p.ParkingSpotId == parkingSpot.ParkingSpotId))
				return false;
			parkingSpots.Add(parkingSpot);
			return true;
		}

		/// <summary>
		/// 根据parkingSpotId获得停车位
		/// </summary>
		/// <param name="parkingSpotId"></param>
		/// <returns></returns>
		public ParkingSpot GetParkingSpotById(string parkingSpotId)
		{
			for (int i = 0; i < parkingSpots.Count; i++)
			{
				if (parkingSpotId == parkingSpots[i].ParkingSpotId)
				{
					return parkingSpots[i];
				}
			}
			return null;
		}

		/// <summary>
		/// 获得所有停车位
		/// </summary>
		/// <returns></returns>
		public List<ParkingSpot> GetAllParks()
		{
			return parkingSpots;
		}

		/// <summary>
		/// 根据parkingSpotId删除停车位
		/// </summary>
		public bool RemoveParkSpotById(string parkingSpotId)
		{
			for (int i = 0; i < parkingSpots.Count; i++)
			{
				if (parkingSpotId == parkingSpots[i].ParkingSpotId)
				{
					parkingSpots.RemoveAt(i);
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 传入停车位对象，若存在数据中则更新
		/// </summary>
		/// <param name="parkingSpot"></param>
		public void UpadteParkingSpot(ParkingSpot parkingSpot)
		{
			int index = parkingSpots.FindIndex(p => p.ParkingSpotId == parkingSpot.ParkingSpotId);
			if (index >= 0) parkingSpots[index] = parkingSpot;
		}
	}
}
