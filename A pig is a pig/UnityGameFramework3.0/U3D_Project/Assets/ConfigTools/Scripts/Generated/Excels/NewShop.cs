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
	public class NewShop
	{
		//物品ID
		[Index(0)]
		public virtual int itemid{ get; set; }

		//元宝
		[Index(1)]
		public virtual int gold{ get; set; }

		//绑定元宝
		[Index(2)]
		public virtual int bind_gold{ get; set; }

		//是否传闻
		[Index(3)]
		public virtual bool is_notice{ get; set; }

		//限购商城元宝
		[Index(4)]
		public virtual int vip_gold{ get; set; }

		//vip限制
		[Index(5)]
		public virtual int vip_limit{ get; set; }

		//购买次数限制
		[Index(6)]
		public virtual int buy_limit{ get; set; }

		//备注
		[Index(7)]
		public virtual string remark{ get; set; }

	}
}
