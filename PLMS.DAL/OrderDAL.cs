using PLMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLMS.DAL
{
    [Serializable]
	/// <summary>
	/// OrderDAL 的摘要说明。
	/// </summary>
	public class OrderDAL
	{
		private List<Order> orders;

		public OrderDAL()
		{
			orders = new List<Order>();
		}

		/// <summary>
		/// 增加订单
		/// </summary>
		/// <param name="order"></param>
		/// <returns></returns>
		public bool AddOrder(Order order)
		{
			if (orders.Contains(order))
				return false;
			orders.Add(order);
			return true;
		}

		/// <summary>
		/// 根据orderId获得订单
		/// </summary>
		/// <param name="orderId"></param>
		/// <returns></returns>
		public Order GetOrderById(string orderId)
		{
			return orders.FirstOrDefault(o => o.OrderId == orderId);
		}

		/// <summary>
		/// 获得所有订单
		/// </summary>
		/// <returns></returns>
		public List<Order> GetAllOrders()
		{
			return orders;
		}

		/// <summary>
		/// 根据orderId删除订单
		/// </summary>
		/// <param name="orderId"></param>
		/// <returns></returns>
		public bool RemoveOrderById(string orderId)
		{
			int index = orders.FindIndex(o => o.OrderId == orderId);
			if(index >= 0)
			{
				orders.RemoveAt(index);
				return true;
			}			
			return false;
		}
	}
}
