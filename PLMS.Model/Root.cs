using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLMS.Model
{
    public class Vertexes_locationItem
    {
        /// <summary>
        /// 图像外接多边形顶点 X 坐标
        /// </summary>
        public int x { get; set; }
        /// <summary>
        /// 图像外接多边形顶点 Y 坐标
        /// </summary>
        public int y { get; set; }
    }

    public class Words_result
    {
        /// <summary>
        /// 车牌号
        /// </summary>
        public string number { get; set; }
        /// <summary>
        /// 图像外接多边形顶点集合
        /// </summary>
        public List<Vertexes_locationItem> vertexes_location { get; set; }
        /// <summary>
        /// 车牌颜色
        /// </summary>
        public string color { get; set; }
        /// <summary>
        /// 区间值
        /// </summary>
        public List<double> probability { get; set; }
    }

    /// <summary>
    /// 百度OCR 接口返回的 Json实体
    /// </summary>
    public class Root
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public Words_result words_result { get; set; }
        /// <summary>
        /// 每次调用的 log_id
        /// </summary>
        public string log_id { get; set; }
    }
}
