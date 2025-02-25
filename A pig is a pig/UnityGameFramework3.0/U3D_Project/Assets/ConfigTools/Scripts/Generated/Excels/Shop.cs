/*  工具菜单 [ ConfigTools/生成关联脚本 ]
 *
 *  当前文件由工具类生成, 请不要手动修改 !
 *
 *  上次生成时间 : 2025/1/14 15:29:28
 */
using System.Collections.Generic;
using ZeroFormatter;

namespace ConfigTools
{
	[ZeroFormattable]
	public class Shop
	{
		//key
		[Index(0)]
		public virtual int id{ get; set; }

		//名称
		[Index(1)]
		public virtual string name{ get; set; }

		//物品id 
		[Index(2)]
		public virtual int itemId{ get; set; }

		//物品数量
		[Index(3)]
		public virtual int itemCount{ get; set; }

		//货币类型
		[Index(4)]
		public virtual int moneyType{ get; set; }

		//价格
		[Index(5)]
		public virtual int price{ get; set; }

		//限购
		[Index(6)]
		public virtual int limit{ get; set; }

		//折扣
		[Index(7)]
		public virtual int discount{ get; set; }

		//商品类型
		[Index(8)]
		public virtual int goodsType{ get; set; }

	}
}
